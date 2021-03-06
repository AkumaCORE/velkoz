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
using static RoninLux.Menus;
using static RoninLux.SpellsManager;

namespace RoninLux.Modes
{
    /// <summary>
    /// This mode will always run
    /// </summary>
    internal class Active
    {

        public static void Execute()
        {

   if (W.IsReady())
            {
                if (Player.Instance.HealthPercent <= 25 && EntityManager.Heroes.Enemies.Count(e => !e.IsDead && W.IsInRange(e)) > 0)
                {
                    if (Player.Instance.HealthPercent <= 25)
                    {
                        return;
                    }
                    else
                    {
                        var firstOrDefault = EntityManager.Heroes.Enemies.FirstOrDefault(e => !e.IsDead && W.IsInRange(e));
                        if (firstOrDefault != null)
                            W.Cast(firstOrDefault.ServerPosition);
                    }
                }

            }

            //Thanks to Mario
            if (KillStealMenu.GetCheckBoxValue("rUse"))
            {
                var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Magical);

                if (rtarget == null) return;

                if (R.IsReady())
                {
                    var passiveDamage = rtarget.HasPassive() ? rtarget.GetPassiveDamage() : 0f;
                    var rDamage = rtarget.GetDamage(SpellSlot.R) + passiveDamage;

                    var predictedHealth = Prediction.Health.GetPrediction(rtarget, R.CastDelay + Game.Ping);

                    if (predictedHealth <= rDamage)
                    {
                        var pred = R.GetPrediction(rtarget);
                        if (pred.HitChancePercent >= 90)
                        {
                            R.Cast(rtarget);
                        }
                    }
                }
            }// END THANKS

            if (!R.IsReady())
            {
                return;
            }

            var stealBlue = KillStealMenu.GetCheckBoxValue("StealBlueBuff");
            var stealRed = KillStealMenu.GetCheckBoxValue("StealRedBuff");
            var stealDragon = KillStealMenu.GetCheckBoxValue("StealDragon");
            var stealBaron = KillStealMenu.GetCheckBoxValue("StealBaron");
            var stealBuffMode = KillStealMenu.GetCheckBoxValue("StealBuffMode");

            if (stealBaron)
            {
                var baron = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(x => x.CharData.BaseSkinName.Equals("SRU_Baron"));
                var predictedHealth = Prediction.Health.GetPrediction(baron, R.CastDelay + Game.Ping);
                if (baron != null)
                {
                    //var healthPred = HealthPrediction.GetHealthPrediction(baron, (int)(R.Delay * 1000) + Game.Ping / 2);
                    var rDamage = baron.GetDamage(SpellSlot.R);
                    if (predictedHealth <= rDamage)
                    {
                        R.Cast(baron);
                    }
                }
            }

            if (stealDragon)
            {
                var dragon =
                    ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(x => x.CharData.BaseSkinName.Equals("SRU_Dragon"));

                if (dragon != null)
                {
                    var predictedHealth = Prediction.Health.GetPrediction(dragon, R.CastDelay + Game.Ping);
                    var rDamage = dragon.GetDamage(SpellSlot.R);

                    if (predictedHealth <= rDamage)
                    {
                        R.Cast(dragon);
                    }
                }
            }

        }
    }
}