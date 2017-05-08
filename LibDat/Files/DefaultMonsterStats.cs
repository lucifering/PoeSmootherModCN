using System.IO;

namespace LibDat.Files
{
	public class DefaultMonsterStats : BaseDat
	{
		[Hidden]
		public int LevelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LevelStringData { get; set; }
        public int U00 { get; set; }
		public int Evasion { get; set; }
		public int Accuracy { get; set; }
		public int Life { get; set; }
		public int Xp { get; set; }
		public int BaseResist { get; set; }
		public int U06 { get; set; }

		public DefaultMonsterStats(BinaryReader inStream)
		{
			LevelStringOffset = inStream.ReadInt32();
			U00 = inStream.ReadInt32();
			Evasion = inStream.ReadInt32();
			Accuracy = inStream.ReadInt32();
			Life = inStream.ReadInt32();
			Xp = inStream.ReadInt32();
			BaseResist = inStream.ReadInt32();
			U06 = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(LevelStringOffset);
			outStream.Write(U00);
			outStream.Write(Evasion);
			outStream.Write(Accuracy);
			outStream.Write(Life);
			outStream.Write(Xp);
			outStream.Write(BaseResist);
			outStream.Write(U06);
		}

		public override int GetSize()
		{
			return 0x20;
		}
	}
}