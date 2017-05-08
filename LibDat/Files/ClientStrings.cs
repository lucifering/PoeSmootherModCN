using System.IO;

namespace LibDat.Files
{
	public class ClientStrings : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int LabelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LabelStringData { get; set; }

		public ClientStrings()
		{
			
		}
		public ClientStrings(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            LabelStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(LabelStringOffset);
		}

		public override int GetSize()
		{
			return 0x08;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}