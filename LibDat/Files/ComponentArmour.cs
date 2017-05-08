using System.IO;

namespace LibDat.Files
{
	public class ComponentArmour : BaseDat
	{
        [Hidden]
        public int MetadataStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MetadataStringData { get; set; }
        public int ArmourRating { get; set; }
		public int EvasionRating { get; set; }
		public int EnergyShield { get; set; }

		public ComponentArmour(BinaryReader inStream)
		{
            MetadataStringOffset = inStream.ReadInt32();
			ArmourRating = inStream.ReadInt32();
			EvasionRating = inStream.ReadInt32();
			EnergyShield = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(MetadataStringOffset);
			outStream.Write(ArmourRating);
			outStream.Write(EvasionRating);
			outStream.Write(EnergyShield);
		}

		public override int GetSize()
		{
			return 0x10;
		}
	}
}