using System.IO;

namespace LibDat.Files
{
	public class Tags : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int HexU01 { get; set; }

		public Tags(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			HexU01 = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(HexU01);
		}

		public override int GetSize()
		{
			return 0x8;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}