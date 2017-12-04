using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
    public class StructureAutoModeOptions
    {
        public bool AecEn { get; set; }
        public bool AecWaitEn { get; set; }
        public bool BuzzerEn { get; set; }
        public bool OptionalStopEn { get; set; }
        public bool BlockSkipEn { get; set; }
        public bool RefMoveEn { get; set; }
        public bool RadOffsetEn { get; set; }
        public bool MachineLockEn { get; set; }
        public bool M02En { get; set; }
        public bool StartNoEn { get; set; }
        public short StartNo { get; set; }
        public void Init()
        {
            AecEn = false;
            AecWaitEn = false;
            BuzzerEn = false;
            OptionalStopEn = false;
            BlockSkipEn = false;
            RefMoveEn = false;
            RadOffsetEn = false;
            MachineLockEn = false;
            M02En = false;
            StartNoEn = false;
            StartNo = 0;
        }

        public void SetOptions(
            bool aecen, 
            bool aecwaiten, 
            bool buzzeren, 
            bool optionalstopen, 
            bool blockskipen, 
            bool refmoveen, 
            bool radoffseten, 
            bool machinelocken, 
            bool m02en,
            bool startnoen,
            short startno)
        {
            AecEn = aecen;
            AecWaitEn = aecwaiten;
            BuzzerEn = buzzeren;
            OptionalStopEn = optionalstopen;
            BlockSkipEn = blockskipen;
            RefMoveEn = refmoveen;
            RadOffsetEn = radoffseten;
            MachineLockEn = machinelocken;
            M02En = m02en;
            StartNoEn = startnoen;
            StartNo = startno;
        }
    }
}
