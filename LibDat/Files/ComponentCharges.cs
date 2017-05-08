using System.IO;

namespace LibDat.Files
{
	public class ComponentCharges : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int MaxCharge { get; set; }
		public int ConsumptionPerUse { get; set; }

		public ComponentCharges(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			MaxCharge = inStream.ReadInt32();
			ConsumptionPerUse = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(MaxCharge);
			outStream.Write(ConsumptionPerUse);
		}

		public override int GetSize()
		{
			return 0xC;
		}
	}
}