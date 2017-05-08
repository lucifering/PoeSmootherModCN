using System;
using System.IO;

namespace LibDat.Files
{
	public class ShrineBuffs : BaseDat
	{
		[Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int BuffsAmountsListLength { get; set; }
        [Hidden]
        public int BuffsAmountsListOffset { get; set; }
        [ResourceOnly]
        public Int32List BuffsAmountsListData { get; set; }
        [Hidden]
        public Int64 BuffKey { get; set; }
        [ExternalReference]
        public BuffDefinitions BuffRef { get; set; }
        [Hidden]
        public Int64 VisualKey { get; set; }
        [ExternalReference]
        public BuffVisuals VisualRef { get; set; }

		public ShrineBuffs()
		{
			
		}
		public ShrineBuffs(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			BuffsAmountsListLength = inStream.ReadInt32();
			BuffsAmountsListOffset = inStream.ReadInt32();
            BuffKey = inStream.ReadInt64();
			VisualKey = inStream.ReadInt64();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(BuffsAmountsListLength);
			outStream.Write(BuffsAmountsListOffset);
			outStream.Write(BuffKey);
			outStream.Write(VisualKey);
		}

		public override int GetSize()
		{
			return 0x1c;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}