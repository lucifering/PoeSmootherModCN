using System;
using System.IO;

namespace LibDat.Files
{
	public class Dances : BaseDat
	{
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        [Hidden]
        public Int64 CharacterKey { get; set; }
        [ExternalReference]
        public Characters CharacterRef { get; set; }

		public Dances(BinaryReader inStream)
		{
			ItemKey = inStream.ReadInt64();
			CharacterKey = inStream.ReadInt64();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(ItemKey);
			outStream.Write(CharacterKey);
		}

		public override int GetSize()
		{
			return 0x10;
		}
	}
}