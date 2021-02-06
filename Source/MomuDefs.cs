/* MomuDefs.cs
 * Momu by Rekasa
 * 
 * Created by IAmMiko.
*/

namespace Momu
{
    using RimWorld;
    using Verse;

    [DefOf]
    public static class MomuDefOf
    {
        public static ThingDef Alien_Momu;
        
        public static NeedDef MomuNeedOutdoors;
        public static NeedDef Outdoors;
    }

    [DefOf]
    public static class LaiDefOf
    {
        public static ThingDef Momu_Lai_Egg_Fertilized;
        public static ThingDef Momu_Lai_Larva;
        public static ThingDef Momu_Lai_Chrysalis;

        public static HediffDef LarvaHediff;
        public static HediffDef ChrysalisHediff;

        public static JobDef Metamorphosing;
    }
}
