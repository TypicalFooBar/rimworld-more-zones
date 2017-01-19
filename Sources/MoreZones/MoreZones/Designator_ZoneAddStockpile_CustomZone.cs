using RimWorld;
using System.IO;
using System.Xml;
using UnityEngine;
using Verse;

namespace MoreZones
{
    public class Designator_ZoneAddStockpile_CustomZone : Designator_ZoneAddStockpile
    {
        /// <summary>
        /// The custom settings that will be used for a zone. Is initialized by an XML file.
        /// </summary>
        protected StorageSettings storageSettings = new StorageSettings();

        /// <summary>
        /// Creates a custome zone creator. All settings for this zone are described in the XML that is passed to it.
        /// </summary>
        /// <param name="xmlPath">The absolute path of the XML document to read.</param>
        public Designator_ZoneAddStockpile_CustomZone(string xmlPath)
        {
            Log.Message("xmlPath: " + xmlPath);

            // Set the preset and icon
            this.preset = StorageSettingsPreset.DumpingStockpile;
            this.icon = ContentFinder<Texture2D>.Get("UI/Designators/ZoneCreate_Stockpile", true);

            // Initialize from the XML file
            this.InitFromXml(xmlPath);
        }

        /// <summary>
        /// Reads the XML document and sets the name, description, and filter that will be used when creating this zone.
        /// </summary>
        /// <param name="xmlPath">The absolute path of the XML document to read.</param>
        private void InitFromXml(string xmlPath)
        {
            // Load the XML file into an XmlDocument
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(File.ReadAllText(xmlPath));

            // Set the label and description
            this.defaultLabel = xmlDocument.SelectSingleNode("zoneData/defaultLabel").InnerText;
            this.defaultDesc = xmlDocument.SelectSingleNode("zoneData/defaultDesc").InnerText;

            // If the description hasn't been set in this file yet, let the user know where the file is so they can edit it
            if (this.defaultDesc == "")
            {
                this.defaultDesc = "To customize this zone, edit the following file: " + xmlPath;
            }

            // Set the storage priority
            string storagePriorityString = xmlDocument.SelectSingleNode("zoneData/settings/priority").InnerText;
            switch (storagePriorityString.ToLower())
            {
                case "unstored":
                    this.storageSettings.Priority = StoragePriority.Unstored;
                    break;
                case "low":
                    this.storageSettings.Priority = StoragePriority.Low;
                    break;
                case "normal":
                    this.storageSettings.Priority = StoragePriority.Normal;
                    break;
                case "preferred":
                    this.storageSettings.Priority = StoragePriority.Preferred;
                    break;
                case "important":
                    this.storageSettings.Priority = StoragePriority.Important;
                    break;
                case "critical":
                    this.storageSettings.Priority = StoragePriority.Critical;
                    break;
                default:
                    this.storageSettings.Priority = StoragePriority.Normal;
                    break;
            }

            // Set the disallowed special filters
            XmlNodeList disallowedSpecialFilters = xmlDocument.SelectNodes("zoneData/settings/filter/disallowedSpecialFilters/li");
            foreach (XmlNode node in disallowedSpecialFilters)
            {
                this.storageSettings.filter.SetAllow(SpecialThingFilterDef.Named(node.InnerText), false);
            }

            // Set the allowed defs
            XmlNodeList allowedDefs = xmlDocument.SelectNodes("zoneData/settings/filter/allowedDefs/li");
            foreach (XmlNode node in allowedDefs)
            {
                this.storageSettings.filter.SetAllow(ThingDef.Named(node.InnerText), true);
            }

            // Set the allowed hit points range
            this.storageSettings.filter.AllowedHitPointsPercents = FloatRange.FromString(xmlDocument.SelectSingleNode("zoneData/settings/filter/allowedHitPointsPercents").InnerText);

            // Set the allowed quality range
            this.storageSettings.filter.AllowedQualityLevels = QualityRange.FromString(xmlDocument.SelectSingleNode("zoneData/settings/filter/allowedQualityLevels").InnerText);
        }

        /// <summary>
        /// Called by the game when the player creates a zone. This is when the custom zone is created with the settings specified in it's XML.
        /// </summary>
        /// <returns>The new custom zone.</returns>
        protected override Zone MakeNewZone()
        {
            // Create the zone, setting the custom settings.
			Zone_Stockpile zone = new Zone_Stockpile(this.preset, null);
            zone.settings.CopyFrom(this.storageSettings);
            zone.label = this.defaultLabel;

            return (Zone)zone;
        }
    }
}
