using System.IO;

namespace LibDat.Files
{
	public class LeagueFlag : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int ImageStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ImageStringData { get; set; }

		public LeagueFlag()
		{
			
		}
		public LeagueFlag(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			ImageStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(ImageStringOffset);
		}

		public override int GetSize()
		{
			return 0x08;
		}
	}
}