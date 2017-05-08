using System;
using System.IO;

namespace LibDat.Files
{
	public class MonsterVarieties : BaseDat
	{
        [Hidden]
        public int MonsterMetadataStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MonsterMetadataStringData { get; set; }
        [Hidden]
        public Int64 MonsterTypesKey { get; set; }
        [ExternalReference]
        public MonsterTypes MonsterTypesRef { get; set; }
        public int U03 { get; set; }
		public int ObjectSize { get; set; } // ?
		public int MinimumAttackDistance { get; set; }
		public int MaximumAttackDistance { get; set; }
		[Hidden]
		public int ActorStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ActorStringData { get; set; }
        [Hidden]
		public int AnimatedObjectStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AnimatedObjectStringData { get; set; }
        [Hidden]
		public int BaseMonsterTypeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString BaseMonsterTypeStringData { get; set; }
        [Hidden]
        public int ModsListLength { get; set; }
        [Hidden]
        public int ModsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List ModsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Mods> ModsListRef { get; set; }
        public int U12 { get; set; } // rarity_bias ?
        [Hidden]
        public int U13StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U13StringData { get; set; }
        [Hidden]
        public int U14StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U14StringData { get; set; }
        [Hidden]
        public int SegmentsStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString SegmentsStringData { get; set; }
        public int U16 { get; set; }
		public int U17 { get; set; }
		public int U18 { get; set; }
		public int U19 { get; set; }
		public int U20 { get; set; }
		[Hidden]
		public int U21StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U21StringData { get; set; }
        public int U22 { get; set; }
        [Hidden]
        public int MonsterTagsListLength { get; set; }
        [Hidden]
        public int MonsterTagsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List MonsterTagsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Tags> MonsterTagsListRef { get; set; }
        public int XpMultiplier { get; set; }
        [Hidden]
        public int U26ListLength { get; set; }
        [Hidden]
        public int U26ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U26ListData { get; set; }
        public int U28 { get; set; }
		public int U29 { get; set; } // scale ?
		public int U30 { get; set; }
		public int U31 { get; set; }
		public int U32 { get; set; }
        [Hidden]
        public int EffectsListLength { get; set; }
        [Hidden]
        public int EffectsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List EffectsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<GrantedEffects> EffectsListRef { get; set; }
        [Hidden]
		public int U35StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U35StringData { get; set; }
        [Hidden]
        public int ImplicitModsListLength { get; set; }
        [Hidden]
        public int ImplicitModsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List ImplicitModsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Mods> ImplicitModsListRef { get; set; }
        [Hidden]
        public int U38StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U38StringData { get; set; }
        public int U39 { get; set; }
		public int U40 { get; set; }
		[Hidden]
		public int NameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NameStringData { get; set; }
        public int DamageMultiplier { get; set; }
		public int HpMultiplier { get; set; }
		public int U44 { get; set; }
        [Hidden]
        public int MainHandListLength { get; set; }
        [Hidden]
        public int MainHandListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List MainHandListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<ItemVisualIdentity> MainHandListRef { get; set; }
        [Hidden]
        public int OffHandListLength { get; set; }
        [Hidden]
        public int OffHandListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List OffHandListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<ItemVisualIdentity> OffHandListRef { get; set; }
        [Hidden]
        public Int64 QuiverKey { get; set; }
        [ExternalReference]
        public ItemVisualIdentity QuiverRef { get; set; }
        public int U51 { get; set; }
		public int U52 { get; set; }
		public Int64 U53 { get; set; }
		public Int64 U55 { get; set; }
		public int U57 { get; set; }
        [Hidden]
        public Int64 AchievementKey { get; set; }
        [ExternalReference]
        public AchievementItems AchievementRef { get; set; }
        [Hidden]
        public int Mods2ListLength { get; set; }
        [Hidden]
        public int Mods2ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Mods2ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Mods> Mods2ListRef { get; set; }
        [Hidden]
        public Int64 RareAchievementKey { get; set; }
        [ExternalReference]
        public AchievementItems RareAchievementRef { get; set; }

