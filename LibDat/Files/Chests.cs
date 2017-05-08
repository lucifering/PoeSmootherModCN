using System;
using System.IO;

namespace LibDat.Files
{
	public class Chests : BaseDat
	{
        [Hidden]
        public int MetadataStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MetadataStringData { get; set; }
        public bool Flag0 { get; set; }
		public int U00 { get; set; }
        [Hidden]
        public int NameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NameStringData { get; set; }
        [Hidden]
        public int AnimatedObjectStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AnimatedObjectStringData { get; set; }
        public bool Flag1 { get; set; }
		public bool Flag2 { get; set; }
		public int U01 { get; set; }
		public bool Flag3 { get; set; }
		public bool Flag4 { get; set; }
		public int U02 { get; set; }
        [Hidden]
        public int DropsListLength { get; set; }
        [Hidden]
        public int DropsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List DropsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<DropPool> DropsListRef { get; set; }
        [Hidden]
        public int DropRatesListLength { get; set; }
        [Hidden]
        public int DropRatesListOffset { get; set; }
        [ResourceOnly]
        public UInt32List DropRatesListData { get; set; }
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        public bool Flag5 { get; set; }

		public Chests(BinaryReader inStream)
		{
            MetadataStringOffset = inStream.ReadInt32();
			Flag0 = inStream.ReadBoolean();
			U00 = inStream.ReadInt32();
            NameStringOffset = inStream.ReadInt32();
            AnimatedObjectStringOffset = inStream.ReadInt32();
			Flag1 = inStream.ReadBoolean();
			Flag2 = inStream.ReadBoolean();
			U01 = inStream.ReadInt32();
			Flag3 = inStream.ReadBoolean();
			Flag4 = inStream.ReadBoolean();
			U02 = inStream.ReadInt32();
            DropsListLength = inStream.ReadInt32();
            DropsListOffset = inStream.ReadInt32();
            DropRatesListLength = inStream.ReadInt32();
            DropRatesListOffset = inStream.ReadInt32();
			ItemKey = inStream.ReadInt64();
			Flag5 = inStream.ReadBoolean();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(MetadataStringOffset);
			outStream.Write(Flag0);
			outStream.Write(U00);
            outStream.Write(NameStringOffset);
            outStream.Write(AnimatedObjectStringOffset);
			outStream.Write(Flag1);
			outStream.Write(Flag2);
			outStream.Write(U01);
			outStream.Write(Flag3);
			outStream.Write(Flag4);
			outStream.Write(U02);
            outStream.Write(DropsListLength);
            outStream.Write(DropsListOffset);
            outStream.Write(DropRatesListLength);
            outStream.Write(DropRatesListOffset);
			outStream.Write(ItemKey);
			outStream.Write(Flag5);
		}

		public override int GetSize()
		{
			return 0x36;
		}

        public override string ToString()
        {
            return NameStringData.ToString() + " [" + Key.ToString() + "]";
        }
	}
}