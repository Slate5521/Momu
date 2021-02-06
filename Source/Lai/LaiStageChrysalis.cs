using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Momu
{
    public class LaiStageChrysalis : LaiStage
    {
        public Building_Bed previousBed;
        public LaiStageChrysalis(CompLai comp) : base(comp) { }

        public override void PostExposeData(CompLai compInstance)
        {
            Scribe_Values.Look(ref LifeStageTicks, @"LifeStageTicks", 0);
        }

        public void Notify_PreEvolveCircumstances(Building_Bed bed)
            => previousBed = bed;

        public override void PostSpawnSetup(CompLai compLai, bool respawningAfterLoad) { }

        public override void PostGeneratePawn(CompLai compInstance)
        {
            Parent.health.AddHediff(LaiDefOf.ChrysalisHediff);
        }

        public override string GetInspectionString()
            => string.Format(@"{0}: {1}",
                             @"Time until adult".Translate(),
                             Mathf.Max(ParentComp.Props.lifeStageTicks - LifeStageTicks, 0).ToStringTicksToPeriod(true, false, false, true));
    }
}
