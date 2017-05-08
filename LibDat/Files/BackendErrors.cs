using System.IO;

namespace LibDat.Files
{
	public class BackendErrors : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int TextStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString TextStringData { get; set; }

		public BackendErrors()
		{
			
		}
		public BackendErrors(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			TextStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(TextStringOffset);
		}

		public override int GetSize()
		{
			return 0x08;
		}
	}
}
