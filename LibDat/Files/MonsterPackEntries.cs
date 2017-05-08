using System;
using System.IO;

namespace LibDat.Files
{
	public class MonsterPackEntries : BaseDat
	{
		[Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public Int64 PackKey { get; set; }
        [ExternalReference]
        public MonsterPacks PackRef { get; set; }
        public bool Flag0 { get; set; }
		public int Percent { get; set; }
        [Hidden]
        public Int64 VarietyKey { get; set; }
        [ExternalReference]
        public MonsterVarieties VarietyRef { get; set; }

		public MonsterPackEntries(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			PackKey = inStream.ReadInt64();
			Flag0 = inStream.ReadBoolean();
			Percent = inStream.ReadInt32();
			VarietyKey = inStream.ReadInt64();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(PackKey);
			outStream.Write(Flag0);
			outStream.Write(Percent);
			outStream.Write(VarietyKey);
		}

		public override int GetSize()
		{
			return 0x19;
		}
	}
}