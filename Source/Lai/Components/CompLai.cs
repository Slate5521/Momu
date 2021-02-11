/* \Lai\Components\CompLai.cs
 * Momu by Rekasa
 *
 * Created by Emiko.
 */

namespace Momu.Lai
{
    using Verse;
    using UnityEngine;

    /// <see cref="CompProperties_Lai"/>
    public class CompLai : ThingComp
    {
        public CompProperties_Lai Props => (CompProperties_Lai)this.props;
        public bool WingsDestroyed => false;
    }
}
