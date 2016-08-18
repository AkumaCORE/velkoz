using System;
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
using static RoninVelkoz.SpellsManager;
using static RoninVelkoz.Menus;

namespace RoninVelkoz.Modes
{
    internal class ModeManager
    {
        /// <summary>
        /// Create the event on tick

        public static void InitializeModes()
        {
            Game.OnTick += Game_OnTick;
        }

      

        private static void Game_OnTick(EventArgs args)
        {
            var orbMode = Orbwalker.ActiveModesFlags;
            var playerMana = Player.Instance.ManaPercent;
            if (UltimateFollower && Program.Champion.HasBuff("VelkozR"))
                Program.UltFollowMode();
            if (RoninVelkoz.Menus.StackMode)
                Program.StackMode();
            Active.Execute();

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Combo.Execute();
            }
            
            if (MiscMenu["MCastQ"].Cast<KeyBind>().CurrentValue)
            {
                var minion = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                Player.Instance.ServerPosition, 1100).ToArray();
                var lineFarmLocation = EntityManager.MinionsAndMonsters.GetLineFarmLocation(minion, 60,
                1000, Game.CursorPos.Extend(Player.Instance.ServerPosition, 1000));
                if (Core.GameTickCount - Program.QTime >= 1500 && lineFarmLocation.HitNumber == 0)
                {
                    SpellsManager.Q.Cast(Game.CursorPos);
                }
                else
                {
                    return;
                }
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass) && playerMana > HarassMenu.GetSliderValue("manaSlider"))
            {
                Harass.Execute();
            }

     //       if (orbMode.HasFlag(Orbwalker.ActiveModes.LastHit) && playerMana > LasthitMenu.GetSliderValue("manaSlider"))
    //        {
    //            LastHit.Execute();
    //        }

    //        if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear) && playerMana > LaneClearMenu.GetSliderValue("manaSlider"))
    //        {
   //             LaneClear.Execute();
    //        }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.JungleClear) && playerMana > JungleClearMenu.GetSliderValue("manaSlider"))
            {
                JungleClear.Execute();
            }

  //          if (playerMana > AutoHarassMenu.GetSliderValue("manaSlider") && AutoHarassMenu.GetKeyBindValue("autoHarassKey"))
//            {
 //               AutoHarass.Execute();
  //          }
        }
    }
}
