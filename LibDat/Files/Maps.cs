using System;
using System.IO;

namespace LibDat.Files
{
	public class Maps : BaseDat
	{
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        [Hidden]
        public Int64 AreaNormalKey { get; set; }
        [ExternalReference]
        public WorldAreas AreaNormalRef { get; set; }
        [Hidden]
        public Int64 AreaUniqueKey { get; set; }
        [ExternalReference]
        public WorldAreas AreaUniqueRef { get; set; }
        [Hidden]
        public Int64 UpgradesToKey { get; set; }
        [ExternalReference]
        public BaseItemTypes UpgradesToRef { get; set; }
        [Hidden]
        public int ExtraPacksListLength { get; set; }
        [Hidden]
        public int ExtraPacksListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List ExtraPacksListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<MonsterPacks> ExtraPacksListRef { get; set; }
        [Hidden]
        public Int64 AchievementKey { get; set; }
        [ExternalReference]
        public AchievementItems AchievementRef { get; set; }
        [Hidden]
        public int GuildLetterNormalStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString GuildLetterNormalStringData { get; set; }
        [Hidden]
        public int GuildLetterUniqueStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString GuildLetterUniqueStringData { get; set; }
        [Hidden]
        public int UpgradesFromListLength { get; set; }
        [Hidden]
        public int UpgradesFromListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List UpgradesFromListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> UpgradesFromListRef { get; set; }

		public Maps(BinaryReader inStream)
		{
			ItemKey = inStream.ReadInt64();
			AreaNormalKey = inStream.ReadInt64();
			AreaUniqueKey = inStream.ReadInt64();
			UpgradesToKey = inStream.ReadInt64();
			ExtraPacksListLength = inStream.ReadInt32();
			ExtraPacksListOffset = inStream.ReadInt32();
			AchievementKey = inStream.ReadInt64();
            GuildLetterNormalStringOffset = inStream.ReadInt32();
            GuildLetterUniqueStringOffset = inStream.ReadInt32();
            UpgradesFromListLength = inStream.ReadInt32();
            UpgradesFromListOffset = inStream.ReadInt32();
        }

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(ItemKey);
			outStream.Write(AreaNormalKey);
			outStream.Write(AreaUniqueKey);
			outStream.Write(UpgradesToKey);
			outStream.Write(ExtraPacksListLength);
			outStream.Write(ExtraPacksListOffset);
			outStream.Write(AchievementKey);
            outStream.Write(GuildLetterNormalStringOffset);
            outStream.Write(GuildLetterUniqueStringOffset );
            outStream.Write(UpgradesFromListLength);
            outStream.Write(UpgradesFromListOffset);
        }

		public override int GetSize()
		{
			return 0x40;
		}

        public override string ToString()
        {
            return "".ToString() + " [" + Key.ToString() + "]";
        }
    }
}