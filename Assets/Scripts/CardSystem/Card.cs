using System;
using UnityEngine;
using Assets.Scripts.Util;
using Assets.Scripts.CardSystem.Actions;

namespace Assets.Scripts.CardSystem
{
    public class Card
    {
        private UnityEngine.UI.Image image;
        private Enums.CardTypes type;
        private Actions.Action action;
        private string name;
        private string description;

        public Enums.CardTypes Type
        {
            get { return type; }
        }

        public Actions.Action Action
        {
            get { return action; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        public Card(string name, string type, int range, int damage, string actionType, Weapons.Hitbox actionHitBox, string description)
        {
            this.name = name;
            SetType(type);
            SetAction(actionType, range, damage, actionHitBox);
            this.description = description;
        }

        private void SetType(string type)
        {
            try
            {
                Enums.CardTypes cardType = (Enums.CardTypes)Enum.Parse(typeof(Enums.CardTypes), type);
                if (Enum.IsDefined(typeof(Enums.CardTypes), cardType) | cardType.ToString().Contains(","))
                    this.type = cardType;
                else
                {
                    this.type = Enums.CardTypes.Error;
                    Debug.LogError("Unable to resolve " + type + " to a type.  Setting " + name + " to type Error.");
                }
            }
            catch (ArgumentException)
            {
                this.type = Enums.CardTypes.Error;
                Debug.LogError("Unable to resolve " + type + " to a type.  Setting " + name + " to type Error.");
            }
        }

        private void SetAction(string actionType, int range, int damage, Weapons.Hitbox actionHitBox)
        {
            try
            {
                action = (Actions.Action)Activator.CreateInstance(null, "Assets.Scripts.CardSystem.Actions." + actionType).Unwrap();
                action.Range = range;
                action.Damage = damage;
                action.HitBox = actionHitBox;
            }
            catch (Exception e)
            {
                action = new Error();
                Debug.LogError(e.Message + ": for " + actionType + ".  Setting " + name + "'s action to Error.");
            }
        }
    }
}
