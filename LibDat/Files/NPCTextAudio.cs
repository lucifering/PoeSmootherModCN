using System;
using System.IO;

namespace LibDat.Files
{
	public class NPCTextAudio : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public Int64 CharacterKey { get; set; }
        [ExternalReference]
        public Characters CharacterRef { get; set; }
        [Hidden]
        public int TextStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString TextStringData { get; set; }
        [Hidden]
        public int AudioStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AudioStringData { get; set; }

		public NPCTextAudio(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			CharacterKey = inStream.ReadInt64();
            TextStringOffset = inStream.ReadInt32();
            AudioStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(CharacterKey);
            outStream.Write(TextStringOffset);
            outStream.Write(AudioStringOffset);
		}

		public override int GetSize()
		{
			return 0x14;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}
