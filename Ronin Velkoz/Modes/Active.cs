﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Mario_s_Lib;
using static RoninVelkoz.Menus;
using static RoninVelkoz.SpellsManager;

namespace RoninVelkoz.Modes
{
    /// <summary>
    /// This mode will always run
    /// </summary>
    internal class Active
    {
        /// <summary>
        /// Put in here what you want to do when the mode is running
        /// </summary>
        public static void Execute()
        {
             //var target = TargetManager.GetChampionTarget(SpellManager.R.Range, DamageType.Magical, false, false, SpellManager.RDamage());
             //   if (target != null)
             //       SpellManager.CastR(target);
            //Thanks to Mario
            if (KillStealMenu.GetCheckBoxValue("rUse"))
            {
                var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                if (R.IsReady())
                {
                    var rDamage = rtarget.GetDamage(SpellSlot.R);
                    var target = TargetManager.GetChampionTarget(SpellsManager.R.Range, DamageType.Magical, false, false, SpellsManager.RDamage() * 2);
                    if (target != null)
                        SpellsManager.R.Cast(target);
                        Program.UltFollowMode();
                }
                }// END THANKS
            }
    }
}
