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
    public static class ZoneStockpileCommands
    {
		private static Zone_Stockpile selectedZoneStockpile = null;

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

		public static Command_Toggle CreateCommandToggle(string label, string description, string icon, Action action, Func<bool> isActive, KeyBindingDef keyBindingDef = null)
		{
			Command_Toggle command = new Command_Toggle ();
			command.defaultLabel = label;
			command.defaultDesc = description;
			command.icon = ContentFinder<Texture2D>.Get (icon, true);
			command.toggleAction = action;
			command.isActive = isActive;

			if (keyBindingDef != null)
			{
				command.hotKey = keyBindingDef;
			}

			return command;
		}

		[DetourMethod(typeof(Zone_Stockpile), "GetGizmos")]
		private static IEnumerable<Gizmo> GetGizmos(this Zone_Stockpile self)
		{
			selectedZoneStockpile = self;

			List<Gizmo> gizmos = new List<Gizmo> ();

			// Add rename command
			KeyBindingDef renameHotkey = new KeyBindingDef();
			renameHotkey.defaultKeyCodeA = KeyCode.B;
			renameHotkey.defaultKeyCodeB = KeyCode.B;
			Command_Action renameCommandAction = CreateCommandAction (
				"Rename",
				"Rename this zone.",
				"UI/Commands/RenameZone",
				new Action(Rename),
				renameHotkey
			);
			gizmos.Add (renameCommandAction);

			// TODO: Add Hide toggle
			KeyBindingDef hideToggleHotkey = new KeyBindingDef();
			renameHotkey.defaultKeyCodeA = KeyCode.H;
			renameHotkey.defaultKeyCodeB = KeyCode.H;
			Command_Toggle hideUnhideToggle = CreateCommandToggle (
				"Hide",
				"Hide/unhide this zone.",
				"UI/Commands/HideZone",
				new Action(ToggleHide),
				new Func<bool>(ToggleIsActive),
				hideToggleHotkey
          	);
			gizmos.Add (hideUnhideToggle);

			// Add delete command
			KeyBindingDef deleteHotkey = new KeyBindingDef();
			deleteHotkey.defaultKeyCodeA = KeyCode.Y;
			Command_Action deleteCommandAction = CreateCommandAction (
				"Delete",
				"Delete this zone.",
				"Delete",
				new Action(Delete),
				deleteHotkey
			);
			gizmos.Add (deleteCommandAction);

			// Add copy settings command
			KeyBindingDef copySettingsHotkey = new KeyBindingDef();
			copySettingsHotkey.defaultKeyCodeA = KeyCode.N;
			Command_Action copySettingsCommandAction = CreateCommandAction (
				"Copy settings",
				"Copy this zone's settings to the clipboard so you can paste them into another zone.",
				"UI/Commands/CopySettings",
				new Action(CopySettings),
				copySettingsHotkey
			);
			gizmos.Add (copySettingsCommandAction);

			// Add paste settings command
			KeyBindingDef pasteSettingsHotkey = new KeyBindingDef();
			pasteSettingsHotkey.defaultKeyCodeA = KeyCode.J;
			Command_Action pasteSettingsCommandAction = CreateCommandAction (
				"Paste settings",
				"Overwrite this zone's settings with those previously copied to the clipboard.",
				"UI/Commands/PasteSettings",
				new Action(PasteSettings),
				pasteSettingsHotkey
			);
			gizmos.Add (pasteSettingsCommandAction);

			return gizmos;
		}

		private static void Rename()
		{
			Find.WindowStack.Add (new Dialog_RenameZone (selectedZoneStockpile));
		}

		private static void ToggleHide()
		{
			if (selectedZoneStockpile.hidden)
			{
				selectedZoneStockpile.hidden = false;
			}
			else
			{
				selectedZoneStockpile.hidden = true;
			}


		}

		private static bool ToggleIsActive()
		{
			return selectedZoneStockpile.hidden;
		}

		private static void Delete()
		{
			selectedZoneStockpile.Delete ();
		}

		private static void CopySettings()
		{

		}

		private static void PasteSettings()
		{

		}
    }
}
