using System;
using Verse;
using System.Collections.Generic;
using UnityEngine;
using HugsLib;

namespace MoreZones
{
	public class NewZoneButton : ThingComp
	{
		public NewZoneButton ()
		{
		}

		public override IEnumerable<Gizmo> CompGetGizmosExtra ()
		{
			List<Gizmo> gizmos = new List<Gizmo> ();

			Command_Action command = new Command_Action ();
			command.icon = ContentFinder<Texture2D>.Get ("UnSkilled", true);
			command.action = new Action (this.DoThing);
			command.defaultLabel = "Test";
			command.defaultDesc = "Test description";

			gizmos.Add (command);

			return gizmos;
		}

		private void DoThing()
		{
			Log.Message ("Button clicked");
		}
	}
}

