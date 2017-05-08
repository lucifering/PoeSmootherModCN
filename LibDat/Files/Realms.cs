using System.IO;

namespace LibDat.Files
{
	public class Realms : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int LabelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LabelStringData { get; set; }
        [Hidden]
        public int UrlStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString UrlStringData { get; set; }
        public bool Flag0 { get; set; }

		public Realms(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            LabelStringOffset = inStream.ReadInt32();
            UrlStringOffset = inStream.ReadInt32();
			Flag0 = inStream.ReadBoolean();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(LabelStringOffset);
            outStream.Write(UrlStringOffset);
			outStream.Write(Flag0);
		}

		public override int GetSize()
		{
			return 0xD;
		}
	}
}