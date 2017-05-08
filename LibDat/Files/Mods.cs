using System;
using System.IO;

namespace LibDat.Files
{
	public class Mods : BaseDat
	{
		[Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int HexU00 { get; set; }
		// Generalized mod group
		//		group 45 = increased attack speed
		//		group 80 = local increased attack speed
		//public int Group { get; set; } // Removed as of latest patch
		public int Level { get; set; }
		public int Stat1Min { get; set; }
		public int Stat1Max { get; set; }
		public int Stat2Min { get; set; }
		public int Stat2Max { get; set; }
		public int Stat3Min { get; set; }
		public int Stat3Max { get; set; }
		public int Stat4Min { get; set; }
		public int Stat4Max { get; set; }
        [Hidden]
        public Int64 Stat1Key { get; set; }
        [ExternalReference]
        public Stats Stat1Ref { get; set; }
        [Hidden]
        public Int64 Stat2Key { get; set; }
        [ExternalReference]
        public Stats Stat2Ref { get; set; }
        [Hidden]
        public Int64 Stat3Key { get; set; }
        [ExternalReference]
        public Stats Stat3Ref { get; set; }
        [Hidden]
        public Int64 Stat4Key { get; set; }
        [ExternalReference]
        public Stats Stat4Ref { get; set; }
        // 1 - Item
		// 2 - Flask
		// 3 - Monster
		// 4 - Chest
		// 5 - Map
		public int Domain { get; set; }
		[Hidden]
		public int AffixStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AffixStringData { get; set; }
        // 1 - Prefix
		// 2 - Suffix
		// 3 - Cannot be generated
		public int GenerationType { get; set; }
        [Hidden]
		public int CorrectGroupStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CorrectGroupStringData { get; set; }
        [Hidden]
        public int TagsListLength { get; set; }
        [Hidden]
        public int TagsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List TagsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Tags> TagsListRef { get; set; }
        [Hidden]
        public int DropChanceListLength { get; set; }
        [Hidden]
        public int DropChanceListOffset { get; set; }
        [ResourceOnly]
        public UInt32List DropChanceListData { get; set; }
        [Hidden]
        public Int64 BuffKey { get; set; }
        [ExternalReference]
        public BuffDefinitions BuffRef { get; set; }
        public int U23 { get; set; } //Radius ?
        [Hidden]
        public int U24ListLength { get; set; }
        [Hidden]
        public int U24ListOffset { get; set; }
        [ResourceOnly]
        public UInt64List U24ListData { get; set; }
        [Hidden]
        public Int64 EffectKey { get; set; }
        [ExternalReference]
        public GrantedEffectsPerLevel EffectRef { get; set; }
        [Hidden]
        public int U27ListLength { get; set; }
        [Hidden]
        public int U27ListOffset { get; set; }
        // 1 = Mod affects the caster
        // 2 = Mod affects caster's allies
        // 3 = Mod affects caster's ennemies
        [ResourceOnly]
        public UInt32List U27ListData { get; set; }
        [Hidden]
        public int U29ListLength { get; set; }
        [Hidden]
        public int U29ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U29ListData { get; set; }
        [Hidden]
        public int U31StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U31StringData { get; set; }
        [Hidden]
        public Int64 AchievementKey { get; set; }
        [ExternalReference]
        public AchievementItems AchievementRef { get; set; }


		public Mods(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			HexU00 = inStream.ReadInt32();
			Level = inStream.ReadInt32();
			Stat1Min = inStream.ReadInt32();
			Stat1Max = inStream.ReadInt32();
			Stat2Min = inStream.ReadInt32();
			Stat2Max = inStream.ReadInt32();
			Stat3Min = inStream.ReadInt32();
			Stat3Max = inStream.ReadInt32();
			Stat4Min = inStream.ReadInt32();
			Stat4Max = inStream.ReadInt32();
			Stat1Key = inStream.ReadInt64();
			Stat2Key = inStream.ReadInt64();
			Stat3Key = inStream.ReadInt64();
			Stat4Key = inStream.ReadInt64();
			Domain = inStream.ReadInt32();
            AffixStringOffset = inStream.ReadInt32();
			GenerationType = inStream.ReadInt32();
			CorrectGroupStringOffset = inStream.ReadInt32();
			TagsListLength = inStream.ReadInt32();
			TagsListOffset = inStream.ReadInt32();
			DropChanceListLength = inStream.ReadInt32();
			DropChanceListOffset = inStream.ReadInt32();
			BuffKey = inStream.ReadInt64();
			U23 = inStream.ReadInt32();
			U24ListLength = inStream.ReadInt32();
			U24ListOffset = inStream.ReadInt32();
			EffectKey = inStream.ReadInt64();
			U27ListLength = inStream.ReadInt32();
			U27ListOffset = inStream.ReadInt32();
            U29ListLength = inStream.ReadInt32();
            U29ListOffset = inStream.ReadInt32();
            U31StringOffset = inStream.ReadInt32();
            AchievementKey = inStream.ReadInt64();

		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(HexU00);
			outStream.Write(Level);
			outStream.Write(Stat1Min);
			outStream.Write(Stat1Max);
			outStream.Write(Stat2Min);
			outStream.Write(Stat2Max);
			outStream.Write(Stat3Min);
			outStream.Write(Stat3Max);
			outStream.Write(Stat4Min);
			outStream.Write(Stat4Max);
			outStream.Write(Stat1Key);
			outStream.Write(Stat2Key);
			outStream.Write(Stat3Key);
			outStream.Write(Stat4Key);
			outStream.Write(Domain);
            outStream.Write(AffixStringOffset);
			outStream.Write(GenerationType);
			outStream.Write(CorrectGroupStringOffset);
			outStream.Write(TagsListLength);
			outStream.Write(TagsListOffset);
			outStream.Write(DropChanceListLength);
			outStream.Write(DropChanceListOffset);
			outStream.Write(BuffKey);
			outStream.Write(U23);
			outStream.Write(U24ListLength);
			outStream.Write(U24ListOffset);
			outStream.Write(EffectKey);
			outStream.Write(U27ListLength);
            outStream.Write(U27ListOffset);
            outStream.Write(U29ListLength);
            outStream.Write(U29ListOffset);
            outStream.Write(U31StringOffset);
            outStream.Write(AchievementKey);
		}

		public override int GetSize()
		{
			return 0xa4;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }

        public override string ToStringNoKey()
        {
            return CodeStringData.ToString();
        }
    }
}
