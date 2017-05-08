using System.IO;

namespace LibDat.Files
{
	public class MonsterTypes : BaseDat
	{
		[Hidden]
		public int LabelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LabelStringData { get; set; }
        public int ResistFire { get; set; }
		public int ResistCold { get; set; }
		public int ResistLightning { get; set; }
		public int ResistChaos { get; set; }
		public int U04 { get; set; }
		public bool Flag0 { get; set; }
		public int BonusStr { get; set; }
		public int BonusDex { get; set; }
		public int BonusInt { get; set; }
		public int U08 { get; set; }
        [Hidden]
        public int TagsListLength { get; set; }
        [Hidden]
        public int TagsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List TagsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Tags> TagsListRef { get; set; }

		public MonsterTypes(BinaryReader inStream)
		{
			LabelStringOffset = inStream.ReadInt32();
			ResistFire = inStream.ReadInt32();
			ResistCold = inStream.ReadInt32();
			ResistLightning = inStream.ReadInt32();
			ResistChaos = inStream.ReadInt32();
			U04 = inStream.ReadInt32();
			Flag0 = inStream.ReadBoolean();
			BonusStr = inStream.ReadInt32();
			BonusDex = inStream.ReadInt32();
			BonusInt = inStream.ReadInt32();
			U08 = inStream.ReadInt32();
			TagsListLength = inStream.ReadInt32();
			TagsListOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(LabelStringOffset);
			outStream.Write(ResistFire);
			outStream.Write(ResistCold);
			outStream.Write(ResistLightning);
			outStream.Write(ResistChaos);
			outStream.Write(U04);
			outStream.Write(Flag0);
			outStream.Write(BonusStr);
			outStream.Write(BonusDex);
			outStream.Write(BonusInt);
			outStream.Write(U08);
			outStream.Write(TagsListLength);
			outStream.Write(TagsListOffset);
		}

		public override int GetSize()
		{
			return 0x31;
		}

        public override string ToString()
        {
            return LabelStringData.ToString() + " [" + Key.ToString() + "]";
        }

        public override string ToStringWiki()
        {
            return "[[" + LabelStringData.ToString() + "]]";
        }
    }
}