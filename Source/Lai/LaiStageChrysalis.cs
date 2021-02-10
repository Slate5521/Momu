/* \Lai\LaiStageChrysalis.cs
 * Momu by Rekasa
 * 
 * Created by IAmMiko.
 */

namespace Momu.Lai
{
    using RimWorld;
    using UnityEngine;
    using Verse;

    public class LaiStageChrysalis : LaiStage
    {
        public Building_Bed previousBed;
        public LaiStageChrysalis(CompLifeStages comp) : base(comp) { }

        public void Notify_PreEvolveCircumstances(Building_Bed bed)
            => previousBed = bed;

        public override void PostSpawnSetup(CompLifeStages compLai, bool respawningAfterLoad) { }

        public override void PostGeneratePawn(CompLifeStages compInstance)
        {
            Parent.health.AddHediff(LaiDefOf.ChrysalisHediff);
        }

        public override string GetInspectionString()
            => string.Format(@"{0}: {1}",
                             @"Time until adult".Translate(),
                             Mathf.Max(ParentComp.Props.lifeStageTicks - LifeStageTicks, 0).ToStringTicksToPeriod(true, false, false, true));
    }
}
