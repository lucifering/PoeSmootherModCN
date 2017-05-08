using System;
using System.IO;

namespace LibDat.Files
{
	public class ItemExperiencePerLevel : BaseDat
	{
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        public int Level { get; set; }
		public int XpAmount { get; set; }

		public ItemExperiencePerLevel(BinaryReader inStream)
		{
            ItemKey = inStream.ReadInt64();
			Level = inStream.ReadInt32();
			XpAmount = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(ItemKey);
			outStream.Write(Level);
			outStream.Write(XpAmount);
		}

		public override int GetSize()
		{
			return 0x10;
		}
	}
}