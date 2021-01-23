/* HarmonyPatches.cs
 * Momu 2021
 *
 * Originally created by Ogliss.
 * 
 * Modified by IAmMiko.
*/


namespace Momu
{
    using System;
    using System.Linq;
    using System.Reflection;
    using HarmonyLib;
    using RimWorld;
    using Verse;

    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        ///
        /// Harmony Patch Documentation
        /// 
        /// <patch original cref="FoodUtility.AddFoodPoisoningHediff(Pawn, Thing, FoodPoisonCause)"
        ///        prefix   cref="FoodUtility__AddFoodPoisoningHediffPrefix">
        ///        Patch for preventing food poison in Momu who are eating raw vegetarian food. 
        ///        
        ///        Originally created by Ogliss and modified by IAmMiko to specifically target Momu defs instead of a ThingComp.
        ///        </patch>
        /// <patch original cref="ApparelUtility.HasPartsToWear(Pawn, ThingDef)"
        ///        postfix  cref="ApparelUtility__HasPartsToWearPostfix">
        ///        Patch for making Momu clothes specific to particular body types (I think).
        ///        
        ///        Originally created by Ogliss.
        ///        </patch>
        /// <patch original cref="Pawn_NeedsTracker.ShouldHaveNeed(NeedDef)"
        ///        postfix  cref="Pawn_NeedsTracker__ShouldHaveNeedPostfix">
        ///        Patch to delete the original vanilla Outdoors need and insert our own <see cref="Momu_Need_Outdoors"/> need.
        ///        
        ///        - IAmMiko
        ///        </patch>

        static HarmonyPatches()
        {
            Harmony harmony = new Harmony("rimworld.iammiko.momu.harmony");
            Type harmonyPatch = typeof(HarmonyPatches);

            /// <a cref="FoodUtility__AddFoodPoisoningHediffPrefix"/>
            harmony.Patch( 
                original: AccessTools.Method(
                    type: typeof(FoodUtility), 
                    name: nameof(FoodUtility.AddFoodPoisoningHediff)), 
                prefix: new HarmonyMethod(harmonyPatch, nameof(FoodUtility__AddFoodPoisoningHediffPrefix)));

            /// <a cref="ApparelUtility__HasPartsToWearPostfix"/>
            harmony.Patch( 
                original: AccessTools.Method(
                    type: typeof(ApparelUtility), 
                    name: nameof(ApparelUtility.HasPartsToWear)),
                postfix: new HarmonyMethod(harmonyPatch, nameof(ApparelUtility__HasPartsToWearPostfix)));

            /// <a cref="Pawn_NeedsTracker__ShouldHaveNeedPostfix"/>
            harmony.Patch( 
                original: AccessTools.Method(
                    type: typeof(Pawn_NeedsTracker),
                    name: @"ShouldHaveNeed"),
                postfix: new HarmonyMethod(harmonyPatch, nameof(Pawn_NeedsTracker__ShouldHaveNeedPostfix), null));
        }

        public static void Pawn_NeedsTracker__ShouldHaveNeedPostfix
        (Pawn_NeedsTracker __instance, NeedDef nd, ref bool __result)
        {
            // Get the pawn through Reflection
            Pawn p = typeof(Pawn_NeedsTracker)
                        .GetField(@"pawn", BindingFlags.NonPublic | BindingFlags.Instance)
                        .GetValue(__instance) as Pawn;

            // Check if it's a Momu
            if (p.def == MomuDefOf.Alien_Momu)
            {
                if (nd == MomuDefOf.Outdoors)
                    __result = false;

                if (nd == MomuDefOf.MomuNeedOutdoors)
                    // Our own Momu outdoor need.
                    __result = true;
            }
        }


        public static void ApparelUtility__HasPartsToWearPostfix
        (Pawn p, ThingDef apparel, ref bool __result)
        {
            if (apparel.HasComp(typeof(CompApparelBodyRestriction)))
            {
                CompProperties_ApparelBodyRestriction compProperties = apparel.GetCompProperties<CompProperties_ApparelBodyRestriction>();
                if (compProperties != null)
                {
                    __result = compProperties.AllowedBodyTypes.Contains(p.story.bodyType);
                } // end if
            } // end if
        } // end method
        
        public static bool FoodUtility__AddFoodPoisoningHediffPrefix
        (Pawn pawn, Thing ingestible, FoodPoisonCause cause)
        {       
            if  // ...
            #region if-statement
                // Check if it's a Momu who just ate a dangerous food type.
                (pawn.def == MomuDefOf.Alien_Momu && 
                 cause == FoodPoisonCause.DangerousFoodType &&
                 
                 // We only want to protect against food poisoning if the Momu ate plant matter or veggie food.
                 ingestible.def.thingCategories.All(
                    cat => 
                    cat == ThingCategoryDefOf.PlantFoodRaw ||
                    cat == ThingCategoryDefOf.PlantMatter))
            #endregion if-statement
            {   // We now know it's a Momu who ate some kind of raw vegetarian food, so we must stop this.
                return false;
            }

            return true;
        } // end method
    }
}
