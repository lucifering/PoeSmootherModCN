using System;
using System.IO;

namespace LibDat.Files
{
	public class MonsterPacks : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public Int64 AreaKey { get; set; }
        [ExternalReference]
        public WorldAreas AreaRef { get; set; }
        public int U03 { get; set; }
		public int U04 { get; set; }
		public int U05 { get; set; }
		public int U06 { get; set; }
		public int U07 { get; set; }
        [Hidden]
        public int VarietiesListLength { get; set; }
        [Hidden]
        public int VarietiesListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List VarietiesListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<MonsterVarieties> VarietiesListRef { get; set; }
        public bool Flag0 { get; set; }
		public int U11 { get; set; }
        [Hidden]
        public int U12ListLength { get; set; }
        [Hidden]
        public int U12ListOffset { get; set; }
        [ResourceOnly]
        public IndirectStringList U12ListData { get; set; }
        [Hidden]
        public int MapTagsListLength { get; set; }
        [Hidden]
        public int MapTagsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List MapTagsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Tags> MapTagsListRef { get; set; }

		public MonsterPacks(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			AreaKey = inStream.ReadInt64();
			U03 = inStream.ReadInt32();
			U04 = inStream.ReadInt32();
			U05 = inStream.ReadInt32();
			U06 = inStream.ReadInt32();
			U07 = inStream.ReadInt32();
			VarietiesListLength = inStream.ReadInt32();
			VarietiesListOffset = inStream.ReadInt32();
			Flag0 = inStream.ReadBoolean();
			U11 = inStream.ReadInt32();
			U12ListLength = inStream.ReadInt32();
			U12ListOffset = inStream.ReadInt32();
			MapTagsListLength = inStream.ReadInt32();
			MapTagsListOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(AreaKey);
			outStream.Write(U03);
			outStream.Write(U04);
			outStream.Write(U05);
			outStream.Write(U06);
			outStream.Write(U07);
			outStream.Write(VarietiesListLength);
			outStream.Write(VarietiesListOffset);
			outStream.Write(Flag0);
			outStream.Write(U11);
			outStream.Write(U12ListLength);
			outStream.Write(U12ListOffset);
			outStream.Write(MapTagsListLength);
			outStream.Write(MapTagsListOffset);
		}

		public override int GetSize()
		{
			return 0x3D;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}