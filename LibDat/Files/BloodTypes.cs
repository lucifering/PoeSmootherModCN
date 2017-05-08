using System;
using System.IO;

namespace LibDat.Files
{
	public class BloodTypes : BaseDat
	{
		[Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
		public int Particle0StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString Particle0StringData { get; set; }
        [Hidden]
		public int Particle1StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString Particle1StringData { get; set; }
        [Hidden]
		public int CriticalStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CriticalStringData { get; set; }
        public Int64 U04 { get; set; }
        [Hidden]
		public int ShopParticle0StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ShopParticle0StringData { get; set; }
        [Hidden]
		public int ShopParticle1StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ShopParticle1StringData { get; set; }
        [Hidden]
		public int ShopCriticalStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ShopCriticalStringData { get; set; }
        public Int64 U09 { get; set; }
        [Hidden]
        public int U11ListLength { get; set; }
        [Hidden]
        public int U11ListOffset { get; set; }
        [ResourceOnly]
        public UInt64List U11ListData { get; set; }
        public Int64 U13 { get; set; }

		public BloodTypes(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			Particle0StringOffset = inStream.ReadInt32();
			Particle1StringOffset = inStream.ReadInt32();
			CriticalStringOffset = inStream.ReadInt32();
			U04 = inStream.ReadInt64();
			ShopParticle0StringOffset = inStream.ReadInt32();
			ShopParticle1StringOffset = inStream.ReadInt32();
			ShopCriticalStringOffset = inStream.ReadInt32();
			U09 = inStream.ReadInt64();
			U11ListLength = inStream.ReadInt32();
			U11ListOffset = inStream.ReadInt32();
			U13 = inStream.ReadInt64();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(Particle0StringOffset);
			outStream.Write(Particle1StringOffset);
			outStream.Write(CriticalStringOffset);
			outStream.Write(U04);
			outStream.Write(ShopParticle0StringOffset);
			outStream.Write(ShopParticle1StringOffset);
			outStream.Write(ShopCriticalStringOffset);
			outStream.Write(U09);
			outStream.Write(U11ListLength);
			outStream.Write(U11ListOffset);
			outStream.Write(U13);
		}

		public override int GetSize()
		{
			return 0x3C;
		}
	}
}
