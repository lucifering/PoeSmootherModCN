using System.IO;

namespace LibDat.Files
{
    public class Quest : BaseDat
    {
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int Act { get; set; }
        [Hidden]
        public int TitleStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString TitleStringData { get; set; }
        public int U03 { get; set; }
        [Hidden]
        public int QuestIconStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString QuestIconStringData { get; set; }

        public Quest(BinaryReader inStream)
        {
            CodeStringOffset = inStream.ReadInt32();
            Act = inStream.ReadInt32();
            TitleStringOffset = inStream.ReadInt32();
            U03 = inStream.ReadInt32();
            QuestIconStringOffset = inStream.ReadInt32();
        }

        public override void Save(BinaryWriter outStream)
        {
            outStream.Write(CodeStringOffset);
            outStream.Write(Act);
            outStream.Write(TitleStringOffset);
            outStream.Write(U03);
            outStream.Write(QuestIconStringOffset);
        }

        public override int GetSize()
        {
            return 0x14;
        }

        public override string ToString()
        {
            return TitleStringData.ToString() + " [" + Key.ToString() + "]";
        }

        public override string ToStringWiki()
        {
            return "[[" + TitleStringData.ToString() + "]]";
        }
    }
}