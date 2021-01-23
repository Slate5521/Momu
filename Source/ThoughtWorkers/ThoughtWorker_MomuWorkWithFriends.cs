/* ThoughtWorker_MomuWorkWithFriends.cs
 * Momu 2021
 *
 * Originally created by Ogliss.
 * 
 * Cleaned up by IAmMiko. 
*/



namespace Momu
{
    using System.Collections.Generic;
    using RimWorld;
    using Verse;
    using Verse.AI;

    public class ThoughtWorker_MomuWorkWithFriends : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            ThoughtState result;

            if (p.def != MomuDefOf.Alien_Momu)
                result = false;
            else
            {   
                JobDriver curDriver = p.jobs.curDriver;
                
                // Check if it has a jobdriver and skills.
                if (curDriver is null || p.skills is null)
                    result = ThoughtState.Inactive;
                else
                {   
                    if (curDriver.ActiveSkill is null)
                            result = ThoughtState.Inactive;
                    else
                    {
                        SkillRecord skill = p.skills.GetSkill(curDriver.ActiveSkill);
                        if (skill is null)
                            result = ThoughtState.Inactive;
                        else
                        {
                            List<Pawn> list = p.Map.mapPawns.AllPawnsSpawned.FindAll(
                                (Pawn x) => x.def == MomuDefOf.Alien_Momu);
                            
                            if (GenList.NullOrEmpty<Pawn>(list))
                                result = ThoughtState.Inactive;
                            else
                            {
                                List<Pawn> list2 = list.FindAll(
                                    (Pawn x) => PawnRelationUtility.GetRelations(x, p) != null && 
                                                x.relations.OpinionOf(p) > 50 && 
                                                x != p);
                                
                                if (GenList.NullOrEmpty<Pawn>(list2))
                                    result = ThoughtState.Inactive;
                                else
                                {
                                    bool flag = !GenCollection.Any<Pawn>(list2, (Pawn x) => x.Position.InHorDistOf(p.Position, 10f));

                                    if (flag) result = ThoughtState.Inactive;
                                    else      result = ThoughtState.ActiveDefault;
                                } // end else
                            } // end else
                        } // end else
                    } // end else
                } // end else
            } // end else
            return result;
        } // end method
    }
}
