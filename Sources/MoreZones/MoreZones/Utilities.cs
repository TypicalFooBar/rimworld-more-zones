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

		public static Command_Action CreateCommandAction(string label, string description, string icon, Action action, KeyBindingDef keyBindingDef = null)
		{
			Command_Action command = new Command_Action ();
			command.defaultLabel = label;
			command.defaultDesc = description;
			command.icon = ContentFinder<Texture2D>.Get (icon, true);
			command.action = action;

			if (keyBindingDef != null)
			{
				command.hotKey = keyBindingDef;
			}

			return command;
		}

		//public static Command_Toggle

		private static Zone_Stockpile currentZoneStockpile = null;
		//private static ZoneManager zoneManager = null;

		[DetourMethod(typeof(Zone_Stockpile), "GetGizmos")]
		private static IEnumerable<Gizmo> GetGizmos(this Zone_Stockpile self)
		{
			currentZoneStockpile = self;

			List<Gizmo> gizmos = new List<Gizmo> ();

			// Add rename command
			Command_Action renameCommandAction = Utilities.CreateCommandAction (
				"Rename",
				"Rename this zone.",
				"UI/PasteSettings",
				new Action(RenameZone),
				KeyBindingDef.Named ("B")
			);

//			Command_Action
//			command.icon = ContentFinder<Texture2D>.Get ("UnSkilled", true);
//			command.action = new Action (DoThing);
//			command.defaultLabel = "Test";
//			command.defaultDesc = "Test description";

			gizmos.Add (renameCommandAction);

			return gizmos;
		}

		private static void RenameZone()
		{
			//Log.Message ("Zone: " + currentZoneStockpile.label);

			//Find.WindowStack.Add (new Dialog_RenameZone (currentZoneStockpile));

			foreach (Texture2D texture in ContentFinder<Texture2D>.GetAllInFolder ("UI")) {
				Log.Message (texture.name);
			}
		}
    }
}
