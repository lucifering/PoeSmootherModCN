using System;
using System.IO;

namespace LibDat.Files
{
	public class ShieldTypes : BaseDat
	{
        [Hidden]
		public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        public int BlockChance { get; set; }

		public ShieldTypes(BinaryReader inStream)
		{
			ItemKey = inStream.ReadInt64();
			BlockChance = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(ItemKey);
			outStream.Write(BlockChance);
		}

		public override int GetSize()
		{
			return 0x0C;
		}
	}
}