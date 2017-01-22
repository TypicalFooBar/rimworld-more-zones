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
    }
}
