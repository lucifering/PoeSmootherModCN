using System.IO;

namespace LibDat.Files
{
	public class ItemVisualEffect : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int DaggerStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString DaggerStringData { get; set; }
        [Hidden]
        public int BowStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString BowStringData { get; set; }
        [Hidden]
        public int Mace1hStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString Mace1hStringData { get; set; }
        [Hidden]
        public int Sword1hStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString Sword1hStringData { get; set; }
        [Hidden]
        public int U05StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U05StringData { get; set; }
        [Hidden]
        public int Sword2hStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString Sword2hStringData { get; set; }
        [Hidden]
        public int StaffStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString StaffStringData { get; set; }
        public int HexU00 { get; set; }
        [Hidden]
        public int Mace2hStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString Mace2hStringData { get; set; }
        [Hidden]
        public int Axe1hStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString Axe1hStringData { get; set; }
        [Hidden]
        public int Axe2hStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString Axe2hStringData { get; set; }
        [Hidden]
        public int WandStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString WandStringData { get; set; }
        [Hidden]
        public int StrikeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString StrikeStringData { get; set; }
        public bool Flag1 { get; set; }

		public ItemVisualEffect(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            DaggerStringOffset = inStream.ReadInt32();
            BowStringOffset = inStream.ReadInt32();
            Mace1hStringOffset = inStream.ReadInt32();
            Sword1hStringOffset = inStream.ReadInt32();
            U05StringOffset = inStream.ReadInt32();
            Sword2hStringOffset = inStream.ReadInt32();
            StaffStringOffset = inStream.ReadInt32();
			HexU00 = inStream.ReadInt32();
            Mace2hStringOffset = inStream.ReadInt32();
            Axe1hStringOffset = inStream.ReadInt32();
            Axe2hStringOffset = inStream.ReadInt32();
            WandStringOffset = inStream.ReadInt32();
            StrikeStringOffset = inStream.ReadInt32();
			Flag1 = inStream.ReadBoolean();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(DaggerStringOffset);
            outStream.Write(BowStringOffset);
            outStream.Write(Mace1hStringOffset);
            outStream.Write(Sword1hStringOffset);
            outStream.Write(U05StringOffset);
            outStream.Write(Sword2hStringOffset);
            outStream.Write(StaffStringOffset);
			outStream.Write(HexU00);
            outStream.Write(Mace2hStringOffset);
            outStream.Write(Axe1hStringOffset);
            outStream.Write(Axe2hStringOffset);
            outStream.Write(WandStringOffset);
            outStream.Write(StrikeStringOffset);
			outStream.Write(Flag1);
		}

		public override int GetSize()
		{
			return 0x39;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}