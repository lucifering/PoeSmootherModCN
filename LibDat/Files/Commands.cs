using System.IO;

namespace LibDat.Files
{
	public class Commands : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int CommandStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CommandStringData { get; set; }
        public bool Flag0 { get; set; }

		public Commands()
		{
			
		}
		public Commands(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            CommandStringOffset = inStream.ReadInt32();
			Flag0 = inStream.ReadBoolean();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(CommandStringOffset);
			outStream.Write(Flag0);
		}

		public override int GetSize()
		{
			return 0x09;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}