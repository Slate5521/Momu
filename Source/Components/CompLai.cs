/* CompLai.cs
 * Momu by Rekasa
 *
 * Created by IAmMiko.
*/

namespace Momu
{
    using Verse;
    using UnityEngine;

    public class CompLai : ThingComp
    {
        // Using an interface so we can have a single comp but with different behavior sets that change based on the lifestage.
        ILaiStage lifeStageComponent;

        public CompProperties_Lai Props => (CompProperties_Lai)this.props;

        public override void PostExposeData()
        {
            lifeStageComponent.PostExposeData(this);
            base.PostExposeData();
        }

        public override void Initialize(CompProperties props)
        {
            var laiCompProps = props as CompProperties_Lai;

            switch (laiCompProps.lifeStage)
            {
                case LaiLifeStage.None:
                    Debug.LogError("[Momu] Tried to initialize CompLai with LaiLifeStage.None!");
                    return;
                case LaiLifeStage.Egg:
                    lifeStageComponent = new LaiStageEgg();
                    break;
                case LaiLifeStage.Larva:
                    lifeStageComponent = new LaiStageLarva();
                    break;
                case LaiLifeStage.Chrysalis:
                    lifeStageComponent = new LaiStageChrysalis();
                    break;
                case LaiLifeStage.FullyGrown:
                    lifeStageComponent = new LaiStageAdult();
                    break;
            }

            lifeStageComponent.Initialize(this, laiCompProps);
            base.Initialize(props);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            lifeStageComponent.PostSpawnSetup(this, respawningAfterLoad);
            base.PostSpawnSetup(respawningAfterLoad);
        }

        public override void CompTick()
        {
            // Regular comptick
            lifeStageComponent.CompTick(this);

            // Rare comptick
            if (parent.IsHashIntervalTick(250))
                lifeStageComponent.CompTickRare(this);

            base.CompTick();
        }
    }
}
