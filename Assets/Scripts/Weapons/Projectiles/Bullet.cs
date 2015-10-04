using UnityEngine;

namespace Assets.Scripts.Weapons.Projectiles
{
    public class Bullet : Hitbox
    {
        [SerializeField]
        private float speed = 10;

        private Util.Enums.Direction direction = Util.Enums.Direction.Right;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Util.Enums.Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        void Update()
        {
            transform.Translate(new Vector3(0,0, direction == Util.Enums.Direction.Left ? -1 : 1) * speed * Time.deltaTime);
            if ((deathTime -= Time.deltaTime) < 0 || dead)
                Destroy(this.gameObject);
        }
    }
}
