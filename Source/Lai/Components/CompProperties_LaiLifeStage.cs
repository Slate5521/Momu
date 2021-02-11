/* \Lai\Components\CompProperties_LaiLifeStage.cs
 * Momu by Rekasa
 *
 * Created by Emiko.
 */

namespace Momu.Lai
{
    using Verse;

    /// <see cref="CompLifeStages"/>
    /// <see cref="LaiStage"/>
    public class CompProperties_LaiLifeStage : CompProperties
    {
        public CompProperties_LaiLifeStage() => this.compClass = typeof(CompLifeStages);

        /// <summary>Current lifestage of the Lai.</summary>
        public LaiLifeStage lifeStage = LaiLifeStage.None;
        /// <summary>The next definition to morph into once this life stage is done.</summary>
        /// <remarks>Defaults to null, which means this won't morph into anything.</remarks>
        public ThingDef nextPawn = null;
        /// <summary>The number of ticks in this lifestage.</summary>
        public int lifeStageTicks = 0;
    }
}
