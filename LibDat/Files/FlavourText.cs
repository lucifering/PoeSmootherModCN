using System.IO;

namespace LibDat.Files
{
	public class FlavourText : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int HexHash0 { get; set; }
        [Hidden]
        public int TextStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString TextStringData { get; set; }

		public FlavourText(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			HexHash0 = inStream.ReadInt32();
            TextStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(HexHash0);
            outStream.Write(TextStringOffset);
		}

		public override int GetSize()
		{
			return 0xC;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}