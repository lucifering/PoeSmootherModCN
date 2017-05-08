using System.IO;

namespace LibDat.Files
{
    public class NPCs : BaseDat
    {
        [Hidden]
        public int MetadataStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MetadataStringData { get; set; }
        [Hidden]
        public int NameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NameStringData { get; set; }
        public int SoldItemCount { get; set; }
        [Hidden]
        public int SoldTagsAmountsListLength { get; set; }
        [Hidden]
        public int SoldTagsAmountsListOffset { get; set; }
        [ResourceOnly]
        public UInt32List SoldTagsAmountsListData { get; set; }
        [Hidden]
        public int SoldTagsListLength { get; set; }
        [Hidden]
        public int SoldTagsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List SoldTagsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Tags> SoldTagsListRef { get; set; }
        public int U05Unknown { get; set; }
        [Hidden]
        public int SoldItemTypesListLength { get; set; }
        [Hidden]
        public int SoldItemTypesListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List SoldItemTypesListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<DropPool> SoldItemTypesListRef { get; set; }
        [Hidden]
        public int U07ListLength { get; set; }
        [Hidden]
        public int U07ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U07ListData { get; set; }
        [Hidden]
        public int AdditionalSoldItemListLength { get; set; }
        [Hidden]
        public int AdditionalSoldItemListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List AdditionalSoldItemListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> AdditionalSoldItemListRef { get; set; }
        [Hidden]
        public int GreetingListLength { get; set; }
        [Hidden]
        public int GreetingListOffset { get; set; }
        [ResourceOnly]
        public IndirectStringList GreetingListData { get; set; }
        [Hidden]
        public int ParentStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ParentStringData { get; set; }
        [Hidden]
        public int GoodbyeListLength { get; set; }
        [Hidden]
        public int GoodbyeListOffset { get; set; }
        [ResourceOnly]
        public IndirectStringList GoodbyeListData { get; set; }
        public int U13Unknown { get; set; }

        public NPCs(BinaryReader inStream)
        {
            MetadataStringOffset = inStream.ReadInt32();
            NameStringOffset = inStream.ReadInt32();
            SoldItemCount = inStream.ReadInt32();
            SoldTagsAmountsListLength = inStream.ReadInt32();
            SoldTagsAmountsListOffset = inStream.ReadInt32();
            SoldTagsListLength = inStream.ReadInt32();
            SoldTagsListOffset = inStream.ReadInt32();
            U05Unknown = inStream.ReadInt32();
            SoldItemTypesListLength = inStream.ReadInt32();
            SoldItemTypesListOffset = inStream.ReadInt32();
            U07ListLength = inStream.ReadInt32();
            U07ListOffset = inStream.ReadInt32();
            AdditionalSoldItemListLength = inStream.ReadInt32();
            AdditionalSoldItemListOffset = inStream.ReadInt32();
            GreetingListLength = inStream.ReadInt32();
            GreetingListOffset = inStream.ReadInt32();
            ParentStringOffset = inStream.ReadInt32();
            GoodbyeListLength = inStream.ReadInt32();
            GoodbyeListOffset = inStream.ReadInt32();
            U13Unknown = inStream.ReadInt32();
        }

        public override void Save(BinaryWriter outStream)
        {
            outStream.Write(MetadataStringOffset);
            outStream.Write(NameStringOffset);
            outStream.Write(SoldItemCount);
            outStream.Write(SoldTagsAmountsListLength);
            outStream.Write(SoldTagsAmountsListOffset);
            outStream.Write(SoldTagsListLength);
            outStream.Write(SoldTagsListOffset);
            outStream.Write(U05Unknown);
            outStream.Write(SoldItemTypesListLength);
            outStream.Write(SoldItemTypesListOffset);
            outStream.Write(U07ListLength);
            outStream.Write(U07ListOffset);
            outStream.Write(AdditionalSoldItemListLength);
            outStream.Write(AdditionalSoldItemListOffset);
            outStream.Write(GreetingListLength);
            outStream.Write(GreetingListOffset);
            outStream.Write(ParentStringOffset);
            outStream.Write(GoodbyeListLength);
            outStream.Write(GoodbyeListOffset);
            outStream.Write(U13Unknown);
        }

        public override int GetSize()
        {
            return 0x50;
        }

        public override string ToString()
        {
            return NameStringData.ToString() + " [" + Key.ToString() + "]";
        }

    }
}