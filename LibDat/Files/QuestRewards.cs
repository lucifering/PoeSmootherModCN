using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibDat.Files
{
	public class QuestRewards : BaseDat
	{
        [Hidden]
        public Int64 QuestKey { get; set; }
        [ExternalReference]
        public Quest QuestRef { get; set; }
		public int DifficultyKey { get; set; }
		public int U03 { get; set; }
        [Hidden]
        public Int64 CharacterKey { get; set; }
        [ExternalReference]
        public Characters CharacterRef { get; set; }
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        public int ItemLevel { get; set; }
		public int ItemRarity { get; set; }
		public int U09 { get; set; }
        [Hidden]
        public int U10StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U10StringData { get; set; }
        public Int64 U11 { get; set; }

		public QuestRewards(BinaryReader inStream)
		{
			QuestKey = inStream.ReadInt64();
			DifficultyKey = inStream.ReadInt32();
			U03 = inStream.ReadInt32();
			CharacterKey = inStream.ReadInt64();
            ItemKey = inStream.ReadInt64();
			ItemLevel = inStream.ReadInt32();
			ItemRarity = inStream.ReadInt32();
			U09 = inStream.ReadInt32();
			U10StringOffset = inStream.ReadInt32();
			U11 = inStream.ReadInt64();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(QuestKey);
			outStream.Write(DifficultyKey);
			outStream.Write(U03);
			outStream.Write(CharacterKey);
			outStream.Write(ItemKey);
			outStream.Write(ItemLevel);
			outStream.Write(ItemRarity);
			outStream.Write(U09);
			outStream.Write(U10StringOffset);
			outStream.Write(U11);
		}

		public override int GetSize()
		{
			return 0x38;
		}
	}
}