using System;
using System.IO;

namespace LibDat.Files
{
	public class GemTags : BaseDat
	{
        [Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
		public int LabelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LabelStringData { get; set; }

		public GemTags(BinaryReader inStream)
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
			return 0x8;
		}

        public override string ToString()
        {
            if (LabelStringData.ToString() != String.Empty)
                return LabelStringData.ToString() + " [" + Key.ToString() + "]";
            else
                return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}