using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class TenguBossStateMachine
    {
        public enum State
        {
            Intro = 0, Wait, Move, TeleportPrep, WaitToAppear, Attack, Return, Tornado, Summon
        };

        private State currState;
        private double hold;
        private int moveCount;

        public TenguBossStateMachine()
        {
            currState = 0;
            hold = 0;
            moveCount = 0;
        }

        public int Run(bool animDone, bool waitTime, bool moveFailed, bool full)
        {
            switch (currState)
            {
                case State.Intro: currState = Intro(animDone); break;
                case State.Wait: currState = Wait(moveFailed, full); break;
                case State.Move: currState = Move(animDone, moveFailed); break;
                case State.TeleportPrep: currState = TeleportPrep(animDone); break;
                case State.WaitToAppear: currState = WaitToAppear(animDone, waitTime); break;
                case State.Attack: currState = Attack(animDone); break;
                case State.Return: currState = Return(animDone); break;
                case State.Tornado: currState = Tornado(animDone); break;
                case State.Summon: currState = Summon(animDone); break;
            }
            return (int)currState;
        }

        private State Intro(bool animDone)
        {
            if (animDone)
                return State.Wait;
            return State.Intro;
        }

        private State Wait(bool moveFailed, bool full)
        {
            hold += Time.deltaTime;
            if (hold > 1.5f)
            {
                hold = 0;
                float r = Random.Range(0f, 1f);
                if (r < .15f && !full)
                    return State.Summon;
                if (r < .40f)
                    return State.Tornado;
                if (r < .65f || moveFailed)
                    return State.TeleportPrep;
                return State.Move;
            }
            return State.Wait;
        }

        private State Move(bool animDone, bool moveFailed)
        {
            if (moveFailed)
                return State.Wait;
            if (animDone)
            {
                float r = Random.Range(0, 1);
                if (r < .25f && moveCount < 3)
                {
                    moveCount++;
                    return State.Move;
                }
                moveCount = 0;
                return State.Wait;
            }
            return State.Move;
        }

        private State TeleportPrep(bool animDone)
        {
            if (animDone)
                return State.WaitToAppear;
            return State.TeleportPrep;
        }

        private State WaitToAppear(bool animDone, bool waitTime)
        {
            if (waitTime)
                return State.Attack;
            return State.WaitToAppear;
        }

        private State Attack(bool animDone)
        {
            if (animDone)
                return State.Return;
            return State.Attack;
        }

        private State Return(bool animDone)
        {
            if (animDone)
                return State.Wait;
            return State.Return;
        }

        private State Tornado(bool animDone)
        {
            if (animDone)
                return State.Wait;
            return State.Tornado;
        }

        private State Summon(bool animDone)
        {
            if (animDone)
                return State.Wait;
            return State.Summon;
        }
    }
}
