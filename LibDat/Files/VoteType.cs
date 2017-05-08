using System.IO;

namespace LibDat.Files
{
	public class VoteType : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int TextStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString TextStringData { get; set; }
        [Hidden]
        public int HelpStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString HelpStringData { get; set; }
        [Hidden]
        public int KillStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString KillStringData { get; set; }
        public int U00 { get; set; }

		public VoteType(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            TextStringOffset = inStream.ReadInt32();
            HelpStringOffset = inStream.ReadInt32();
            KillStringOffset = inStream.ReadInt32();
			U00 = inStream.ReadInt32();
		}
		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(TextStringOffset);
            outStream.Write(HelpStringOffset);
            outStream.Write(KillStringOffset);
			outStream.Write(U00);
		}

		public override int GetSize()
		{
			return 0x14;
		}
	}
}
