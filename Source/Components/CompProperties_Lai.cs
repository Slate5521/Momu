/* CompProperties_Lai.cs
 * Momu by Rekasa
 *
 * Created by Emiko.
*/

namespace Momu
{
    using System;
    using Verse;

    public class CompProperties_Lai : CompProperties
    {
        public CompProperties_Lai() => this.compClass = typeof(CompLai);

        public LaiLifeStage lifeStage = LaiLifeStage.None;
        public LaiLifeStage nextLifeStage = LaiLifeStage.None;
        public ThingDef nextPawn = null;
        public int lifeStageTicks = 0;
    }
}
