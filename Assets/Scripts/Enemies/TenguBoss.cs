using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Enemies
{
    class TenguBoss : Enemy
    {
        [SerializeField]
        private Animator anim;
        [SerializeField]
        private SkinnedMeshRenderer[] body;
        [SerializeField]
        private GameObject bullet;
        [SerializeField]
        private Vector3 holdPos;
        [SerializeField]
        private Enemy[] minions;
        [SerializeField]
        private Weapons.Hitbox meleeHitbox;
        [SerializeField]
        private GameObject meleeEffect;
        [SerializeField]
        private Util.SoundPlayer sfx;

        private Vector3 TeleportPos;
        private GameObject player;
        private int state, prevState;
        private bool doOnce, moveToHold, moveFailed;
        private float waitTime, moveFailedWait;
        private Enemy[] summmoned;

        private TenguBossStateMachine machine;

        protected override void Initialize()
        {
            player = FindObjectOfType<Player.Player>().gameObject;
            state = 0;
            prevState = 0;
            waitTime = Random.Range(0, 2);
            doOnce = false;
            moveToHold = false;
            moveFailed = false;
            moveFailedWait = 1f;
            machine = new TenguBossStateMachine();
            summmoned = new Enemy[3];
        }

        protected override void RunAI()
        {
            if (state != prevState)
            {
                doOnce = false;
                prevState = state;
                waitTime = Random.Range(0, 2);
                anim.SetInteger("state", state);
            }
            if (animDone)
            {
                if (moveToHold)
                {
                    transform.position = holdPos;
                    moveToHold = false;
                }
                if(state == (int)TenguBossStateMachine.State.Move)
                    doOnce = false;
            }            
            if(moveFailed)
            {
                if ((moveFailedWait -= Time.deltaTime) < 0)
                {
                    moveFailedWait = 1f;
                    moveFailed = false;
                }
            }
            bool full = true;
            foreach (Enemy e in summmoned)
                if (e != null)
                    full = false;
            state = machine.Run(animDone, waitTime < 0, moveFailed, full);

            switch (state)
            {
                case (int)TenguBossStateMachine.State.Intro: Intro(); break;
                case (int)TenguBossStateMachine.State.Wait: Wait(); break;
                case (int)TenguBossStateMachine.State.Move: Move(); break;
                case (int)TenguBossStateMachine.State.TeleportPrep: TeleportPrep(); break;
                case (int)TenguBossStateMachine.State.WaitToAppear: WaitToAppear(); break;
                case (int)TenguBossStateMachine.State.Attack: Attack(); break;
                case (int)TenguBossStateMachine.State.Return: Return(); break;
                case (int)TenguBossStateMachine.State.Tornado: Tornado(); break;
                case (int)TenguBossStateMachine.State.Summon: Summon(); break;
            }
        }

        protected override void Render(bool render)
        {
            foreach (SkinnedMeshRenderer b in body)
                b.enabled = render;
        }

        private void Intro()
        {
        }

        private void Wait()
        {
        }

        private void Move()
        {
            if(!doOnce)
            {
                doOnce = true;
                GameObject[] tiles = GameObject.FindGameObjectsWithTag("Blue");
                List<Grid.GridNode> usableTiles = new List<Grid.GridNode>();
                Grid.GridNode n;
                foreach (GameObject g in tiles)
                {
                    n = g.GetComponent<Grid.GridNode>();
                    if (!n.Occupied)
                        usableTiles.Add(n);
                }
                int tile = usableTiles.Count > 0 ? Random.Range(0, usableTiles.Count - 1) : -1;
                if (tile == -1)
                    moveFailed = true;
                else
                {
                    currentNode.clearOccupied();
                    currentNode = usableTiles[tile];
                    currentNode.Owner = (this);
                    transform.position = currentNode.transform.position;
                }
            }
        }

        private void TeleportPrep()
        {
            if(!doOnce)
            {
                doOnce = true;
                moveToHold = true;
            }
        }

        private void WaitToAppear()
        {
            waitTime -= Time.deltaTime;
            if(!doOnce && waitTime < .1f)
            {
                doOnce = true;
                TeleportPos = player.transform.position;
            }
        }

        private void Attack()
        {
            if (!doOnce)
            {
                doOnce = true;
                transform.position = new Vector3(TeleportPos.x, TeleportPos.y, TeleportPos.z + 2f);
                Weapons.Hitbox h = Instantiate(meleeHitbox).GetComponent<Weapons.Hitbox>();
                h.transform.position = TeleportPos;
                GameObject g = Instantiate(meleeEffect);
                g.transform.position = TeleportPos;
                sfx.PlaySong(0);
            }
        }

        private void Return()
        {
            if(!doOnce)
            {
                doOnce = true;
                transform.position = currentNode.transform.position;
            }
        }

        private void Tornado()
        {
            if (!doOnce)
            {
                doOnce = true;
                Weapons.Hitbox b = Instantiate(bullet).GetComponent<Weapons.Hitbox>();
                b.transform.position = currentNode.Left.transform.position;
                b.CurrentNode = currentNode.Left;
                sfx.PlaySong(1);
            }
        }

        private void Summon()
        {
            if (!doOnce)
            {
                doOnce = true;
                doOnce = true;
                GameObject[] tiles = GameObject.FindGameObjectsWithTag("Blue");
                List<Grid.GridNode> usableTiles = new List<Grid.GridNode>();
                Grid.GridNode n;
                foreach (GameObject g in tiles)
                {
                    n = g.GetComponent<Grid.GridNode>();
                    if (!n.Occupied)
                        usableTiles.Add(n);
                }
                int tile = usableTiles.Count > 0 ? Random.Range(0, usableTiles.Count - 1) : -1;
                if (tile == -1)
                    moveFailed = true;
                else
                {
                    Enemy minion = Instantiate(minions[Random.Range(0, 3)]).GetComponent<Enemy>();
                    minion.CurrentNode = usableTiles[tile];
                    minion.transform.position = minion.CurrentNode.transform.position;
                    if (minions[0] == null)
                        minions[0] = minion;
                    else if (minions[1] == null)
                        minions[1] = minion;
                    else if (minions[2] == null)
                        minions[2] = minion;
                    sfx.PlaySong(2);
                }
            }
        }
    }
}
