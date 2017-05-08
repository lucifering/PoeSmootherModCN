using System.IO;

namespace LibDat.Files
{
    public class Music : BaseDat
    {
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int MusicFileStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MusicFileStringData { get; set; }

        public Music(BinaryReader inStream)
        {
            CodeStringOffset = inStream.ReadInt32();
            MusicFileStringOffset = inStream.ReadInt32();
        }

        public override void Save(BinaryWriter outStream)
        {
            outStream.Write(CodeStringOffset);
            outStream.Write(MusicFileStringOffset);
        }

        public override int GetSize()
        {
            return 0x8;
        }

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}