using System.IO;

namespace LibDat.Files
{
	public class MiscObjects : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int MetadataStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MetadataStringData { get; set; }

		public MiscObjects(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            MetadataStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(MetadataStringOffset);
		}

		public override int GetSize()
		{
			return 0x8;
		}
	}
}