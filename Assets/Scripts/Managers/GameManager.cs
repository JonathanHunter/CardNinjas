using UnityEngine;
using Assets.Scripts.Util;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private static Enums.GameStates state = Enums.GameStates.CardSelection;

        void Awake()
        {
            if(instance == null)
            {
                DontDestroyOnLoad(this.gameObject);
                instance = this;
            }
            else if(this != instance)
            {
                Destroy(this.gameObject);
            }
        }

        void Update()
        {
            if(CustomInput.BoolFreshPress(CustomInput.UserInput.SelectCards))
            {
                GameObject.Find("Card Selection Canvas").GetComponent<UI.CardSelector>().EnableCanvas();
            }
        }

        private void CardSelectorStateEnable()
        {
            state = Enums.GameStates.CardSelection;
        }
        private void CardSelectorStateDisable()
        {
            state = Enums.GameStates.Battle;
        }

        public static Enums.GameStates State
        {
            get { return state; }
        }

        void OnEnable()
        {
            UI.CardSelector.CardSelectorEnabled += CardSelectorStateEnable;
            UI.CardSelector.CardSelectorDisabled += CardSelectorStateDisable;
        }
        void OnDisable()
        {
            UI.CardSelector.CardSelectorEnabled -= CardSelectorStateEnable;
            UI.CardSelector.CardSelectorDisabled -= CardSelectorStateDisable;
        }
    }
}
