using System.IO;

namespace LibDat.Files
{
	public class DropPool : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int Amount { get; set; }

		public DropPool(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			Amount = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(Amount);
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