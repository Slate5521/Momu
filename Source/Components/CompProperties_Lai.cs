/* CompProperties_Lai.cs
 * Momu by Rekasa
 *
 * Created by Emiko.
*/

namespace Momu
{
    using Verse;

    public class CompProperties_Lai : CompProperties
    {
        public CompProperties_Lai() => this.compClass = typeof(CompLai);

        LaiLifeStage lifeStage;
        ThingDef nextStage = null;
    }
}
