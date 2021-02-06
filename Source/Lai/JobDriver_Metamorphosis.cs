using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace Momu
{
    public class JobDriver_Metamorphosis : JobDriver
    {
        Building_Bed Bed => this.job.GetTarget(TargetIndex.A).Thing as Building_Bed;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
            => job.GetTarget(TargetIndex.A).HasThing &&
               (ReservationUtility.HasReserved(pawn, TargetA, job) || ReservationUtility.Reserve(pawn, TargetA, job));

        protected override IEnumerable<Toil> MakeNewToils()
        {
            // Claim bed if possible.
            if(this.job.GetTarget(TargetIndex.A).HasThing)
            {
                yield return Toils_Bed.ClaimBedIfNonMedical(TargetIndex.A, TargetIndex.None);
            }

            Toil toil = new Toil
            {
                defaultCompleteMode = ToilCompleteMode.Never,
                actor = pawn,

                // ...

                initAction =
                    delegate
                    {
                        // Check if our pawn is even in the bed.
                        if (!TargetA.Thing.OccupiedRect().Contains(pawn.Position))
                        {   // Nope.
                            base.ReadyForNextToil();
                        }

                        pawn.jobs.posture = PawnPosture.LayingInBed;
                        this.asleep = false;
                    },
                
                // ...

                tickAction = 
                    delegate
                    {
                        // Heal the pawn
                        #region Original RW Code
                        float restEffectiveness;
                        
                        if (Bed != null && Bed.def.statBases.StatListContains(StatDefOf.BedRestEffectiveness))
                        {
                            restEffectiveness = Bed.GetStatValue(StatDefOf.BedRestEffectiveness, true);
                        }
                        else
                        {
                            restEffectiveness = StatDefOf.BedRestEffectiveness.valueIfMissing;
                        }

                        pawn.needs.rest.TickResting(restEffectiveness);

                        if (pawn.IsHashIntervalTick(100) && !pawn.Position.Fogged(pawn.Map))
                        {
                            if (pawn.health.hediffSet.GetNaturallyHealingInjuredParts().Any<BodyPartRecord>())
                            {
                                MoteMaker.ThrowMetaIcon(pawn.Position, pawn.Map, ThingDefOf.Mote_HealingCross);
                            }
                        }
                        #endregion Original RW Code                   
                    }
            };

            toil.FailOnBedNoLongerUsable(TargetIndex.A);

            yield return toil;
            yield break;
        }

        public override string GetReport()
            => "Metamorphosing".Translate();
    }
}
