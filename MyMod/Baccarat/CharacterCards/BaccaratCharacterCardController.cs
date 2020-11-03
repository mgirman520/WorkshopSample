namespace Cauldron.Baccarat
{
    using System;
    using System.Collections;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class BaccaratCharacterCardController : HeroCharacterCardController
    {
        #region Constructors

        public BaccaratCharacterCardController(Card card, TurnTakerController turnTakercontroller) : base(card, turnTakercontroller)
        {

        }

        #endregion Constructors

        #region Properties

        private ITrigger ReduceTrigger
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public override IEnumerator UseIncapacitatedAbility(int index)
        {
            switch (index)
            {
                case 0:
                    {
                        //Put 2 cards from a trash on the bottom of their deck.
                        IEnumerator coroutine = base.GameController.SelectCardsFromLocationAndMoveThem(this.HeroTurnTakerController, base.TurnTaker.Trash, new int?(0), 2, null, this.TurnTaker.Trash, false, true, true, false, null, null, true, false, false, this.TurnTaker, false, true, null, null, base.GetCardSource(null));
                        if (base.UseUnityCoroutines)
                        {
                            yield return base.GameController.StartCoroutine(coroutine);
                        }
                        else
                        {
                            base.GameController.ExhaustCoroutine(coroutine);
                        }
                        break;
                    }
                case 1:
                    {
                        //Increase the next damage dealt by a hero target by 2.
                        if (base.UseUnityCoroutines)
                        {
                            yield return base.GameController.StartCoroutine(coroutine2);
                        }
                        else
                        {
                            base.GameController.ExhaustCoroutine(coroutine2);
                        }
                        break;
                    }
                case 2:
                    {
                        //Each hero character may deal themselves 3 toxic damage to use a power now.
                        if (base.UseUnityCoroutines)
                        {
                            yield return base.GameController.StartCoroutine(coroutine3);
                        }
                        else
                        {
                            base.GameController.ExhaustCoroutine(coroutine3);
                        }
                        break;
                    }
            }
            yield break;
        }

        public override IEnumerator UsePower(int index = 0)
        {
            //Discard the top card of your deck or put up to 2 trick cards with the same name from your trash into play.
        }

        #endregion Methods
    }
}