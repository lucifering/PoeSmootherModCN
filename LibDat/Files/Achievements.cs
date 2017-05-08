using System.IO;

namespace LibDat.Files
{
	public class Achievements : BaseDat
	{
		[Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int DescriptionStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString DescriptionStringData { get; set; }
        public int AchievementSet { get; set; }
        [Hidden]
        public int ObjectiveStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ObjectiveStringData { get; set; }
        public int U04 { get; set; }
		public bool Flag0 { get; set; }
		public bool Flag1 { get; set; }

		public Achievements(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			DescriptionStringOffset = inStream.ReadInt32();
			AchievementSet = inStream.ReadInt32();
			ObjectiveStringOffset = inStream.ReadInt32();
			U04 = inStream.ReadInt32();
			Flag0 = inStream.ReadBoolean();
			Flag1 = inStream.ReadBoolean();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(DescriptionStringOffset);
			outStream.Write(AchievementSet);
			outStream.Write(ObjectiveStringOffset);
			outStream.Write(U04);
			outStream.Write(Flag0);
			outStream.Write(Flag1);
		}

		public override int GetSize()
		{
			return 0x16;
		}

        public override string ToString()
        {
            return DescriptionStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}
