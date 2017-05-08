using System.IO;

namespace LibDat.Files
{
	public class Difficulties : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int ElementalDamageMultiplier { get; set; }
		public int MinLevel { get; set; }
        [Hidden]
        public int LabelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LabelStringData { get; set; }
        [Hidden]
        public int PrefixStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString PrefixStringData { get; set; }

		public Difficulties(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            ElementalDamageMultiplier = inStream.ReadInt32();
			MinLevel = inStream.ReadInt32();
            LabelStringOffset = inStream.ReadInt32();
            PrefixStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(ElementalDamageMultiplier);
			outStream.Write(MinLevel);
            outStream.Write(LabelStringOffset);
            outStream.Write(PrefixStringOffset);
		}

		public override int GetSize()
		{
			return 0x14;
		}

        public override string ToString()
        {
            return LabelStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}