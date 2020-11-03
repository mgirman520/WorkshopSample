namespace Cauldron.Baccarat
{
    using System;
    using System.Collections;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class IFoldCardController : CardController
    {
        #region Constructors

        public IFoldCardController(Card card, TurnTakerController turnTakerController) : base(card, turnTakerController) { }

        #endregion Constructors

        #region Methods

        public override IEnumerator Play()
        {
            //Discard your hand...
            IEnumerator coroutine = base.GameController.DiscardHand(this.HeroTurnTakerController, false, null, this.TurnTaker, base.GetCardSource(null));
            if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(coroutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(coroutine);
            }
            //... and draw 3 cards.
            coroutine = base.GameController.DrawCards(this.HeroTurnTakerController, 3, false, false, null, true, null, base.GetCardSource(null));
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

        #endregion Methods
    }
}