/* \Lai\Components\CompLifestages.cs
 * Momu by Rekasa
 *
 * Created by IAmMiko.
 * 
 * Describes a component that allows a pawn to morph into an indefinite amount of different pawns throughout its lifetime.
 */

namespace Momu.Lai
{
    using Verse;
    using UnityEngine;
    
    public class CompLifeStages : ThingComp
    {
        // Using an interface so we can have a single comp but with different behavior sets that change based on the lifestage.
        public LaiStage LifeStageComponent { get; protected set; }

        public CompProperties_LaiLifeStage Props => (CompProperties_LaiLifeStage)this.props;

        public override void PostExposeData()
        {
            LifeStageComponent.PostExposeData();
            base.PostExposeData();
        }

        public override void Initialize(CompProperties props)
        {
            var laiCompProps = props as CompProperties_LaiLifeStage;

            switch (laiCompProps.lifeStage)
            {
                default:
                case LaiLifeStage.None:
                    Debug.LogError("[Momu] Tried to initialize CompLai with LaiLifeStage.None or default enum value!");
                    return;
                case LaiLifeStage.Larva:
                    LifeStageComponent = new LaiStageLarva(this);
                    break;
                case LaiLifeStage.Chrysalis:
                    LifeStageComponent = new LaiStageChrysalis(this);
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
