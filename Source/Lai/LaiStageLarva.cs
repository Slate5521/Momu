/* \Lai\LaiStageLarva.cs
 * Momu by Rekasa
 * 
 * Created by IAmMiko.
 */

namespace Momu.Lai
{
    using RimWorld;
    using UnityEngine;
    using Verse;

    public class LaiStageLarva : LaiStage
    {
        public LaiStageLarva(CompLifeStages comp) : base(comp) { }

        public override void PostSpawnSetup(CompLifeStages compLai, bool respawningAfterLoad) { }
        public override void PostGeneratePawn(CompLifeStages compInstance) 
        {
            Parent.health.AddHediff(LaiDefOf.LarvaHediff);
        }

        protected override void HandleEvolveDiffs(Pawn oldPawn, ref Pawn newPawn)
        {
            (newPawn.GetComp<CompLifeStages>().LifeStageComponent as LaiStageChrysalis).Notify_PreEvolveCircumstances(oldPawn.CurrentBed());
        }

        public override string GetInspectionString()
            => string.Format(@"{0}: {1}", 
                             @"Time until chrysalis".Translate(), 
                             Mathf.Max(ParentComp.Props.lifeStageTicks - LifeStageTicks, 0).ToStringTicksToPeriod(true, false, false, true));
    }
}
