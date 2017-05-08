using System;
using System.IO;

namespace LibDat.Files
{
	public class WorldAreas : BaseDat, IComparable
	{
        [Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
		public int LabelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LabelStringData { get; set; }
        public int Act { get; set; }
		public bool IsTown { get; set; }
		public bool HasWaypoint { get; set; }
        [Hidden]
        public int ConnectionsListLength { get; set; } 
        [Hidden]
        public int ConnectionsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt32List ConnectionsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<WorldAreas> ConnectionsListRef { get; set; }
        public Int64 MonsterLevel { get; set; }
		public bool Flag2 { get; set; }
		public int WorldAreaId { get; set; } // Used for waypoints and other UI-related things.
		public int U07 { get; set; }
        public int U08 { get; set; }
        [Hidden]
        public int LoadingScreenStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LoadingScreenStringData { get; set; }
        public int QuestFlag1 { get; set; }
        [Hidden]
        public int U11ListLength { get; set; }
        [Hidden]
        public int U11ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U11ListData { get; set; }
        public int U13 { get; set; }
        [Hidden]
        public int TopologiesListLength { get; set; }
        [Hidden]
        public int TopologiesListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List TopologiesListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Topologies> TopologiesListRef { get; set; }
        [Hidden]
        public int RespawnPointKey { get; set; }
        [ExternalReference]
        public WorldAreas RespawnPointRef { get; set; }
        [Hidden]
        public int DifficultyKey { get; set; }
        [ExternalReference]
        public Difficulties DifficultyRef { get; set; }
        public int U18 { get; set; }
		public int U19 { get; set; }
		public int U20 { get; set; }
		public int QuestFlag2 { get; set; }
		public int QuestFlag3 { get; set; }
        [Hidden]
        public int MapbossListLength { get; set; }
        [Hidden]
        public int MapbossListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List MapbossListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<MonsterVarieties> MapbossListRef { get; set; }
        [Hidden]
        public int PreloadedVarietiesListLength { get; set; }
        [Hidden]
        public int PreloadedVarietiesListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List PreloadedVarietiesListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<MonsterVarieties> PreloadedVarietiesListRef { get; set; }
        public int U27 { get; set; }
        [Hidden]
        public int DropTagListLength { get; set; }
        [Hidden]
        public int DropTagListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List DropTagListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Tags> DropTagListRef { get; set; }
        [Hidden]
        public int DropTagWeightListLength { get; set; }
        [Hidden]
        public int DropTagWeightListOffset { get; set; }
        [ResourceOnly]
        public UInt32List DropTagWeightListData { get; set; }
        public bool IsMap { get; set; }
        [Hidden]
        public Int64 AchievementKey { get; set; }
        [ExternalReference]
        public AchievementItems AchievementRef { get; set; }
        public int PVPType { get; set; }
        public int U34 { get; set; }
        [Hidden]
        public Int64 MapAchievementKey { get; set; }
        [ExternalReference]
        public AchievementItems MapAchievementRef { get; set; }
        [Hidden]
        public int ModsListLength { get; set; }
        [Hidden]
        public int ModsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List ModsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Mods> ModsListRef { get; set; }
        [Hidden]
        public int SoundEffectStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString SoundEffectStringData { get; set; }
        public int U38 { get; set; }

		public WorldAreas(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			LabelStringOffset = inStream.ReadInt32();
			Act = inStream.ReadInt32();
			IsTown = inStream.ReadBoolean();
			HasWaypoint = inStream.ReadBoolean();
			ConnectionsListLength = inStream.ReadInt32();
			ConnectionsListOffset = inStream.ReadInt32();
			MonsterLevel = inStream.ReadInt64();
			Flag2 = inStream.ReadBoolean();
			WorldAreaId = inStream.ReadInt32();
			U07 = inStream.ReadInt32();
            U08 = inStream.ReadInt32();
            LoadingScreenStringOffset = inStream.ReadInt32();
			QuestFlag1 = inStream.ReadInt32();
            U11ListLength = inStream.ReadInt32();
			U11ListOffset = inStream.ReadInt32();
			U13 = inStream.ReadInt32();
			TopologiesListLength = inStream.ReadInt32();
			TopologiesListOffset = inStream.ReadInt32();
			RespawnPointKey = inStream.ReadInt32();
			DifficultyKey = inStream.ReadInt32();
			U18 = inStream.ReadInt32();
			U19 = inStream.ReadInt32();
			U20 = inStream.ReadInt32();
			QuestFlag2 = inStream.ReadInt32();
			QuestFlag3 = inStream.ReadInt32();
			MapbossListLength = inStream.ReadInt32();
			MapbossListOffset = inStream.ReadInt32();
			PreloadedVarietiesListLength = inStream.ReadInt32();
			PreloadedVarietiesListOffset = inStream.ReadInt32();
			U27 = inStream.ReadInt32();
			DropTagListLength = inStream.ReadInt32();
			DropTagListOffset = inStream.ReadInt32();
			DropTagWeightListLength = inStream.ReadInt32();
			DropTagWeightListOffset = inStream.ReadInt32();
            IsMap = inStream.ReadBoolean();
			AchievementKey = inStream.ReadInt64();
			PVPType = inStream.ReadInt32();
            U34 = inStream.ReadInt32();
            MapAchievementKey = inStream.ReadInt64();
			ModsListLength = inStream.ReadInt32();
			ModsListOffset = inStream.ReadInt32();
            SoundEffectStringOffset = inStream.ReadInt32();
            U38 = inStream.ReadInt32();
        }

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(LabelStringOffset);
			outStream.Write(Act);
			outStream.Write(IsTown);
			outStream.Write(HasWaypoint);
			outStream.Write(ConnectionsListLength);
			outStream.Write(ConnectionsListOffset);
			outStream.Write(MonsterLevel);
			outStream.Write(Flag2);
			outStream.Write(WorldAreaId);
			outStream.Write(U07);
            outStream.Write(U08);
            outStream.Write(LoadingScreenStringOffset);
			outStream.Write(QuestFlag1);
            outStream.Write(U11ListLength);
			outStream.Write(U11ListOffset);
			outStream.Write(U13);
			outStream.Write(TopologiesListLength);
			outStream.Write(TopologiesListOffset);
			outStream.Write(RespawnPointKey);
			outStream.Write(DifficultyKey);
			outStream.Write(U18);
			outStream.Write(U19);
			outStream.Write(U20);
			outStream.Write(QuestFlag2);
			outStream.Write(QuestFlag3);
			outStream.Write(MapbossListLength);
			outStream.Write(MapbossListOffset);
			outStream.Write(PreloadedVarietiesListLength);
			outStream.Write(PreloadedVarietiesListOffset);
			outStream.Write(U27);
			outStream.Write(DropTagListLength);
			outStream.Write(DropTagListOffset);
			outStream.Write(DropTagWeightListLength);
			outStream.Write(DropTagWeightListOffset);
            outStream.Write(IsMap);
			outStream.Write(AchievementKey);
			outStream.Write(PVPType);
			outStream.Write(U34);
			outStream.Write(MapAchievementKey);
			outStream.Write(ModsListLength);
			outStream.Write(ModsListOffset);
            outStream.Write(SoundEffectStringOffset);
            outStream.Write(U38);
        }

