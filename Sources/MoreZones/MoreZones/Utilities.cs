using Verse;

namespace MoreZones
{
    public class Utilities
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
