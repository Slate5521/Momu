/* HarmonyPatches.cs
 * Momu 2021
 * 
 * Momu require a custom outdoors need since they do not need to go outside as often.
 * 
 * Created by IAmMiko.
*/

namespace Momu
{
    using RimWorld;
    using System.Collections.Generic;
    using Verse;

    public class Momu_Need_Outdoors : Need_Outdoors
    {
        const float THRESH_025 = 0.25f;
        const float THRESH_005 = 0.05f;

        public Momu_Need_Outdoors(Pawn pawn) : base(pawn)
        {
            this.threshPercents = new List<float>
            {
                THRESH_025,
                THRESH_005
            };
        }

        public new int CurCategory
        {
            get
			{
				if (this.CurLevel > THRESH_025) return 2;
				if (this.CurLevel > THRESH_005) return 1;
				                                return 0;
			}
        }
    }
}
