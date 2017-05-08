using System.IO;

namespace LibDat.Files
{
	public class ComponentAttributeRequirements : BaseDat
	{
        [Hidden]
        public int MetadataStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MetadataStringData { get; set; }
        public int StrReq { get; set; }
		public int DexReq { get; set; }
		public int IntReq { get; set; }

		public ComponentAttributeRequirements(BinaryReader inStream)
		{
            MetadataStringOffset = inStream.ReadInt32();
			StrReq = inStream.ReadInt32();
			DexReq = inStream.ReadInt32();
			IntReq = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(MetadataStringOffset);
			outStream.Write(StrReq);
			outStream.Write(DexReq);
			outStream.Write(IntReq);
		}

		public override int GetSize()
		{
			return 0x10;
		}
	}
}