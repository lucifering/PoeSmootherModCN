using System;
using System.IO;

namespace LibDat.Files
{
	public class Shrines : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int U00 { get; set; }
        [Hidden]
        public int LabelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LabelStringData { get; set; }
        public bool ChargesShared { get; set; } // guess
        [Hidden]
        public Int64 PlayerBuffKey { get; set; }
        [ExternalReference]
        public ShrineBuffs PlayerBuffRef { get; set; }
        public int U04 { get; set; }
        public int U05 { get; set; }
        [Hidden]
        public int DescriptionStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString DescriptionStringData { get; set; }
        [Hidden]
        public Int64 MonsterBuffKey { get; set; }
        [ExternalReference]
        public ShrineBuffs MonsterBuffRef { get; set; }
        [Hidden]
        public Int64 VarietyKey { get; set; }
        [ExternalReference]
        public MonsterVarieties VarietyRef { get; set; }
        [Hidden]
        public Int64 VarietyPlayerKey { get; set; }
        [ExternalReference]
        public MonsterVarieties VarietyPlayerRef { get; set; }
        public int U10 { get; set; }
        public int U11 { get; set; }
        [Hidden]
        public Int64 SoundKey { get; set; }
        [ExternalReference]
        public ShrineSounds SoundRef { get; set; }
        public bool U14 { get; set; }
        [Hidden]
        public Int64 AchievementKey { get; set; }
        [ExternalReference]
        public AchievementItems AchievementRef { get; set; }

		public Shrines(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            U00 = inStream.ReadInt32();
            LabelStringOffset = inStream.ReadInt32();
            ChargesShared = inStream.ReadBoolean();
            PlayerBuffKey = inStream.ReadInt64();
            U04 = inStream.ReadInt32();
            U05 = inStream.ReadInt32();
            DescriptionStringOffset = inStream.ReadInt32();
            MonsterBuffKey = inStream.ReadInt64();
            VarietyKey = inStream.ReadInt64();
            VarietyPlayerKey = inStream.ReadInt64();
            U10 = inStream.ReadInt32();
            U11 = inStream.ReadInt32();
            SoundKey = inStream.ReadInt64();
            U14 = inStream.ReadBoolean();
            AchievementKey = inStream.ReadInt64();
		}
		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(U00);
            outStream.Write(LabelStringOffset);
            outStream.Write(ChargesShared);
            outStream.Write(PlayerBuffKey);
            outStream.Write(U04);
            outStream.Write(U05);
            outStream.Write(DescriptionStringOffset);
            outStream.Write(MonsterBuffKey);
            outStream.Write(VarietyKey);
            outStream.Write(VarietyPlayerKey);
            outStream.Write(U10);
            outStream.Write(U11);
            outStream.Write(SoundKey);
            outStream.Write(U14);
            outStream.Write(AchievementKey);
		}

		public override int GetSize()
		{
			return 0x52;
		}
	}
}
