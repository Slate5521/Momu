/* CompApparelBodyRestrictions.cs
 * Momu by Rekasa
 * 
 * This class is for creating a customized Outdoors need for the Momu. They don't get negative moodlets from being inside too quickly.
 *
 * Created by IAmMiko.
*/

namespace Momu
{
	using RimWorld;
	using System;
	using Verse;

	class ThoughtWorker_MomuNeedOutdoors : ThoughtWorker
    {
		protected override ThoughtState CurrentStateInternal(Pawn p)
		{
			Momu_Need_Outdoors momuOutdoorNeed = p.needs.TryGetNeed<Momu_Need_Outdoors>();

			if (momuOutdoorNeed == null) 
				return ThoughtState.Inactive;
			if (p.HostFaction != null)
				return ThoughtState.Inactive;

			switch (momuOutdoorNeed.CurCategory)
			{
				case 0: return ThoughtState.ActiveAtStage(0);
				case 1: return ThoughtState.ActiveAtStage(1);
				case 2: return ThoughtState.Inactive;
				default: throw new InvalidOperationException("Unknown OutdoorsCategory");
			}
		}
	}
}
