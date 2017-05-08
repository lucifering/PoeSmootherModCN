using System.IO;

namespace LibDat.Files
{
	public class ExperienceLevels : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int Level { get; set; }
		public uint Experience { get; set; }

		public ExperienceLevels(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			Level = inStream.ReadInt32();
			Experience = inStream.ReadUInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(Level);
			outStream.Write(Experience);
		}

		public override int GetSize()
		{
			return 0xC;
		}
	}
}