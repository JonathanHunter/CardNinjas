using UnityEngine;
using System.Collections.Generic;
namespace Assets.Scripts.CardSystem
{
    public class Deck
    {
        public const int DRAW_SIZE = 8;
        private List<Card> deck;

        public Deck(List<Card> deck)
        {
            this.deck = deck;
            //Random.seed = System.DateTime.Today.Millisecond;
            Card temp;
            for (int i = 0; i < this.deck.Count; i++)
            {
                int r = i + (int)(Random.Range(0f, 1f) * (this.deck.Count - i));
                temp = this.deck[r];
                this.deck[r] = this.deck[i];
                this.deck[i] = temp;
            }
        }

        public List<Card> DrawHand()
        {
            if (deck.Count == 0)
                return null;
            List<Card> selection = new List<Card>();
            int limitedDrawSize = deck.Count > DRAW_SIZE ? DRAW_SIZE : deck.Count;
            for (int i = 0; i < limitedDrawSize; i++)
                selection.Add(deck[i]);
            deck.RemoveRange(0, limitedDrawSize);
            return selection;
        }

        public void ReturnUsedCards(List<Card> cards)
        {
            deck.InsertRange(0, cards);
        }

        public List<Card> GetDeck
        {
            get { return deck; }
        }
    }
}
