using System.IO;

namespace LibDat.Files
{
	public class SoundEffects : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int SoundFileStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString SoundFileStringData { get; set; }

		public SoundEffects(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            SoundFileStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(SoundFileStringOffset);
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