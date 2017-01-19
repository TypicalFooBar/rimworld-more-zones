using Verse;
using HugsLib.Source.Detour;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.Collections;

namespace MoreZones
{
    public static class Utilities
    {
        /// <summary>
        /// The root directory of this mod.
        /// </summary>
        public static string ModRootDir
        {
            get
            {
                foreach (ModContentPack runningMod in LoadedModManager.RunningMods)
                {
                    if (runningMod.Name == "MoreZones")
                        return runningMod.RootDir;
                }

                return null;
            }
        }

		private static Zone_Stockpile currentZoneStockpile = null;

		[DetourMethod(typeof(Zone_Stockpile), "GetGizmos")]
		private static IEnumerable<Gizmo> GetGizmos(this Zone_Stockpile self) {
			currentZoneStockpile = self;

			List<Gizmo> gizmos = new List<Gizmo> ();

			Command_Action command = new Command_Action ();
			command.icon = ContentFinder<Texture2D>.Get ("UnSkilled", true);
			command.action = new Action (DoThing);
			command.defaultLabel = "Test";
			command.defaultDesc = "Test description";

			gizmos.Add (command);

			//return self.GetGizmos ().Concat (new Gizmo[] { command });

			return gizmos;
		}

		private static void DoThing()
		{
			Log.Message ("Zone: " + currentZoneStockpile.label);
		}
    }
}
