using HugsLib;
using HugsLib.GuiInject;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using HugsLib.Utils;
using System.Linq;
using HugsLib.Source.Detour;

namespace MoreZones
{
	public class MoreZones : ModBase
	{
		public override string ModIdentifier
		{
			get { return "MoreZones"; }
		}

		public MoreZones ()
		{
			
		}

		public override void Initialize() {
			Logger.Message("Initialized");
		}

		public override void Tick(int currentTick) {
			//Logger.Message("Tick:"+currentTick);
		}

		public override void Update() {
			//Logger.Message("Update");
		}

		public override void FixedUpdate() {
			//Logger.Message("FixedUpdate");
		}

		public override void OnGUI() {
			//Logger.Message("OnGUI");
		}

		public override void WorldLoaded() {
			Logger.Message("WorldLoaded");
		}

		public override void MapComponentsInitializing(Map map) {
			Logger.Message("MapComponentsInitializing on map:"+map);
		}

		public override void MapLoaded(Map map) {
			Logger.Message("MapLoaded:"+map);
			// this will produce an exception if the map mesh was not regenerated yet
			map.mapDrawer.MapMeshDirty(new IntVec3(0, 0, 0), MapMeshFlag.Buildings);

			//HugsLibController.Instance.CallbackScheduler.ScheduleCallback(() => Logger.Trace("scheduler callback"), 150, true);

			foreach(Zone zone in map.zoneManager.AllZones)
			{
				if (zone.GetType() == typeof(Zone_Stockpile))
				{
					Zone_Stockpile zoneStockpile = (Zone_Stockpile)zone;

					Logger.Message ("Zone: " + zoneStockpile.label);

					foreach (Gizmo gizmo in zoneStockpile.GetGizmos())
					{
						Logger.Message ("Gizmo: " + gizmo.ToString ());

					}
				}
			}
		}

		public override void SceneLoaded(Scene scene) {
			Logger.Message("SceneLoaded:"+scene.name);
		}

		public override void SettingsChanged() {
			Logger.Message("SettingsChanged");
		}

		public override void DefsLoaded()
		{
			Logger.Message("DefsLoaded");

			List<ThingDef> thingDefs = DefDatabase<ThingDef>.AllDefs.ToList<ThingDef>();

			foreach (ThingDef thingDef in thingDefs)
			{
				//Logger.Message ("ThingDef.defName: " + thingDef.defName);

				thingDef.comps.Add (new NewZoneCompProperties ());
			}
		}

//		[WindowInjection(typeof(Dialog_SaveFileList_Load))]
//		private static void DrawSaveDialogButton(Window window, Rect inRect) {
//			Log.Message ("Injecting to window: " + window.ToString ());
//			var buttonSize = new Vector2(40f, 40f);
//			if (Widgets.ButtonText(new Rect(0, buttonSize.y, buttonSize.x, buttonSize.y), "Save")) {
//				// do stuff             
//			}
//		}
	}
}

