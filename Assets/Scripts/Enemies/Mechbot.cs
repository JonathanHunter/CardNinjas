using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class Mechbot : Enemy
    {
        [SerializeField]
        private GameObject bullet;
        [SerializeField]
        private SkinnedMeshRenderer[] body;
        [SerializeField]
        private Util.SoundPlayer sfx;

        private Player.Player player;
        private float turn = 0;
        public Animator mechAnima;

        protected override void Initialize()
        {
            player = FindObjectOfType<Player.Player>();
            mechAnima.GetComponent<Animator>();
        }

        protected override void RunAI()
        {
            //We change turns each second
            turn += Time.deltaTime;
            if (!hit)
            {
                if (turn > 1f)
                {
                    turn = 0;
                    //If player is above us
                    if (player.CurrentNode.Position.x < currentNode.Position.x)
                    {
                        //Check if we can move up.
                        if (currentNode.panelNotDestroyed(Util.Enums.Direction.Up) && !currentNode.Up.Occupied)
                        {
                            mechAnima.SetTrigger("Wait");
                            currentNode.clearOccupied();//Say we aren't here
                            currentNode = currentNode.Up;//Say we're there
                            currentNode.Owner = (this);//Tell the place we own it.
                        }
                    }
                    //If player is above us
                    else if (player.CurrentNode.Position.x > currentNode.Position.x)
                    {
                        //Check if we can move up.
                        if (currentNode.panelNotDestroyed(Util.Enums.Direction.Down) && !currentNode.Down.Occupied)
                        {
                            mechAnima.SetTrigger("Wait");
                            currentNode.clearOccupied();//Say we aren't here
                            currentNode = currentNode.Down;//Say we're there
                            currentNode.Owner = (this);//Tell the place we own it.
                        }
                    }
                    //If they are in front of us, ATTACK!.
                    else
                    {
                        mechAnima.SetTrigger("Fire");
                        Weapons.Hitbox b = Instantiate(bullet).GetComponent<Weapons.Hitbox>();
                        b.transform.position = currentNode.Left.transform.position;
                        b.CurrentNode = currentNode.Left;
                        sfx.PlaySong(0);
                    }
                }
            }
            else
            {
                mechAnima.SetTrigger("Hurt");
                turn = 0;
            }

            transform.position = currentNode.transform.position;
        }

        protected override void Render(bool render)
        {
            foreach (SkinnedMeshRenderer b in body)
                b.enabled = render;
        }
    }
}
