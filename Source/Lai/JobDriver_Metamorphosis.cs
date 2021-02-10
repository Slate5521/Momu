/* \Lai\JobDriver_Metamorphosis.cs
 * Momu by Rekasa
 *
 * Created by Emiko.
 * 
 * Describes a JobDriver that gives a metamorphosis job to a Lai Chrysalis. The only thing it does is lay there and put itself into a laying down
 * posture so that it heals itself. 
 */ /// <see cref="LaiStageChrysalis"/>


namespace Momu.Lai
{
    using RimWorld;
    using System.Collections.Generic;
    using System.Linq;
    using Verse;
    using Verse.AI;

    /// <see cref="JobGiver_Metamorphosis"/>
    public class JobDriver_Metamorphosis : JobDriver
    {   
        public override bool TryMakePreToilReservations(bool errorOnFailed)
            // Check if our bed exists, and if it does, either make sure it's reserved already or is presently reserved.
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

                        // Set our posture to laying in bed so we aren't downed.
                        pawn.jobs.posture = PawnPosture.LayingInBed;
                        this.asleep = false;
                    },
                
                // ...

                tickAction = 
                    delegate
                    {
                        // If the pawn is in bed spawn a healing mote.
                        #region Original RW Code
                        
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
