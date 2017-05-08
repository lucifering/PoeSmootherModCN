using System;
using System.IO;

namespace LibDat.Files
{
	public class ArmourTypes : BaseDat
	{
        [Hidden]
		public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }

		public ArmourTypes(BinaryReader inStream)
		{
			ItemKey = inStream.ReadInt64();
		}

		public override void Save(System.IO.BinaryWriter outStream)
		{
			outStream.Write(ItemKey);
		}

		public override int GetSize()
		{
			return 0x08;
		}
	}
}
