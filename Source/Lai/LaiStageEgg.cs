using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Momu
{
    public class LaiStageEgg : LaiStage
    {
        public LaiStageEgg(CompLai comp) : base(comp)
        {

        }

        public override void CompTick(CompLai compLai)
        { return; }

        public override void CompTickRare(CompLai compLai)
        { return; }

        public override string GetInspectionString()
        { return String.Empty; }

        public override void PostExposeData(CompLai compInstance)
        { return; }

        public override void PostSpawnSetup(CompLai compLai, bool respawningAfterLoad)
        { return; }
    }
}
