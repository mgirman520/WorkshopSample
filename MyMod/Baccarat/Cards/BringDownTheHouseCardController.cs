namespace Cauldron.Baccarat
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Handelabra;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class BringDownTheHouseCardController : CardController
    {
        #region Constructors

        public BringDownTheHouseCardController(Card card, TurnTakerController turnTakerController) : base(card, turnTakerController) { }

        #endregion Constructors

        #region Methods

        public override IEnumerator Play()
        {
            LinqCardCriteria cardCriteria = new LinqCardCriteria((Card c) => TwoOrMoreCopiesInTrash(c), "two cards with the same name", true, false, null, null, false);
            int maxNumberOfPairs = (from c in base.TurnTaker.Trash.Cards
                                    where TwoOrMoreCopiesInTrash(c)
                                    select c).Count<Card>() / 2;
            List<MoveCardDestination> moveCardDestination = new List<MoveCardDestination>();
            moveCardDestination.Add(new MoveCardDestination(base.TurnTaker.Deck, true, false, false));
            List<SelectCardDecision> storedSelectResults = new List<SelectCardDecision>();
            List<MoveCardAction> storedMoveResults = new List<MoveCardAction>();
            int X = 0;

            //Shuffle any number of pairs of cards with the same name from your trash into your deck.
            //move first card of pairs
            IEnumerator coroutine = base.GameController.SelectCardsFromLocationAndMoveThem(this.DecisionMaker, base.TurnTaker.Trash, new int?(0), maxNumberOfPairs, cardCriteria, moveCardDestination, false, false, false, true, storedSelectResults, storedMoveResults, false, false, false, this.TurnTaker, false, false, null, null, base.GetCardSource(null));
            if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(coroutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(coroutine);
            }
            //move second card of pairs to deck
            foreach(SelectCardDecision cardDecision in storedSelectResults)
            {
                coroutine = base.GameController.SelectCardFromLocationAndMoveIt(this.DecisionMaker, base.TurnTaker.Trash, new LinqCardCriteria((Card c) => c.Identifier == cardDecision.SelectedCard.Identifier, "second card with the same name", true, false, null, null, false), moveCardDestination, false, true, true, true, null, false, true, null, false, true, null, null, base.GetCardSource(null));
                if (base.UseUnityCoroutines)
                {
                    yield return base.GameController.StartCoroutine(coroutine);
                }
                else
                {
                    base.GameController.ExhaustCoroutine(coroutine);
                }
                X++;
            }

            //You may destroy up to X ongoing or environment cards, where X is the number of pairs you shuffled this way.
            coroutine = base.GameController.SelectAndDestroyCards(this.HeroTurnTakerController, new LinqCardCriteria((Card c) => c.IsOngoing || c.IsEnvironment), X, true, null, null, null, null, false, this.CharacterCard, true, null, base.GetCardSource(null));
            if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(coroutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(coroutine);
            }
            yield break;
        }

        private bool TwoOrMoreCopiesInTrash(Card c)
        {
            int num = (from card in base.TurnTaker.Trash.Cards
                       where card.Identifier == c.Identifier
                       select card).Count<Card>();
            return num >= 2;

        }

        #endregion Methods
    }
}