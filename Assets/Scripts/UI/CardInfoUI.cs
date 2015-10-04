using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.UI
{
    public class CardInfoUI : MonoBehaviour
    {
        private Text nameText, typeText, descriptionText, rangeText, damageText;

        void OnEnable()
        {
            Player.Player.NewSelect += UpdateCardInfo;
        }
        void OnDisable()
        {
            Player.Player.NewSelect -= UpdateCardInfo;
        }

        void Awake()
        {
            nameText = GameObject.Find("Name Text").GetComponent<Text>();
            typeText = GameObject.Find("Type Text").GetComponent<Text>();
            descriptionText = GameObject.Find("Description Text").GetComponent<Text>();
            rangeText = GameObject.Find("Range Text").GetComponent<Text>();
            damageText = GameObject.Find("Damage Text").GetComponent<Text>();

            HideDescription();
        }

        private void UpdateCardInfo(CardSystem.Card card)
        {
            if (card == null)
            {
                nameText.text = "Name: ";
                typeText.text = "Type: ";
                descriptionText.text = "Description: ";
                rangeText.text = "Range: ";
                damageText.text = "Damage: ";
            }
            else
            {
                nameText.text = "Name: " + card.Name;
                typeText.text = "Type: " + card.Type.ToString();
                descriptionText.text = "Description: " + card.Description;
                rangeText.text = "Range: " + card.Action.Range.ToString();
                damageText.text = "Damage: " + card.Action.Damage.ToString();
            }
        }

        public void DisplayDescription()
        {
            if(!descriptionText.IsActive()) descriptionText.gameObject.SetActive(true);
        }

        public void HideDescription()
        {
            if (descriptionText.IsActive()) descriptionText.gameObject.SetActive(false);
        }
    }
}
