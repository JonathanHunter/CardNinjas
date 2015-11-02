using UnityEngine;
using Assets.Scripts.Player;

namespace Assets.Scripts.CardSystem.Actions
{
    class MeleeVertical : Action
    {
        public override void useCard(Character actor)
        {
            if (actor.Direction == Util.Enums.Direction.Left)
            {
                if (actor.CurrentNode.panelExists(Util.Enums.Direction.Left))
                    spawnObjectUsingPrefabAsModel(damage, -1, .2f, false, Util.Enums.Direction.None, 0, 0, true, actor.CurrentNode.Left, actor);
                if (range > 1 && actor.CurrentNode.Left.panelExists(Util.Enums.Direction.Down))
                    spawnObjectUsingPrefabAsModel(damage, -1, .2f, false, Util.Enums.Direction.None, 0, 0, true, actor.CurrentNode.Left.Left, actor);
                if (range > 2 && actor.CurrentNode.Left.panelExists(Util.Enums.Direction.Up))
                    spawnObjectUsingPrefabAsModel(damage, -1, .2f, false, Util.Enums.Direction.None, 0, 0, true, actor.CurrentNode.Left.Left.Left, actor);
            }

            if (actor.Direction == Util.Enums.Direction.Right)
            {
                if (actor.CurrentNode.panelExists(Util.Enums.Direction.Right))
                    spawnObjectUsingPrefabAsModel(damage, -1, .2f, false, Util.Enums.Direction.None, 0, 0, true, actor.CurrentNode.Right, actor);
                if (range > 1 && actor.CurrentNode.Right.panelExists(Util.Enums.Direction.Down))
                    spawnObjectUsingPrefabAsModel(damage, -1, .2f, false, Util.Enums.Direction.None, 0, 0, true, actor.CurrentNode.Right.Right, actor);
                if (range > 2 && actor.CurrentNode.Right.panelExists(Util.Enums.Direction.Up))
                    spawnObjectUsingPrefabAsModel(damage, -1, .2f, false, Util.Enums.Direction.None, 0, 0, true, actor.CurrentNode.Right.Right.Right, actor);
            }
        }
    }
}
