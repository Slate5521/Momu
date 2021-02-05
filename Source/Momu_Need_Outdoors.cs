/* HarmonyPatches.cs
 * Momu by Rekasa
 * 
 * Momu require a custom outdoors need since they do not need to go outside as often. Sorry Tynan for ripping code straight from Rimworld!
 * 
 * Created by IAmMiko.
*/

namespace Momu
{
    using RimWorld;
    using System.Collections.Generic;
    using UnityEngine;
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

        #region Original RW Code
        private bool Disabled
		{
			get
			{
				return this.pawn.story.traits.HasTrait(TraitDefOf.Undergrounder);
			}
		}
		#endregion Original RW Code

		public override void NeedInterval()
		{
            #region Original RW Code
            if (this.Disabled)
			{
				this.CurLevel = 1f;
				return;
			}
			if (this.IsFrozen)
			{
				return;
			}

			float b = Minimum_IndoorsThinRoof;
			bool flag = !this.pawn.Spawned || this.pawn.Position.UsesOutdoorTemperature(this.pawn.Map);
			RoofDef roofDef = this.pawn.Spawned ? this.pawn.Position.GetRoof(this.pawn.Map) : null;
			float num;
			if (!flag)
			{
				if (roofDef == null)
				{
					num = Delta_IndoorsNoRoof;
				}
				else if (!roofDef.isThickRoof)
				{
					num = Delta_IndoorsThinRoof;
				}
				else
				{
					num = Delta_IndoorsThickRoof;
					b = 0f;
				}
			}
			else if (roofDef == null)
			{
				num = Delta_OutdoorsNoRoof;
			}
			else if (roofDef.isThickRoof)
			{	
				num = Delta_OutdoorsThickRoof;
			}
			else
			{
				num = Delta_OutdoorsThinRoof;
			}
			if (this.pawn.InBed() && num < 0f)
			{
				num *= DeltaFactor_InBed;
			}

			num *= 0.0025f;

			float curLevel = this.CurLevel;
			if (num < 0f)
			{
				this.CurLevel = Mathf.Min(this.CurLevel, Mathf.Max(this.CurLevel + num, b));
			}
			else
			{
				this.CurLevel = Mathf.Min(this.CurLevel + num, 1f);
			}
			this.lastEffectiveDelta = this.CurLevel - curLevel;
			#endregion Original RW Code
		}

		#region Original RW Code, modified by IAmMiko
		// Values modified by IAmMiko

		private const float Delta_IndoorsThickRoof = -0.45f;
		private const float Delta_OutdoorsThickRoof = -0.4f;
		private const float Delta_IndoorsThinRoof = -0.32f;
		private const float Minimum_IndoorsThinRoof = 0.2f;
		private const float Delta_OutdoorsThinRoof = 1f;
		private const float Delta_IndoorsNoRoof = 5f;
		private const float Delta_OutdoorsNoRoof = 8f;
		private const float DeltaFactor_InBed = 0.2f;

		private float lastEffectiveDelta;

		#endregion Original RW Code, modified by IAmMiko
	}
}
