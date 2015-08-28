﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.PowerUps.Behaviors
{
    public class Bouncy : Behavior
    {
        public override void ApplyBuffToPlayer(Player player)
        {
            player.IsBouncy = true;
        }

        public override void RemoveBuffFromPlayer(Player player)
        {
            player.IsBouncy = false;
        }
    }
}