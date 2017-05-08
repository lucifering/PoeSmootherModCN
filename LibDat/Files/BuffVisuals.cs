using System;
using System.IO;

namespace LibDat.Files
{
	public class BuffVisuals : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int IconStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString IconStringData { get; set; }
        [Hidden]
        public int EffectAStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString EffectAStringData { get; set; }
        [Hidden]
        public int EffectBStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString EffectBStringData { get; set; }
        [Hidden]
        public Int64 AnimationKey { get; set; }
        [ExternalReference]
        public MiscAnimated AnimationRef { get; set; }
        public Int64 U05 { get; set; }

		public BuffVisuals()
		{
			
		}

		public BuffVisuals(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			IconStringOffset = inStream.ReadInt32();
			EffectAStringOffset = inStream.ReadInt32();
			EffectBStringOffset = inStream.ReadInt32();
			AnimationKey = inStream.ReadInt64();
			U05 = inStream.ReadInt64();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(IconStringOffset);
			outStream.Write(EffectAStringOffset);
			outStream.Write(EffectBStringOffset);
			outStream.Write(AnimationKey);
			outStream.Write(U05);
		}

		public override int GetSize()
		{
			return 0x20;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}