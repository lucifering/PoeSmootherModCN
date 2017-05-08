using System.IO;

namespace LibDat.Files
{
	public class ShrineSounds : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int StereoStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString StereoStringData { get; set; }
        [Hidden]
        public int MonoStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MonoStringData { get; set; }

		public ShrineSounds()
		{
			
		}
		public ShrineSounds(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            StereoStringOffset = inStream.ReadInt32();
            MonoStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(StereoStringOffset);
            outStream.Write(MonoStringOffset);
		}

		public override int GetSize()
		{
			return 0x0C;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}