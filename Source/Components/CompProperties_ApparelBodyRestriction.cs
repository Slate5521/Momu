/* CompProperties_ApparelBodyRestrictions.cs
 * Momu by Rekasa
 *
 * Originally created by Ogliss.
*/

namespace Momu
{
    using System.Collections.Generic;
    using RimWorld;
    using Verse;

    public class CompProperties_ApparelBodyRestriction : CompProperties
    {
        public CompProperties_ApparelBodyRestriction() => this.compClass = typeof(CompApparelBodyRestriction);

        // Token: 0x04000007 RID: 7
        public List<BodyTypeDef> AllowedBodyTypes = new List<BodyTypeDef>();
    }
}
