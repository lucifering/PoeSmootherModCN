using System.IO;

namespace LibDat.Files
{
	public class Environments : BaseDat
	{
		[Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
		public int AudioStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AudioStringData { get; set; }
        [Hidden]
        public int MusicRegularListLength { get; set; }
        [Hidden]
        public int MusicRegularListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List MusicRegularListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Music> MusicRegularListRef { get; set; }
        [Hidden]
		public int EnvRegularStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString EnvRegularStringData { get; set; }
        [Hidden]
		public int EnvCorruptedStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString EnvCorruptedStringData { get; set; }
        [Hidden]
        public int MusicCorruptedListLength { get; set; }
        [Hidden]
        public int MusicCorruptedListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List MusicCorruptedListData { get; set; } 
        [ExternalReferenceList]
        public ExternalDatList<Music> MusicCorruptedListRef { get; set; }
        [Hidden]
        public int AudioCorruptedStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AudioCorruptedStringData { get; set; }

		public Environments(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			AudioStringOffset = inStream.ReadInt32();
			MusicRegularListLength = inStream.ReadInt32();
			MusicRegularListOffset = inStream.ReadInt32();
			EnvRegularStringOffset = inStream.ReadInt32();
			EnvCorruptedStringOffset = inStream.ReadInt32();
			MusicCorruptedListLength = inStream.ReadInt32();
			MusicCorruptedListOffset = inStream.ReadInt32();
			AudioCorruptedStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(AudioStringOffset);
			outStream.Write(MusicRegularListLength);
			outStream.Write(MusicRegularListOffset);
			outStream.Write(EnvRegularStringOffset);
			outStream.Write(EnvCorruptedStringOffset);
			outStream.Write(MusicCorruptedListLength);
			outStream.Write(MusicCorruptedListOffset);
			outStream.Write(AudioCorruptedStringOffset);
		}

		public override int GetSize()
		{
			return 0x24;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}