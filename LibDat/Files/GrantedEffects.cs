using System.IO;

namespace LibDat.Files
{
	public class GrantedEffects : BaseDat
	{
		[Hidden]
		public int EffectNameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString EffectNameStringData { get; set; }
        public bool IsSupport { get; set; }
        [Hidden]
        public int U00ListLength { get; set; }
        [Hidden]
        public int U00ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U00ListData { get; set; }
        [Hidden]
        public int SupportLetterStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString SupportLetterStringData { get; set; }
        public int GemColor { get; set; }
        [Hidden]
        public int U02ListLength { get; set; }
        [Hidden]
        public int U02ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U02ListData { get; set; }
        [Hidden]
        public int U03ListLength { get; set; }
        [Hidden]
        public int U03ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U03ListData { get; set; }

		public GrantedEffects(BinaryReader inStream)
		{
			EffectNameStringOffset = inStream.ReadInt32();
			IsSupport = inStream.ReadBoolean();
			U00ListLength = inStream.ReadInt32();
			U00ListOffset = inStream.ReadInt32();
			SupportLetterStringOffset = inStream.ReadInt32();
			GemColor = inStream.ReadInt32();
			U02ListLength = inStream.ReadInt32();
			U02ListOffset = inStream.ReadInt32();
			U03ListLength = inStream.ReadInt32();
			U03ListOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(EffectNameStringOffset);
			outStream.Write(IsSupport);
			outStream.Write(U00ListLength);
			outStream.Write(U00ListOffset);
			outStream.Write(SupportLetterStringOffset);
			outStream.Write(GemColor);
			outStream.Write(U02ListLength);
			outStream.Write(U02ListOffset);
			outStream.Write(U03ListLength);
			outStream.Write(U03ListOffset);
		}

		public override int GetSize()
		{
			return 0x25;
		}

        public override string ToString()
        {
            return EffectNameStringData.ToString() + " [" + Key.ToString() + "]";
        }

        public override string ToStringNoKey()
        {
            return EffectNameStringData.ToString();
        }
    }
}