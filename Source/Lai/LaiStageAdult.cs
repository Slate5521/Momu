using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Momu
{
    public class LaiStageAdult : LaiStage
    {
        public LaiStageAdult(CompLai comp) : base(comp)
        {

        }

        public override void CompTick(CompLai compInstance)
        { return; } // Override default ticking. We don't want to tick for lifestages.


        public override void CompTickRare(CompLai compInstance)
        { return; } // Override default ticking. We don't want to tick for lifestages.

        public override string GetInspectionString()
        { return String.Empty; }

        public override void PostExposeData(CompLai compInstance)
        { return; }

        public override void PostSpawnSetup(CompLai compLai, bool respawningAfterLoad)
        { return; }
    }
}
