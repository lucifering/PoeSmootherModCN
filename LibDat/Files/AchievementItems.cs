using System;
using System.IO;

namespace LibDat.Files
{
	public class AchievementItems : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        public int LengthInSet { get; set; }
		public int StartingPositionInSet { get; set; }
        [Hidden]
        public int NameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NameStringData { get; set; }
        public int NumberOfElements { get; set; }
        [Hidden]
        public Int64 AchievementKey { get; set; }
        [ExternalReference]
        public Achievements AchievementRef { get; set; }

		public AchievementItems(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			LengthInSet = inStream.ReadInt32();
			StartingPositionInSet = inStream.ReadInt32();
			NameStringOffset = inStream.ReadInt32();
			NumberOfElements = inStream.ReadInt32();
			AchievementKey = inStream.ReadInt64();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(LengthInSet);
			outStream.Write(StartingPositionInSet);
			outStream.Write(NameStringOffset);
			outStream.Write(NumberOfElements);
			outStream.Write(AchievementKey);
		}

		public override int GetSize()
		{
			return 0x1c;
		}

        public override string ToString()
        {
            if (NameStringData.ToString() != String.Empty)
                return NameStringData.ToString() + " [" + Key.ToString() + "]";
            else
                return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}
