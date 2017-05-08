using System;
using System.IO;

namespace LibDat.Files 
{
    public class BaseItemTypes : BaseDat
    {
        [Hidden]
        public int ItemTypeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ItemTypeStringData { get; set; }
        public int ItemClass { get; set; }
        public int U01 { get; set; }
        public int U02 { get; set; }
        [Hidden]
        public int NameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NameStringData { get; set; }
        [Hidden]
        public int InheritsFromStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString InheritsFromStringData { get; set; }
        public int MinimumItemLevel { get; set; }
        [Hidden]
        public Int64 FlavourTextKey { get; set; }
        [ExternalReference]
        public FlavourText FlavourTextRef { get; set; }
        [Hidden]
        public int ImplicitModsListLength { get; set; }
        [Hidden]
        public int ImplicitModsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List ImplicitModsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Mods> ImplicitModsListRef { get; set; }
        public int U06 { get; set; }
        public int DropRate { get; set; }
        public int U08 { get; set; }
        [Hidden]
        public Int64 SoundEffectKey { get; set; }
        [ExternalReference]
        public SoundEffects SoundEffectRef { get; set; }
        [Hidden]
        public Int64 DropPoolKey { get; set; }
        [ExternalReference]
        public DropPool DropPoolRef { get; set; }
        [Hidden]
        public int U11ListLength { get; set; }
        [Hidden]
        public int U11ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U11ListData { get; set; }
        [Hidden]
        public int U12ListLength { get; set; }
        [Hidden]
        public int U12ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U12ListData { get; set; }
        [Hidden]
        public int U13ListLength { get; set; }
        [Hidden]
        public int U13ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U13ListData { get; set; }
        [Hidden]
        public int U14ListLength { get; set; }
        [Hidden]
        public int U14ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U14ListData { get; set; }
        [Hidden]
        public int TagsListLength { get; set; }
        [Hidden]
        public int TagsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List TagsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Tags> TagsListRef { get; set; }
        public int U16 { get; set; }
        [Hidden]
        public Int64 VisualIdentityKey { get; set; }
        [ExternalReference]
        public ItemVisualIdentity VisualIdentityRef { get; set; }
        public int HexU18 { get; set; }
        [Hidden]
        public int LeagueRestrictionListLength { get; set; }
        [Hidden]
        public int LeagueRestrictionListOffset { get; set; }
        [ResourceOnly]
        public UInt32List LeagueRestrictionListData { get; set; }
        [Hidden]
        public Int64 RecipeAchievementKey { get; set; }
        [ExternalReference]
        public AchievementItems RecipeAchievementRef { get; set; }


        public BaseItemTypes(BinaryReader inStream)
        {
            ItemTypeStringOffset = inStream.ReadInt32();
            ItemClass = inStream.ReadInt32();
            U01 = inStream.ReadInt32();
            U02 = inStream.ReadInt32();
            NameStringOffset = inStream.ReadInt32();
            InheritsFromStringOffset = inStream.ReadInt32();
            MinimumItemLevel = inStream.ReadInt32();
            FlavourTextKey = inStream.ReadInt64();
            ImplicitModsListLength = inStream.ReadInt32();
            ImplicitModsListOffset = inStream.ReadInt32();
            U06 = inStream.ReadInt32();
            DropRate = inStream.ReadInt32();
            U08 = inStream.ReadInt32();
            SoundEffectKey = inStream.ReadInt64();
            DropPoolKey = inStream.ReadInt64();
            U11ListLength = inStream.ReadInt32();
            U11ListOffset = inStream.ReadInt32();
            U12ListLength = inStream.ReadInt32();
            U12ListOffset = inStream.ReadInt32();
            U13ListLength = inStream.ReadInt32();
            U13ListOffset = inStream.ReadInt32();
            U14ListLength = inStream.ReadInt32();
            U14ListOffset = inStream.ReadInt32();
            TagsListLength = inStream.ReadInt32();
            TagsListOffset = inStream.ReadInt32();
            U16 = inStream.ReadInt32();
            VisualIdentityKey = inStream.ReadInt64();
            HexU18 = inStream.ReadInt32();
            LeagueRestrictionListLength = inStream.ReadInt32();
            LeagueRestrictionListOffset = inStream.ReadInt32();
            RecipeAchievementKey = inStream.ReadInt64();

        }
        public override void Save(BinaryWriter outStream)
        {
            outStream.Write(ItemTypeStringOffset);
            outStream.Write(ItemClass);
            outStream.Write(U01);
            outStream.Write(U02);
            outStream.Write(NameStringOffset);
            outStream.Write(InheritsFromStringOffset);
            outStream.Write(MinimumItemLevel);
            outStream.Write(FlavourTextKey);
            outStream.Write(ImplicitModsListLength);
            outStream.Write(ImplicitModsListOffset);
            outStream.Write(U06);
            outStream.Write(DropRate);
            outStream.Write(U08);
            outStream.Write(SoundEffectKey);
            outStream.Write(DropPoolKey);
            outStream.Write(U11ListLength);
            outStream.Write(U11ListOffset);
            outStream.Write(U12ListLength);
            outStream.Write(U12ListOffset);
            outStream.Write(U13ListLength);
            outStream.Write(U13ListOffset);
            outStream.Write(U14ListLength);
            outStream.Write(U14ListOffset);
            outStream.Write(TagsListLength);
            outStream.Write(TagsListOffset);
            outStream.Write(U16);
            outStream.Write(VisualIdentityKey);
            outStream.Write(HexU18);
            outStream.Write(LeagueRestrictionListLength);
            outStream.Write(LeagueRestrictionListOffset);
            outStream.Write(RecipeAchievementKey);

        }

        public override int GetSize()
        {
            return 0x90;
        }

        public override string ToString()
        {
            return NameStringData.ToString() + " [" + Key.ToString() + "]";
        }

        public override string ToStringWiki()
        {
            if (InheritsFromStringData.ToString().Contains("/Gems/"))
                return "{{sl|" + NameStringData.ToString() + "}}";
            else
                return "[[" + NameStringData.ToString() + "]]";
        }
    }
}
