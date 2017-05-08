using System.IO;

namespace LibDat.Files
{
	public class GameConstants : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int ConstantValue { get; set; }

		public GameConstants(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			ConstantValue = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(ConstantValue);
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