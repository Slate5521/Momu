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
        public LaiStage LifeStageComponent { get; protected set; }

        public CompProperties_Lai Props => (CompProperties_Lai)this.props;

        public override void PostExposeData()
        {
            LifeStageComponent.PostExposeData(this);
            base.PostExposeData();
        }

        public override void Initialize(CompProperties props)
        {
            var laiCompProps = props as CompProperties_Lai;

            switch (laiCompProps.lifeStage)
            {
                default:
                case LaiLifeStage.None:
                    Debug.LogError("[Momu] Tried to initialize CompLai with LaiLifeStage.None or default enum value!");
                    return;
                case LaiLifeStage.Egg:
                    LifeStageComponent = new LaiStageEgg(this);
                    break;
                case LaiLifeStage.Larva:
                    LifeStageComponent = new LaiStageLarva(this);
                    break;
                case LaiLifeStage.Chrysalis:
                    LifeStageComponent = new LaiStageChrysalis(this);
                    break;
                case LaiLifeStage.FullyGrown:
                    LifeStageComponent = new LaiStageAdult(this);
                    break;
            }

            base.Initialize(props);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

            LifeStageComponent.PostSpawnSetup(this, respawningAfterLoad);
        }

        public override string CompInspectStringExtra()
        {
            string inspectString = LifeStageComponent.GetInspectionString();

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
            LifeStageComponent.CompTick(this);

            // Rare comptick
            if (parent.IsHashIntervalTick(250))
                LifeStageComponent.CompTickRare(this);

            base.CompTick();
        }

        internal void PostGeneratePawn()
        {
            LifeStageComponent.PostGeneratePawn(this);
        }
    }
}
