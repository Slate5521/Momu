/* LaiStageLarva.cs
 * Momu by Rekasa
 * 
 * Created by IAmMiko.
 */

namespace Momu
{
    using RimWorld;
    using UnityEngine;
    using Verse;

    public class LaiStageLarva : LaiStage
    {
        public LaiStageLarva(CompLai comp) : base(comp) { }

        public override void PostExposeData(CompLai compInstance)
        { 
            Scribe_Values.Look(ref LifeStageTicks, @"LifeStageTicks", 0);
        }

        public override void PostSpawnSetup(CompLai compLai, bool respawningAfterLoad) { }
        public override void PostGeneratePawn(CompLai compInstance) 
        {
            Parent.health.AddHediff(LaiDefOf.LarvaHediff);
        }

        public override string GetInspectionString()
            => string.Format(@"{0}: {1}", 
                             @"Time until chrysalis".Translate(), 
                             (ParentComp.Props.lifeStageTicks - LifeStageTicks).ToStringTicksToPeriod(true, false, false, true));
    }
}
