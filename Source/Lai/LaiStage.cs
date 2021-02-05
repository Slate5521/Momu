﻿/* LaiStage.cs
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
               GetNextStage() != LaiLifeStage.None;
        
        protected LaiStage(CompLai comp)
        {
            ParentComp = comp;
            LifeStageTicks = 0;
        }

        public LaiLifeStage GetNextStage()
        {
            switch(ParentComp.Props.nextLifeStage)
            {
                default: 
                case LaiLifeStage.None:
                    return LaiLifeStage.None;
                case LaiLifeStage.Egg:
                    return LaiLifeStage.Larva;
                case LaiLifeStage.Larva:
                    return LaiLifeStage.Chrysalis;
                case LaiLifeStage.Chrysalis:
                    return LaiLifeStage.FullyGrown;
                case LaiLifeStage.FullyGrown:
                    goto case LaiLifeStage.None;
            }
        }

        private void EvolveNow()
        {
            ThingDef nextStage = ParentComp.Props.nextPawn;
            IntVec3 oldPawnPos = Parent.Position;
            Map oldMap         = Parent.Map;

            Parent.def = nextStage;

            Pawn newPawn = PawnGenerator.GeneratePawn(
                TailorPawnNext(
                    defName: nextStage.defName, 
                    faction: Parent.Faction, 
                    name: Parent.Name.ToString(), 
                    bioAge: Parent.ageTracker.AgeBiologicalYearsFloat, 
                    chronoAge: Parent.ageTracker.AgeChronologicalYearsFloat));

            Parent.Destroy();

            GenSpawn.Spawn(newPawn, oldPawnPos, oldMap);
            SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(oldPawnPos, oldMap));
        }

        public virtual void CompTick(CompLai compInstance)
        { return; }

        public virtual void CompTickRare(CompLai compInstance)
        {
            LifeStageTicks += 250;

            if (ShouldEvolve)
                EvolveNow();
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
                newborn: true,
                allowAddictions: false,
                fixedBirthName: name,
                fixedBiologicalAge: bioAge,
                fixedChronologicalAge: chronoAge);

        public abstract void PostSpawnSetup(CompLai compLai, bool respawningAfterLoad);
        public abstract void PostExposeData(CompLai compInstance);
        public abstract string GetInspectionString();
    }
}
