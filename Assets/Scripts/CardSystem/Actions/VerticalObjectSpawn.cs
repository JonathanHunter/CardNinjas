using UnityEngine;
using Assets.Scripts.Player;

namespace Assets.Scripts.CardSystem.Actions
{
    class VerticalObjectSpawn : Action
    {
        public override void useCard(Character actor)
        {
            Weapons.Hitbox temp = MonoBehaviour.Instantiate(hitbox);
            temp.Damage = damage;
            temp.DeathTime = .2f;
            if (actor.Direction == Util.Enums.Direction.Left)
            {
                temp.transform.position = actor.CurrentNode.Left.transform.position;
                if (range > 1 && actor.CurrentNode.Left.panelExists(Util.Enums.Direction.Down))
                {
                    temp = MonoBehaviour.Instantiate(hitbox);
                    temp.Damage = damage;
                    temp.DeathTime = .2f;
                    temp.transform.position = actor.CurrentNode.Left.Down.transform.position;
                }
                if (range > 2 && actor.CurrentNode.Left.panelExists(Util.Enums.Direction.Up))
                {
                    temp = MonoBehaviour.Instantiate(hitbox);
                    temp.Damage = damage;
                    temp.DeathTime = .2f;
                    temp.transform.position = actor.CurrentNode.Left.Up.transform.position;
                }
            }

            if (actor.Direction == Util.Enums.Direction.Right)
            {
                temp.transform.position = actor.CurrentNode.Right.transform.position;
                if (range > 1 && actor.CurrentNode.Right.panelExists(Util.Enums.Direction.Down))
                {
                    temp = MonoBehaviour.Instantiate(hitbox);
                    temp.Damage = damage;
                    temp.DeathTime = .2f;
                    temp.transform.position = actor.CurrentNode.Right.Down.transform.position;
                }
                if (range > 2 && actor.CurrentNode.Right.panelExists(Util.Enums.Direction.Up))
                {
                    temp = MonoBehaviour.Instantiate(hitbox);
                    temp.Damage = damage;
                    temp.DeathTime = .2f;
                    temp.transform.position = actor.CurrentNode.Right.Up.transform.position;
                }
            }
        }
    }
}
