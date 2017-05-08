using System;
using System.IO;

namespace LibDat.Files
{
	public class BuffDefinitions : BaseDat
	{
        [Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
		public int DescriptionStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString DescriptionStringData { get; set; }
        public bool Invisible { get; set; }
		public bool Removable { get; set; }
		[Hidden]
		public int NameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NameStringData { get; set; }
        [Hidden]
        public int ImpactedStatsListLength { get; set; }
        [Hidden]
        public int ImpactedStatsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List ImpactedStatsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Stats> ImpactedStatsListRef { get; set; }
        public bool Flag0 { get; set; }
		public int U02 { get; set; }
		public bool IsCharged { get; set; }
        [Hidden]
        public Int64 Stat1Key { get; set; }
        [ExternalReference]
        public Stats Stat1Ref { get; set; }
        [Hidden]
        public Int64 Stat2Key { get; set; }
        [ExternalReference]
        public Stats Stat2Ref { get; set; }
        public bool Flag2 { get; set; }
		public int U05 { get; set; }
        [Hidden]
        public Int64 VisualKey { get; set; }
        [ExternalReference]
        public BuffVisuals VisualRef { get; set; }
        public bool Flag3 { get; set; }
		public bool Flag4 { get; set; }
		public int U07 { get; set; }

		public BuffDefinitions(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			DescriptionStringOffset = inStream.ReadInt32();
			Invisible = inStream.ReadBoolean();
			Removable = inStream.ReadBoolean();
			NameStringOffset = inStream.ReadInt32();
			ImpactedStatsListLength = inStream.ReadInt32();
			ImpactedStatsListOffset = inStream.ReadInt32();
			Flag0 = inStream.ReadBoolean();
			U02 = inStream.ReadInt32();
			IsCharged = inStream.ReadBoolean();
            Stat1Key = inStream.ReadInt64();
            Stat2Key = inStream.ReadInt64();
			Flag2 = inStream.ReadBoolean();
			U05 = inStream.ReadInt32();
			VisualKey = inStream.ReadInt64();
			Flag3 = inStream.ReadBoolean();
			Flag4 = inStream.ReadBoolean();
			U07 = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(DescriptionStringOffset);
			outStream.Write(Invisible);
			outStream.Write(Removable);
			outStream.Write(NameStringOffset);
			outStream.Write(ImpactedStatsListLength);
			outStream.Write(ImpactedStatsListOffset);
			outStream.Write(Flag0);
			outStream.Write(U02);
			outStream.Write(IsCharged);
            outStream.Write(Stat1Key);
            outStream.Write(Stat2Key);
			outStream.Write(Flag2);
			outStream.Write(U05);
			outStream.Write(VisualKey);
			outStream.Write(Flag3);
			outStream.Write(Flag4);
			outStream.Write(U07);
		}

		public override int GetSize()
		{
			return 0x3F;
		}

        public override string ToString()
        {
            return NameStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}