		public override int GetSize()
		{
			return 0xB0;
		}

        public override string ToString()
        {
            return LabelStringData.ToString() + " [" + Key.ToString() + "]";
        }

        public override string ToStringWiki()
        {
            return "[[" + LabelStringData.ToString() + "]]";
        }

        public override int CompareTo(object obj)
        {
            if (!(obj is WorldAreas))
                throw new NotImplementedException("Can only compare WorldAreas");

            WorldAreas other = obj as WorldAreas;
            string thisCode = this.CodeStringData.ToString();
            string otherCode = other.CodeStringData.ToString();
            if (thisCode.Contains("Descent2_"))
            {
                thisCode = thisCode.Replace("Descent2_", "7_");
            }
            if (otherCode.Contains("Descent2_"))
            {
                otherCode = otherCode.Replace("Descent2_", "7_");
            }
            if (thisCode.Contains("Descent"))
            {
                thisCode = thisCode.Replace("Descent", "6_");
            }
            if (otherCode.Contains("Descent"))
            {
                otherCode = otherCode.Replace("Descent", "6_");
            }
            if (thisCode.Contains("MapTier"))
            {
                thisCode = thisCode.Replace("MapTier", "9_");
            }
            if (otherCode.Contains("MapTier"))
            {
                otherCode = otherCode.Replace("MapTier", "9_");
            }
            if (thisCode.Contains("EndlessLedge"))
            {
                thisCode = thisCode.Replace("EndlessLedge", "8_").Replace("Map", "");
            }
            if (otherCode.Contains("EndlessLedge"))
            {
                otherCode = otherCode.Replace("EndlessLedge", "8_").Replace("Map", "");
            }
            if (thisCode.Split('_').Length >= 2)
            {
                string[] splitedStr = thisCode.Split('_');
                if (splitedStr[1].Length == 1) splitedStr[1] = "0" + splitedStr[1];
                if (splitedStr[1].Length == 2) splitedStr[1] = "0" + splitedStr[1];
                if (splitedStr.Length >= 3)
                {
                    string oldSplitered = splitedStr[2];
                    splitedStr[2] = splitedStr[2].Replace("a", "").Replace("b", "").Replace("c", "").Replace("d", "");
                    if (splitedStr[2].Length == 1) splitedStr[2] = "0" + splitedStr[2] + oldSplitered;
                }
                thisCode = string.Join("_", splitedStr);
            }
            if (otherCode.Split('_').Length >= 2)
            {
                string[] splitedStr = otherCode.Split('_');
                if (splitedStr[1].Length == 1) splitedStr[1] = "0" + splitedStr[1];
                if (splitedStr[1].Length == 2) splitedStr[1] = "0" + splitedStr[1];
                if (splitedStr.Length >= 3)
                {
                    string oldSplitered = splitedStr[2];
                    splitedStr[2] = splitedStr[2].Replace("a", "").Replace("b", "").Replace("c", "").Replace("d", "");
                    if (splitedStr[2].Length == 1) splitedStr[2] = "0" + splitedStr[2] + oldSplitered;
                }
                otherCode = string.Join("_", splitedStr);
            }
            return thisCode.CompareTo(otherCode);
        }
    }
}