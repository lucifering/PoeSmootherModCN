using System.IO;

namespace LibDat.Files
{
	public class GlobalAudioConfig : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int Value { get; set; }

		public GlobalAudioConfig()
		{
			
		}
		public GlobalAudioConfig(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			Value = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(Value);
		}

		public override int GetSize()
		{
			return 0x08;
		}
	}
}