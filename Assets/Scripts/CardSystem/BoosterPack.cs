using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.CardSystem
{
    class BoosterPack : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.UI.Image packImage;
        [SerializeField]
        private Vector2 cardRange;
        [SerializeField]
        private int packSize;

        public UnityEngine.UI.Image PackImage
        {
            get { return packImage; }
        }

        public List<Card> GetCards()
        {
            List<Card> cards = FindObjectOfType<CardList>().Cards;
            List<Card> pack = new List<Card>();
            Random.seed = System.DateTime.Today.Millisecond;
            for (int i = 0; i < packSize; i++)
            {
                pack.Add(cards[Random.Range((int)cardRange.x, (int)cardRange.y)]);
            }
            return pack;
        }
    }
}
