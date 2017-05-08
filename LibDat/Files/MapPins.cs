using System;
using System.IO;

namespace LibDat.Files
{
    public class MapPins : BaseDat
    {
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int U00 { get; set; }
        public int U01 { get; set; }
        [Hidden]
        public Int64 NormalAreaKey { get; set; }
        [ExternalReference]
        public WorldAreas NormalAreaRef { get; set; }
        [Hidden]
        public int NormalAreasListLength { get; set; }
        [Hidden]
        public int NormalAreasListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List NormalAreasListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<WorldAreas> NormalAreasListRef { get; set; }
        [Hidden]
        public int NameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NameStringData { get; set; }
        [Hidden]
        public int NotesStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NotesStringData { get; set; }
        [Hidden]
        public int U05ListLength { get; set; }
        [Hidden]
        public int U05ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U05ListData { get; set; }
        public int U07 { get; set; }
        public int Act { get; set; }
        [Hidden]
        public int MercilessAreasListLength { get; set; }
        [Hidden]
        public int MercilessAreasListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List MercilessAreasListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<WorldAreas> MercilessAreasListRef { get; set; }
        [Hidden]
        public int CruelAreasListLength { get; set; }
        [Hidden]
        public int CruelAreasListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List CruelAreasListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<WorldAreas> CruelAreasListRef { get; set; }
        [Hidden]
        public Int64 CruelAreaKey { get; set; }
        [ExternalReference]
        public WorldAreas CruelAreaRef { get; set; }
        [Hidden]
        public Int64 MercilessAreaKey { get; set; }
        [ExternalReference]
        public WorldAreas MercilessAreaRef { get; set; }
        [Hidden]
        public int Code2StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString Code2StringData { get; set; }

        public MapPins(BinaryReader inStream)
        {
            CodeStringOffset = inStream.ReadInt32();
            U00 = inStream.ReadInt32();
            U01 = inStream.ReadInt32();
            NormalAreaKey = inStream.ReadInt64();
            NormalAreasListLength = inStream.ReadInt32();
            NormalAreasListOffset = inStream.ReadInt32();
            NameStringOffset = inStream.ReadInt32();
            NotesStringOffset = inStream.ReadInt32();
            U05ListLength = inStream.ReadInt32();
            U05ListOffset = inStream.ReadInt32();
            U07 = inStream.ReadInt32();
            Act = inStream.ReadInt32();
            MercilessAreasListLength = inStream.ReadInt32();
            MercilessAreasListOffset = inStream.ReadInt32();
            CruelAreasListLength = inStream.ReadInt32();
            CruelAreasListOffset = inStream.ReadInt32();
            CruelAreaKey = inStream.ReadInt64();
            MercilessAreaKey = inStream.ReadInt64();
            Code2StringOffset = inStream.ReadInt32();
        }

        public override void Save(BinaryWriter outStream)
        {
            outStream.Write(CodeStringOffset);
            outStream.Write(U00);
            outStream.Write(U01);
            outStream.Write(NormalAreaKey);
            outStream.Write(NormalAreasListLength);
            outStream.Write(NormalAreasListOffset);
            outStream.Write(NameStringOffset);
            outStream.Write(NotesStringOffset);
            outStream.Write(U05ListLength);
            outStream.Write(U05ListOffset);
            outStream.Write(U07);
            outStream.Write(Act);
            outStream.Write(MercilessAreasListLength);
            outStream.Write(MercilessAreasListOffset);
            outStream.Write(CruelAreasListLength);
            outStream.Write(CruelAreasListOffset);
            outStream.Write(CruelAreaKey);
            outStream.Write(MercilessAreaKey);
            outStream.Write(Code2StringOffset);
        }

        public override int GetSize()
        {
            return 0x58;
        }

        public override string ToString()
        {
            return NameStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}