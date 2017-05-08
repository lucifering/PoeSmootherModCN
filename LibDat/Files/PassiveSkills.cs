using System;
using System.IO;

namespace LibDat.Files
{
	public class PassiveSkills : BaseDat
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
        public int StatsListLength { get; set; }
        [Hidden]
        public int StatsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List StatsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Stats> StatsListRef { get; set; }
        public int Stat1Amount { get; set; }
		public int Stat2Amount { get; set; }
		public int Stat3Amount { get; set; }
		public int Stat4Amount { get; set; }
		public int HexU08 { get; set; }
        [Hidden]
        public int NameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NameStringData { get; set; }
        [Hidden]
        public int CharacterStartingPointListLength { get; set; }
        [Hidden]
        public int CharacterStartingPointListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List CharacterStartingPointListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Characters> CharacterStartingPointListRef { get; set; }
        public bool IsKeystone { get; set; }
		public bool IsNotable { get; set; }
        [Hidden]
        public int FlavourTextStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString FlavourTextStringData { get; set; }
        public bool IsJustIcon { get; set; }
        [Hidden]
        public Int64 AchievementKey { get; set; }
        [ExternalReference]
        public AchievementItems AchievementRef { get; set; }

		public PassiveSkills(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            IconStringOffset = inStream.ReadInt32();
			StatsListLength = inStream.ReadInt32();
			StatsListOffset = inStream.ReadInt32();
			Stat1Amount = inStream.ReadInt32();
			Stat2Amount = inStream.ReadInt32();
			Stat3Amount = inStream.ReadInt32();
			Stat4Amount = inStream.ReadInt32();
			HexU08 = inStream.ReadInt32();
            NameStringOffset = inStream.ReadInt32();
			CharacterStartingPointListLength = inStream.ReadInt32();
			CharacterStartingPointListOffset = inStream.ReadInt32();
			IsKeystone = inStream.ReadBoolean();
			IsNotable = inStream.ReadBoolean();
            FlavourTextStringOffset = inStream.ReadInt32();
			IsJustIcon = inStream.ReadBoolean();
            AchievementKey = inStream.ReadInt64();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(IconStringOffset);
			outStream.Write(StatsListLength);
			outStream.Write(StatsListOffset);
			outStream.Write(Stat1Amount);
			outStream.Write(Stat2Amount);
			outStream.Write(Stat3Amount);
			outStream.Write(Stat4Amount);
			outStream.Write(HexU08);
            outStream.Write(NameStringOffset);
			outStream.Write(CharacterStartingPointListLength);
			outStream.Write(CharacterStartingPointListOffset);
			outStream.Write(IsKeystone);
			outStream.Write(IsNotable);
            outStream.Write(FlavourTextStringOffset);
			outStream.Write(IsJustIcon);
            outStream.Write(AchievementKey);
		}

		public override int GetSize()
		{
			return 0x3F;
		}
	}
}