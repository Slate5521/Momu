/* LaiStage.cs
 * Momu by Rekasa
 * 
 * Created by IAmMiko.
 */

namespace Momu
{
    using RimWorld;
    using UnityEngine;
    using Verse;
    using Verse.Sound;

    public abstract class LaiStage
    {
        protected CompLai ParentComp;
        protected int LifeStageTicks; // In RareTicks

        protected Pawn Parent => ParentComp.parent as Pawn;
        protected bool ShouldEvolve
            => LifeStageTicks >= ParentComp.Props.lifeStageTicks && 
               ParentComp.Props.nextLifeStage != LaiLifeStage.None;
        
        protected LaiStage(CompLai comp)
        {
            ParentComp = comp;
            LifeStageTicks = 0;
        }

        private void EvolveNow()
        {
            ThingDef nextStage = ParentComp.Props.nextPawn;
            IntVec3 oldPawnPos = Parent.Position;
            Map oldMap         = Parent.Map;

            Debug.Log("DOGGOGAGASDGW");

            Debug.Log("BBBBBBBBBBBBBB1: " + nextStage.defName);
            Debug.Log("BBBBBBBBBBBBBB1b: " + PawnKindDef.Named(nextStage.defName));
            Debug.Log("BBBBBBBBBBBBBB2: " + Parent.Faction);
            Debug.Log("BBBBBBBBBBBBBB3: " + Parent.Name.ToString());
            Debug.Log("BBBBBBBBBBBBBB4: " + Parent.ageTracker.AgeBiologicalYearsFloat);
            Debug.Log("BBBBBBBBBBBBBB5: " + Parent.ageTracker.AgeChronologicalYearsFloat);
            Pawn newPawn = PawnGenerator.GeneratePawn(
                TailorPawnNext(
                    defName: nextStage.defName, 
                    faction: Parent.Faction, 
                    name: Parent.Name.ToString(), 
                    bioAge: Parent.ageTracker.AgeBiologicalYearsFloat, 
                    chronoAge: Parent.ageTracker.AgeChronologicalYearsFloat));

            Debug.Log("AAAAAAAAAAAAAA3");
            Parent.Destroy();

            Debug.Log("AAAAAAAAAAAAAA4");
            GenSpawn.Spawn(newPawn, oldPawnPos, oldMap);
            SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(oldPawnPos, oldMap));
        }

        public virtual void CompTick(CompLai compInstance)
        { 
            ++LifeStageTicks; 
        }

        public virtual void CompTickRare(CompLai compInstance)
        {
            if (ShouldEvolve)
            {
                EvolveNow();
            }
        }

        public virtual void PostGeneratePawn(CompLai compInstance)
        {
            Parent.health.AddHediff(LaiDefOf.LarvaHediff);
        }

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

        public abstract void PostSpawnSetup(CompLai compLai, bool respawningAfterLoad);
        public abstract void PostExposeData(CompLai compInstance);
        public abstract string GetInspectionString();
    }
}
