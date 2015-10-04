using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using Assets.Scripts.Player;
using Assets.Scripts.Util;
using Assets.Scripts.CardSystem;

namespace Assets.Scripts.UI
{
    public class CardSelector : MonoBehaviour
    {
        public delegate void CardSelectorAction();
        public static event CardSelectorAction CardSelectorEnabled;
        public static event CardSelectorAction CardSelectorDisabled;

        private static int playerIndex;
        private int numSelections = 0;
        private const int NUM_SELECTIONS = 8;
        private const int MAX_SELECTIONS = 4;
        private const int CHILD_LABEL_INDEX = 1;

        public Player.Player player;
        private Deck deck;
        private Hand selectedCards;
        private Hand selectionOptions;
        private Toggle[] selectionButtons;

        void Start()
        {
            //should find players provided they are named in the fashion: "Player 1" or "Player 42"
            player = GameObject.Find("Player " + ++playerIndex).GetComponent<Player.Player>();

            deck = player.Deck;
            selectionOptions = new Hand();
            selectedCards = new Hand();

            selectionButtons = new Toggle[NUM_SELECTIONS];
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Selection").OrderBy(go => go.name).ToArray();
            for(int i = 0; i < gos.Length; i++)
            {
                selectionButtons[i] = gos[i].GetComponent<Toggle>();
            }
            DrawPossibleSelections();
        }

        void Update()
        {
            if (Managers.GameManager.State == Enums.GameStates.CardSelection)
            {
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.Attack))
                {
                    DrawPossibleSelections();
                }
            }
        }

        private void DrawPossibleSelections()
        {

            selectionOptions.PlayerHand = deck.DrawHand();
            for(int i = 0; i < NUM_SELECTIONS; i++)
            {
                selectionButtons[i].isOn = false;
                selectionButtons[i].interactable = true;

                if (i < selectionOptions.PlayerHand.Count && selectionOptions.PlayerHand[i] != null)
                    selectionButtons[i].transform.GetChild(CHILD_LABEL_INDEX).GetComponent<Text>().text = selectionOptions.PlayerHand[i].Name;
                else
                {
                    selectionButtons[i].transform.GetChild(CHILD_LABEL_INDEX).GetComponent<Text>().text = "<empty>";
                    selectionButtons[i].interactable = false;
                }
            }
        }

        public void SelectCard(int index)
        {
            if(!selectionButtons[index].isOn)
            {
                if(numSelections >= MAX_SELECTIONS)
                {
                    for(int i = 0; i < selectionButtons.Length; i++)
                    {
                        if (!selectionButtons[i].interactable && selectionButtons[i].transform.GetChild(CHILD_LABEL_INDEX).GetComponent<Text>().text != "<empty>") selectionButtons[i].interactable = true;
                    }
                }
                Toggle t = selectionButtons[index];
                ColorBlock cb = t.colors;
                cb.highlightedColor = Color.white;
                cb.normalColor = Color.white;
                t.colors = cb;
                selectedCards.PlayerHand.Remove(selectionOptions.PlayerHand[index]);
                numSelections--;
            }
            else
            {
                Toggle t = selectionButtons[index];
                ColorBlock cb = t.colors;
                cb.normalColor = Color.yellow;
                cb.highlightedColor = Color.yellow;
                t.colors = cb;
                numSelections++;
                selectedCards.PlayerHand.Add(selectionOptions.PlayerHand[index]);
                if (numSelections >= MAX_SELECTIONS)
                {
                    for (int i = 0; i < selectionButtons.Length; i++)
                    {
                        if (selectionButtons[i].interactable && !selectionButtons[i].isOn) selectionButtons[i].interactable = false;
                    }
                }
            }
        }

        public void EnableCanvas()
        {
            selectedCards.PlayerHand.Clear();
            transform.GetComponent<Canvas>().enabled = true;
            DrawPossibleSelections();
            if (CardSelectorEnabled != null) CardSelectorEnabled();
        }

        public void Okay()
        {
            for(int i = 0; i < selectedCards.PlayerHand.Count; i++)
            {
                selectionOptions.PlayerHand.Remove(selectedCards.PlayerHand[i]);
            }
            deck.ReturnUsedCards(selectionOptions.PlayerHand);
            player.AddCardsToHand(selectedCards.PlayerHand);
            transform.GetComponent<Canvas>().enabled = false;
            if (CardSelectorDisabled != null) CardSelectorDisabled();
        }

        void OnLevelWasLoaded(int i)
        {
            playerIndex = 0;
        }
    }
}