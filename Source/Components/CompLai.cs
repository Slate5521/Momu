/* CompLai.cs
 * Momu by Rekasa
 *
 * Created by IAmMiko.
*/

namespace Momu
{
    using Verse;
    using UnityEngine;
    using System;

    public class CompLai : ThingComp
    {
        // Using an interface so we can have a single comp but with different behavior sets that change based on the lifestage.
        LaiStage lifeStageComponent;

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
                    lifeStageComponent = new LaiStageEgg(this);
                    break;
                case LaiLifeStage.Larva:
                    lifeStageComponent = new LaiStageLarva(this);
                    break;
                case LaiLifeStage.Chrysalis:
                    lifeStageComponent = new LaiStageChrysalis(this);
                    break;
                case LaiLifeStage.FullyGrown:
                    lifeStageComponent = new LaiStageAdult(this);
                    break;
            }

            base.Initialize(props);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            lifeStageComponent.PostSpawnSetup(this, respawningAfterLoad);
        }

        public override string CompInspectStringExtra()
        {
            string inspectString = lifeStageComponent.GetInspectionString();

            // Check if there's actually a return string
            if (inspectString.NullOrEmpty())
                // There's nothing to be seen, so let's just return the base.
                return base.CompInspectStringExtra();
            else
                // There's something to be seen, so let's use ours.
                return inspectString;
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

        internal void PostGeneratePawn()
        {
            lifeStageComponent.PostGeneratePawn(this);
        }
    }
}
