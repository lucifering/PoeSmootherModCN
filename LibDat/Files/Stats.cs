using System;
using System.IO;

namespace LibDat.Files
{
    public class Stats : BaseDat
    {
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public bool Flag0 { get; set; }
        public bool Flag1 { get; set; }
        public bool Flag2 { get; set; }
        public int U02 { get; set; }
        public bool Flag3 { get; set; }
        [Hidden]
        public int LabelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LabelStringData { get; set; }
        public bool Flag4 { get; set; }
        public bool Flag5 { get; set; }
        public bool Flag6 { get; set; }
        [Hidden]
        public int MainHandStatKey { get; set; }
        [ExternalReference]
        public Stats MainHandStatRef { get; set; }
        [Hidden]
        public int OffHandStatKey { get; set; }
        [ExternalReference]
        public Stats OffHandStatRef { get; set; }
        public bool Flag7 { get; set; }

        public Stats(BinaryReader inStream)
        {
            CodeStringOffset = inStream.ReadInt32();
            Flag0 = inStream.ReadBoolean();
            Flag1 = inStream.ReadBoolean();
            Flag2 = inStream.ReadBoolean();
            U02 = inStream.ReadInt32();
            Flag3 = inStream.ReadBoolean();
            LabelStringOffset = inStream.ReadInt32();
            Flag4 = inStream.ReadBoolean();
            Flag5 = inStream.ReadBoolean();
            Flag6 = inStream.ReadBoolean();
            MainHandStatKey = inStream.ReadInt32();
            OffHandStatKey = inStream.ReadInt32();
            Flag7 = inStream.ReadBoolean();
        }

        public override void Save(BinaryWriter outStream)
        {
            outStream.Write(CodeStringOffset);
            outStream.Write(Flag0);
            outStream.Write(Flag1);
            outStream.Write(Flag2);
            outStream.Write(U02);
            outStream.Write(Flag3);
            outStream.Write(LabelStringOffset);
            outStream.Write(Flag4);
            outStream.Write(Flag5);
            outStream.Write(Flag6);
            outStream.Write(MainHandStatKey);
            outStream.Write(OffHandStatKey);
            outStream.Write(Flag7);
        }

        public override int GetSize()
        {
            return 0x1C;
        }

        public override string ToString()
        {
            //if (LabelStringData.ToString() != String.Empty)
            //    return LabelStringData.ToString() + " [" + Key.ToString() + "]";
            //else
                return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}