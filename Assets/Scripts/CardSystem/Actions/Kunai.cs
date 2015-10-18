﻿using UnityEngine;
using Assets.Scripts.Player;

namespace Assets.Scripts.CardSystem.Actions
{
    class Kunai : Action
    {
        public override void useCard(Character actor)
        {
            if (actor.Direction == Util.Enums.Direction.Left)
            {
                if (actor.CurrentNode.panelExists(Util.Enums.Direction.Left))
                    spawnObjectUsingPrefabAsModel(damage, 9, .2f, false, Util.Enums.Direction.Left, 10, 0, true, actor.CurrentNode.Left);
                if (range > 1 && actor.CurrentNode.Left.panelExists(Util.Enums.Direction.Down))
                    spawnObjectUsingPrefabAsModel(damage, 9, .2f, false, Util.Enums.Direction.Left, 10, 0, true, actor.CurrentNode.Left.Down);
                if (range > 2 && actor.CurrentNode.Left.panelExists(Util.Enums.Direction.Up))
                    spawnObjectUsingPrefabAsModel(damage, 9, .2f, false, Util.Enums.Direction.Left, 10, 0, true, actor.CurrentNode.Left.Up);
            }

            if (actor.Direction == Util.Enums.Direction.Right)
            {
                if (actor.CurrentNode.panelExists(Util.Enums.Direction.Right))
                    spawnObjectUsingPrefabAsModel(damage, 9, .2f, false, Util.Enums.Direction.Right, 10, 0, true, actor.CurrentNode.Right);
                if (range > 1 && actor.CurrentNode.Right.panelExists(Util.Enums.Direction.Down))
                    spawnObjectUsingPrefabAsModel(damage, 9, .2f, false, Util.Enums.Direction.Right, 10, 0, true, actor.CurrentNode.Right.Down);
                if (range > 2 && actor.CurrentNode.Right.panelExists(Util.Enums.Direction.Up))
                    spawnObjectUsingPrefabAsModel(damage, 9, .2f, false, Util.Enums.Direction.Right, 10, 0, true, actor.CurrentNode.Right.Up);
            }
        }
    }
}
