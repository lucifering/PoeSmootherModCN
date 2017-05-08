using System;
using System.IO;

namespace LibDat.Files
{
	public class QuestStaticRewards : BaseDat
	{
		public int U00 { get; set; }
		public int U01 { get; set; }
        [Hidden]
		public Int64 StatKey { get; set; }
        [ExternalReference]
        public Stats StatRef { get; set; }
        public int StatAmount { get; set; }
        [Hidden]
        public Int64 DifficultyKey { get; set; }
        [ExternalReference]
        public Difficulties DifficultyRef { get; set; }
        [Hidden]
        public Int64 QuestKey { get; set; }
        [ExternalReference]
        public Quest QuestRef { get; set; }

		public QuestStaticRewards(BinaryReader inStream)
		{
			U00 = inStream.ReadInt32();
			U01 = inStream.ReadInt32();
			StatKey = inStream.ReadInt64();
			StatAmount = inStream.ReadInt32();
			DifficultyKey = inStream.ReadInt64();
			QuestKey = inStream.ReadInt64();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(U00);
			outStream.Write(U01);
			outStream.Write(StatKey);
			outStream.Write(StatAmount);
			outStream.Write(DifficultyKey);
			outStream.Write(QuestKey);
		}

		public override int GetSize()
		{
			return 0x24;
		}
	}
}