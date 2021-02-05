/* CompLai.cs
 * Momu by Rekasa
 *
 * Created by IAmMiko.
 */


namespace Momu
{
    public interface ILaiStage
    {
        void CompTick(CompLai compInstance);
        void CompTickRare(CompLai compInstance);
        void PostExposeData(CompLai compInstance);
        void PostSpawnSetup(CompLai compLai, bool respawningAfterLoad);
        void Initialize(CompLai compLai, CompProperties_Lai props);
    }
}
