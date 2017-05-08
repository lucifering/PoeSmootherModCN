using System.IO;

namespace LibDat.Files
{
	public class ChestClusters : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int U00ListLength { get; set; }
        [Hidden]
        public int U00ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List U00ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Chests> U00ListRef { get; set; }
        [Hidden]
        public int U01ListLength { get; set; }
        [Hidden]
        public int U01ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U01ListData { get; set; }
        public int U03 { get; set; }
		public int U04 { get; set; }
		public int U05 { get; set; }

		public ChestClusters(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            U00ListLength = inStream.ReadInt32();
			U00ListOffset = inStream.ReadInt32();
			U01ListLength = inStream.ReadInt32();
			U01ListOffset = inStream.ReadInt32();
			U03 = inStream.ReadInt32();
			U04 = inStream.ReadInt32();
			U05 = inStream.ReadInt32();
		}
		public override void Save(System.IO.BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(U00ListLength);
			outStream.Write(U00ListOffset);
			outStream.Write(U01ListLength);
			outStream.Write(U01ListOffset);
			outStream.Write(U03);
			outStream.Write(U04);
			outStream.Write(U05);
		}

		public override int GetSize()
		{
			return 0x20;
		}
	}
}
