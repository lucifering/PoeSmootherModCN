using System;
using System.IO;

namespace LibDat.Files
{
	public class ShopItem : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int NameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NameStringData { get; set; }
        [Hidden]
        public int DescriptionStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString DescriptionStringData { get; set; }
        [Hidden]
        public int BundledItemListLength { get; set; }
        [Hidden]
        public int BundledItemListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt32List BundledItemListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<ShopItem> BundledItemListRef { get; set; }
        public bool Flag0 { get; set; }
        [Hidden]
        public int BundledItemAmountListLength { get; set; }
        [Hidden]
        public int BundledItemAmountListOffset { get; set; }
        [ResourceOnly]
        public UInt32List BundledItemAmountListData { get; set; }
        public int Price { get; set; }
        [Hidden]
        public int U08ListLength { get; set; }
        [Hidden]
        public int U08ListOffset { get; set; }
        [ResourceOnly]
        public UInt64List U08ListData { get; set; }
        [Hidden]
        public int IconStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString IconStringData { get; set; }
        public Int64 U11 { get; set; }
        [Hidden]
        public int YoutubeVideoStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString YoutubeVideoStringData { get; set; }
        [Hidden]
        public int LargeIconStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LargeIconStringData { get; set; }
        public int U15 { get; set; }
        [Hidden]
        public int DailyDealStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString DailyDealStringData { get; set; }

		public ShopItem(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            NameStringOffset = inStream.ReadInt32();
            DescriptionStringOffset = inStream.ReadInt32();
			BundledItemListLength = inStream.ReadInt32();
			BundledItemListOffset = inStream.ReadInt32();
			Flag0 = inStream.ReadBoolean();
			BundledItemAmountListLength = inStream.ReadInt32();
			BundledItemAmountListOffset = inStream.ReadInt32();
			Price = inStream.ReadInt32();
			U08ListLength = inStream.ReadInt32();
			U08ListOffset = inStream.ReadInt32();
			IconStringOffset = inStream.ReadInt32();
			U11 = inStream.ReadInt64();
			YoutubeVideoStringOffset = inStream.ReadInt32();
			LargeIconStringOffset = inStream.ReadInt32();
            U15 = inStream.ReadInt32();
            DailyDealStringOffset = inStream.ReadInt32();
        }

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(NameStringOffset);
            outStream.Write(DescriptionStringOffset);
			outStream.Write(BundledItemListLength);
			outStream.Write(BundledItemListOffset);
			outStream.Write(Flag0);
			outStream.Write(BundledItemAmountListLength);
			outStream.Write(BundledItemAmountListOffset);
			outStream.Write(Price);
			outStream.Write(U08ListLength);
			outStream.Write(U08ListOffset);
			outStream.Write(IconStringOffset);
			outStream.Write(U11);
			outStream.Write(YoutubeVideoStringOffset);
			outStream.Write(LargeIconStringOffset);
            outStream.Write(U15);
            outStream.Write(DailyDealStringOffset);
        }

		public override int GetSize()
		{
			return 0x45;
		}

        public override string ToString()
        {
            return NameStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}