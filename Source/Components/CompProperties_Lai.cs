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
    }
}
