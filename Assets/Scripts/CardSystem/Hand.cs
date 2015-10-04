using System.Collections.Generic;
using Assets.Scripts.Util;

namespace Assets.Scripts.CardSystem
{
    public class Hand
    {
        private List<Card> hand;

        public List<Card> PlayerHand
        {
            get { return hand; }
            set { hand = value; }
        }

        public Hand()
        {
            hand = new List<Card>();
        }

        public Enums.CardTypes GetCurrentType()
        {
            if (hand == null)
                return Enums.CardTypes.Error;
            return hand.Count > 0 ? hand[0].Type : Enums.CardTypes.Error;
        }

        public Card getCurrent()
        {
            if (hand == null)
                return null;
            return hand.Count > 0 ? hand[0] : null;
        }

        public bool Empty()
        {
            if (hand == null)
                return true;
            return hand.Count == 0;
        }

        public void UseCurrent(Player.Character character)
        {
            if (hand == null)
                return ;
            hand[0].Action.useCard(character);
            hand.RemoveAt(0);
        }
    }
}
