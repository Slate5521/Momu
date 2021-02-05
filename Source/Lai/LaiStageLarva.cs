using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momu
{
    public class LaiStageLarva : ILaiStage
    {
        public LaiStageLarva()
        {

        }

        public void CompTick(CompLai compInstance)
        {
            throw new NotImplementedException();
        }

        public void CompTickRare(CompLai compInstance)
        {
            throw new NotImplementedException();
        }

        public void Initialize(CompLai compLai, CompProperties_Lai props)
        {
            throw new NotImplementedException();
        }

        public void PostExposeData(CompLai compInstance)
        {
            throw new NotImplementedException();
        }

        public void PostSpawnSetup(CompLai compLai, bool respawningAfterLoad)
        {
            // Check if this is the first time it's being spawned, ever.
            if(!respawningAfterLoad)
            {

            }
        }
    }
}
