using System;
using System.IO;

namespace LibDat.Files
{
	public class MapConnections : BaseDat
	{
        [Hidden]
        public Int64 Zone1Key { get; set; }
        [ExternalReference]
        public MapPins Zone1Ref { get; set; }
        [Hidden]
        public Int64 Zone2Key { get; set; }
        [ExternalReference]
        public MapPins Zone2Ref { get; set; }
        public int U02 { get; set; }
        [Hidden]
        public int RestrictedAreaTextStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString RestrictedAreaTextStringData { get; set; }
        public int U03 { get; set; }
		public int U04 { get; set; }
		public int U05 { get; set; }

		public MapConnections(BinaryReader inStream)
		{
			Zone1Key = inStream.ReadInt64();
			Zone2Key = inStream.ReadInt64();
			U02 = inStream.ReadInt32();
            RestrictedAreaTextStringOffset = inStream.ReadInt32();
			U03 = inStream.ReadInt32();
			U04 = inStream.ReadInt32();
			U05 = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(Zone1Key);
			outStream.Write(Zone2Key);
			outStream.Write(U02);
            outStream.Write(RestrictedAreaTextStringOffset);
			outStream.Write(U03);
			outStream.Write(U04);
			outStream.Write(U05);
		}

		public override int GetSize()
		{
			return 0x24;
		}
	}
}