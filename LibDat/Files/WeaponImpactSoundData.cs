using System.IO;

namespace LibDat.Files
{
	public class WeaponImpactSoundData : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int U01 { get; set; }
		public int U02 { get; set; }
		public int U03 { get; set; }
		public int U04 { get; set; }
		public int U05 { get; set; }

		public WeaponImpactSoundData()
		{
			
		}
		public WeaponImpactSoundData(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			U01 = inStream.ReadInt32();
			U02 = inStream.ReadInt32();
			U03 = inStream.ReadInt32();
			U04 = inStream.ReadInt32();
			U05 = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(U01);
			outStream.Write(U02);
			outStream.Write(U03);
			outStream.Write(U04);
			outStream.Write(U05);
		}

		public override int GetSize()
		{
			return 0x18;
		}
	}
}