/* ColorGenerator_MomuTones.cs
 * Momu by Rekasa
 *
 * Generates fur tones based on a list of provided weighted colors.
 * 
 * Created by IAmMiko.
*/

namespace Momu
{
    using System.Collections.Generic;
    using UnityEngine;
    using Verse;
    class ColorGenerator_MomuTones : ColorGenerator
    {
        public override Color NewRandomizedColor()
            => options.RandomElementByWeight(a => a.weight).RandomizedColor();

        public List<ColorOption> options;
    }
}
