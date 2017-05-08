using System.IO;

namespace LibDat.Files
{
	public class Words : BaseDat
	{
		public int Wordlist { get; set; }
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int TagsListLength { get; set; }
        [Hidden]
        public int TagsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List TagsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Tags> TagsListRef { get; set; }
        [Hidden]
        public int U04ListLength { get; set; }
        [Hidden]
        public int U04ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U04ListData { get; set; }
        public int HexU06 { get; set; }

		public Words(BinaryReader inStream)
		{
			Wordlist = inStream.ReadInt32();
            CodeStringOffset = inStream.ReadInt32();
			TagsListLength = inStream.ReadInt32();
            TagsListOffset = inStream.ReadInt32();
			U04ListLength = inStream.ReadInt32();
			U04ListOffset = inStream.ReadInt32();
			HexU06 = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(Wordlist);
            outStream.Write(CodeStringOffset);
			outStream.Write(TagsListLength);
            outStream.Write(TagsListOffset);
			outStream.Write(U04ListLength);
			outStream.Write(U04ListOffset);
			outStream.Write(HexU06);
		}

		public override int GetSize()
		{
			return 0x1C;
		}
	}
}