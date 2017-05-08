using System;
using System.IO;

namespace LibDat.Files
{
	public class QuestStates : BaseDat
	{
        [Hidden]
        public Int64 QuestKey { get; set; }
        [ExternalReference]
        public Quest QuestRef { get; set; }
        public int U02 { get; set; }
		[Hidden]
        public int QuestFlagsListLength { get; set; }
        [Hidden]
        public int QuestFlagsListOffset { get; set; }
        [ResourceOnly]
        public UInt32List QuestFlagsListData { get; set; }
        [Hidden]
        public int U04ListLength { get; set; }
        [Hidden]
        public int U04ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U04ListData { get; set; }
        [Hidden]
		public int StartTextStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString StartTextStringData { get; set; }
        public bool Flag0 { get; set; }
        [Hidden]
        public int EndTextStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString EndTextStringData { get; set; }
        [Hidden]
        public int MapPinsListLength { get; set; }
        [Hidden]
        public int MapPinsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List MapPinsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<MapPins> MapPinsListRef { get; set; }
        public int U09 { get; set; }

		public QuestStates(BinaryReader inStream)
		{
			QuestKey = inStream.ReadInt64();
			U02 = inStream.ReadInt32();
			QuestFlagsListLength = inStream.ReadInt32();
			QuestFlagsListOffset = inStream.ReadInt32();
			U04ListLength = inStream.ReadInt32();
			U04ListOffset = inStream.ReadInt32();
            StartTextStringOffset = inStream.ReadInt32();
            Flag0 = inStream.ReadBoolean();
            EndTextStringOffset = inStream.ReadInt32();
            MapPinsListLength = inStream.ReadInt32();
            MapPinsListOffset = inStream.ReadInt32();
			U09 = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(QuestKey);
			outStream.Write(U02);
			outStream.Write(QuestFlagsListLength);
			outStream.Write(QuestFlagsListOffset);
			outStream.Write(U04ListLength);
			outStream.Write(U04ListOffset);
            outStream.Write(StartTextStringOffset);
            outStream.Write(Flag0);
            outStream.Write(EndTextStringOffset);
            outStream.Write(MapPinsListLength);
            outStream.Write(MapPinsListOffset);
			outStream.Write(U09);
		}

		public override int GetSize()
		{
			return 0x31;
		}
	}
}
