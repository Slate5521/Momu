/* \Lai\JobGiver_Metamorphosis.cs
 * Momu by Rekasa
 *
 * Created by Emiko.
 * 
 * Describes a JobGiver that is specific to the Lai Chrysalis' think nodes.
 */ /// <see cref="Momu.Lai.LaiStageChrysalis"/>
    /// <see cref="Momu.Lai.JobDriver_Metamorphosis"/>

namespace Momu.Lai
{
    using RimWorld;
    using Verse;
    using Verse.AI;

    /// <see cref="JobDriver_Metamorphosis"/>
    public class JobGiver_Metamorphosis : ThinkNode_JobGiver
    {
        /// <see cref="LaiStageChrysalis.Notify_PreEvolveCircumstances(Building_Bed)"/>
        protected override Job TryGiveJob(Pawn pawn)
        {
            // Check if it's a Chyrsalis, if not don't do the job
            if (pawn.def != LaiDefOf.Momu_Lai_Chrysalis)
                return null;

            // Get the bed target based on if the pawn currently has a bed. If the pawn doesn't have a bed, then see if the saved bed from the larva
            // generation has a bed.
            Building_Bed curBed = pawn.CurrentBed() ?? 
                (pawn.GetComp<CompLifeStages>().LifeStageComponent as LaiStageChrysalis).previousBed;

            // If there's no bed to begin with, there's no point.
            if (curBed is null)
                return null;

            return JobMaker.MakeJob(LaiDefOf.Metamorphosing, curBed);
        }

        public override float GetPriority(Pawn pawn)
        {
            // Return highest possible priority if this is a Chrysalis otherwise default.
            return pawn.def == LaiDefOf.Momu_Lai_Chrysalis ?
                   // highest priority(?)
                   8f :
                   // default priority
                   base.GetPriority(pawn);
        }
    }
}


