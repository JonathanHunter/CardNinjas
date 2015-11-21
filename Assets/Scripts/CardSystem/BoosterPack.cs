using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.CardSystem
{
    class BoosterPack
    {
        private Sprite packImage;
        private int packSize;
        private List<Card> cards;

        public Sprite PackImage
        {
            get { return packImage; }
        }

        public BoosterPack(List<Card> cards, int packSize, Sprite packImage)
        {
            this.cards = cards;
            this.packSize = packSize;
            this.packImage = packImage;
        }

        public List<Card> GetCards()
        {
            List<Card> pack = new List<Card>();
            for (int i = 0; i < packSize; i++)
            {
                pack.Add(cards[Random.Range(0, cards.Count)]);
            }
            return pack;
        }
    }
}