		public MonsterVarieties(BinaryReader inStream)
		{
			MonsterMetadataStringOffset = inStream.ReadInt32();
			MonsterTypesKey = inStream.ReadInt64();
			U03 = inStream.ReadInt32();
			ObjectSize = inStream.ReadInt32();
			MinimumAttackDistance = inStream.ReadInt32();
			MaximumAttackDistance = inStream.ReadInt32();
			ActorStringOffset = inStream.ReadInt32();
			AnimatedObjectStringOffset = inStream.ReadInt32();
			BaseMonsterTypeStringOffset = inStream.ReadInt32();
			ModsListLength = inStream.ReadInt32();
			ModsListOffset = inStream.ReadInt32();
			U12 = inStream.ReadInt32();
			U13StringOffset = inStream.ReadInt32();
			U14StringOffset = inStream.ReadInt32();
			SegmentsStringOffset = inStream.ReadInt32();
			U16 = inStream.ReadInt32();
			U17 = inStream.ReadInt32();
			U18 = inStream.ReadInt32();
			U19 = inStream.ReadInt32();
			U20 = inStream.ReadInt32();
            U21StringOffset = inStream.ReadInt32();
			U22 = inStream.ReadInt32();
			MonsterTagsListLength = inStream.ReadInt32();
			MonsterTagsListOffset = inStream.ReadInt32();
			XpMultiplier = inStream.ReadInt32();
			U26ListLength = inStream.ReadInt32();
			U26ListOffset = inStream.ReadInt32();
			U28 = inStream.ReadInt32();
			U29 = inStream.ReadInt32();
			U30 = inStream.ReadInt32();
			U31 = inStream.ReadInt32();
			U32 = inStream.ReadInt32();
			EffectsListLength = inStream.ReadInt32();
			EffectsListOffset = inStream.ReadInt32();
			U35StringOffset = inStream.ReadInt32();
			ImplicitModsListLength = inStream.ReadInt32();
			ImplicitModsListOffset = inStream.ReadInt32();
			U38StringOffset = inStream.ReadInt32();
			U39 = inStream.ReadInt32();
			U40 = inStream.ReadInt32();
			NameStringOffset = inStream.ReadInt32();
			DamageMultiplier = inStream.ReadInt32();
			HpMultiplier = inStream.ReadInt32();
			U44 = inStream.ReadInt32();
            MainHandListLength = inStream.ReadInt32();
            MainHandListOffset = inStream.ReadInt32();
			OffHandListLength = inStream.ReadInt32();
            OffHandListOffset = inStream.ReadInt32();
			QuiverKey = inStream.ReadInt64();
			U51 = inStream.ReadInt32();
			U52 = inStream.ReadInt32();
			U53 = inStream.ReadInt64();
			U55 = inStream.ReadInt64();
			U57 = inStream.ReadInt32();
			AchievementKey = inStream.ReadInt64();
			Mods2ListLength = inStream.ReadInt32();
			Mods2ListOffset = inStream.ReadInt32();
            RareAchievementKey = inStream.ReadInt64();
        }

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(MonsterMetadataStringOffset);
			outStream.Write(MonsterTypesKey);
			outStream.Write(U03);
			outStream.Write(ObjectSize);
			outStream.Write(MinimumAttackDistance);
			outStream.Write(MaximumAttackDistance);
			outStream.Write(ActorStringOffset);
			outStream.Write(AnimatedObjectStringOffset);
			outStream.Write(BaseMonsterTypeStringOffset);
			outStream.Write(ModsListLength);
			outStream.Write(ModsListOffset);
			outStream.Write(U12);
			outStream.Write(U13StringOffset);
			outStream.Write(U14StringOffset);
			outStream.Write(SegmentsStringOffset);
			outStream.Write(U16);
			outStream.Write(U17);
			outStream.Write(U18);
			outStream.Write(U19);
			outStream.Write(U20);
            outStream.Write(U21StringOffset);
			outStream.Write(U22);
			outStream.Write(MonsterTagsListLength);
			outStream.Write(MonsterTagsListOffset);
			outStream.Write(XpMultiplier);
			outStream.Write(U26ListLength);
			outStream.Write(U26ListOffset);
			outStream.Write(U28);
			outStream.Write(U29);
			outStream.Write(U30);
			outStream.Write(U31);
			outStream.Write(U32);
			outStream.Write(EffectsListLength);
			outStream.Write(EffectsListOffset);
			outStream.Write(U35StringOffset);
			outStream.Write(ImplicitModsListLength);
			outStream.Write(ImplicitModsListOffset);
			outStream.Write(U38StringOffset);
			outStream.Write(U39);
			outStream.Write(U40);
			outStream.Write(NameStringOffset);
			outStream.Write(DamageMultiplier);
			outStream.Write(HpMultiplier);
			outStream.Write(U44);
            outStream.Write(MainHandListLength);
            outStream.Write(MainHandListOffset);
			outStream.Write(OffHandListLength);
            outStream.Write(OffHandListOffset);
			outStream.Write(QuiverKey);
			outStream.Write(U51);
			outStream.Write(U52);
			outStream.Write(U53);
			outStream.Write(U55);
			outStream.Write(U57);
			outStream.Write(AchievementKey);
			outStream.Write(Mods2ListLength);
            outStream.Write(Mods2ListOffset);
            outStream.Write(RareAchievementKey);
		}

		public override int GetSize()
		{
			return 0x100;
		}

        public override string ToString()
        {
            return NameStringData.ToString() + " [" + Key.ToString() + "]";
        }

        public override string ToStringWiki()
        {
            return "[[" + NameStringData.ToString() + "]]";
        }
    }
}
