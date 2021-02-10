/* \Lai\LaiStage.cs
 * Momu by Rekasa
 * 
 * Created by IAmMiko.
 * 
 * Describes an abstract class that represents a LaiStage and all of its basic components.
 */

namespace Momu.Lai
{
    using RimWorld;
    using Verse;
    using Verse.Sound;

    public abstract class LaiStage
    {
        protected CompLifeStages ParentComp;
        /// <summary>Current life stage ticks.</summary>
        protected int LifeStageTicks; 

        protected Pawn Parent => ParentComp.parent as Pawn;
        protected bool ShouldEvolve
            // Only advance to the next lifestage if the pawn is ready chronologically, and if there's a defined def for the next stage.
            => LifeStageTicks >= ParentComp.Props.lifeStageTicks && 
               ParentComp.Props.nextPawn != null;
        
        protected LaiStage(CompLifeStages comp)
        {
            ParentComp = comp;
            LifeStageTicks = 0;
        }

        /// <summary>Advance to the next lifestage.</summary>
        private void EvolveNow()
        {
            ThingDef nextStage = ParentComp.Props.nextPawn;
            IntVec3 oldPawnPos = Parent.Position;
            Map oldMap         = Parent.Map;

            Pawn newPawn = PawnGenerator.GeneratePawn(
                TailorPawnNext(
                    defName: nextStage.defName, 
                    faction: Parent.Faction, 
                    name: Parent.Name.ToString(), 
                    bioAge: Parent.ageTracker.AgeBiologicalYearsFloat, 
                    chronoAge: Parent.ageTracker.AgeChronologicalYearsFloat));

            HandleEvolveDiffs(Parent, ref newPawn);

            Parent.Destroy();

            GenSpawn.Spawn(newPawn, oldPawnPos, oldMap);
            SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(oldPawnPos, oldMap));
        }

        /// <summary>Handle the difference when a pawn transforms to the next stage.</summary>
        protected virtual void HandleEvolveDiffs(Pawn oldPawn, ref Pawn newPawn)
        { return; }

        /// <summary>Called when the pawn finishes generating.</summary>
        public virtual void PostGeneratePawn(CompLifeStages compInstance)
        { return; }

        public virtual void CompTick(CompLifeStages compInstance)
        { 
            ++LifeStageTicks; 
        }

        /// <summary>Tailor the next pawn based on a PawnGenerationRequest.</summary>
        /// <see cref="PawnGenerationRequest"/>
        public virtual PawnGenerationRequest TailorPawnNext(string defName, Faction faction, string name, float bioAge, float chronoAge)
            => new PawnGenerationRequest(
                kind: PawnKindDef.Named(defName),
                faction: faction,
                context: PawnGenerationContext.NonPlayer,
                newborn: false,
                allowAddictions: false,
                fixedBirthName: name,
                fixedBiologicalAge: bioAge,
                fixedChronologicalAge: chronoAge);

        public virtual void CompTickRare(CompLifeStages compInstance)
        {
            if (ShouldEvolve)
            {
                EvolveNow();
            }
        }

        public virtual void PostExposeData()
        {
            Scribe_Values.Look(ref LifeStageTicks, @"LifeStageTicks", 0);
        }

        public abstract void PostSpawnSetup(CompLifeStages compLai, bool respawningAfterLoad);
        public abstract string GetInspectionString();
    }
}
