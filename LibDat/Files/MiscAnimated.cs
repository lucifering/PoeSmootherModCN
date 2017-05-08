using System.IO;

namespace LibDat.Files
{
	public class MiscAnimated : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int AnimatedObjectStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AnimatedObjectStringData { get; set; }

		public MiscAnimated(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            AnimatedObjectStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(AnimatedObjectStringOffset);
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