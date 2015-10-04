using Assets.Scripts.Util;

namespace Assets.Scripts.Player
{
    /* This file controls all of the transitions between states*/
    class PlayerStateMachine
    {
        private delegate Enums.PlayerState machine(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty);//function pointer
        private machine[] getNextState;//array of function pointers
        private Enums.PlayerState currState;
        private static float hold = 0;//used for delays
        private static bool die = false;

        public PlayerStateMachine()
        {
            currState = Enums.PlayerState.Idle;
            //fill array with functions
            getNextState = new machine[] { Idle, MoveBegining, MoveEnding, Hit, Dead, BasicAttack, Sword };
        }

        public Enums.PlayerState update(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty)
        {
            currState = getNextState[((int)currState)](hit, animDone, direction, type, handEmpty);//gets te next Enums.PlayerState
            return currState;
        }


        //The following methods control when and how you can transition between states

        private static Enums.PlayerState Idle(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (!handEmpty && CustomInput.BoolFreshPress(CustomInput.UserInput.UseCard))
            {
                //switch off of type
                return Enums.PlayerState.Sword;
            }
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Attack))
                return Enums.PlayerState.BasicAttack;
            if (direction != Enums.Direction.None)
                return Enums.PlayerState.MoveBegining;
            return Enums.PlayerState.Idle;
        }

        private static Enums.PlayerState MoveBegining(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.MoveEnding;
            return Enums.PlayerState.MoveBegining;
        }

        private static Enums.PlayerState MoveEnding(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.MoveEnding;
        }

        private static Enums.PlayerState BasicAttack(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (CustomInput.BoolFreshPress(CustomInput.UserInput.Attack))
                hold = 0;
            hold += UnityEngine.Time.deltaTime;
            if (hold > .5f)
            {
                hold = 0;
                return Enums.PlayerState.Idle;
            }
            return Enums.PlayerState.BasicAttack;
        }

        private static Enums.PlayerState Sword(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty)
        {
            if (hit)
                return Enums.PlayerState.Hit;
            if (animDone)
                return Enums.PlayerState.Idle;
            return Enums.PlayerState.Sword;
        }

        private static Enums.PlayerState Hit(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty)
        {
            hold += UnityEngine.Time.deltaTime;
            if (hold > .4f)
            {
                hold = 0;
                if (die)
                    return Enums.PlayerState.Dead;
                return Enums.PlayerState.Idle;
            }
            return Enums.PlayerState.Hit;
        }

        //this is used to prevent the player character from doing any thing while dead
        private static Enums.PlayerState Dead(bool hit, bool animDone, Enums.Direction direction, Enums.CardTypes type, bool handEmpty)
        {
            return Enums.PlayerState.Dead;
        }

        internal void Die()
        {
            die = true;
        }

        internal void Revive()
        {
            currState = Enums.PlayerState.Idle;
            die = false;
        }
    }
}
