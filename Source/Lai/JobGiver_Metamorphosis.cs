using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace Momu
{
    public class JobGiver_Metamorphosis : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            // Check if it's a Chyrsalis, if not don't do the job
            if (pawn.def != LaiDefOf.Momu_Lai_Chrysalis)
                return null;

            Building_Bed curBed = pawn.InBed() ? 
                pawn.CurrentBed() : 
                (pawn.GetComp<CompLai>().LifeStageComponent as LaiStageChrysalis).previousBed;

            return JobMaker.MakeJob(LaiDefOf.Metamorphosing, curBed);
        }

        public override float GetPriority(Pawn pawn)
        {
            // Return highest possible priority if this is a Chrysalis otherwise default.
            return pawn.def == LaiDefOf.Momu_Lai_Chrysalis ?
                   // highest priority
                   8f :
                   // default priority
                   base.GetPriority(pawn);
        }
    }
}
