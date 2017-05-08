using System;
using System.IO;

namespace LibDat.Files
{
	public class DescentRewardChests : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int Marauder1ListLength { get; set; }
        [Hidden]
        public int Marauder1ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Marauder1ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Marauder1ListRef { get; set; }
        [Hidden]
        public int Marauder2ListLength { get; set; }
        [Hidden]
        public int Marauder2ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Marauder2ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Marauder2ListRef { get; set; }
        [Hidden]
        public int Duelist1ListLength { get; set; }
        [Hidden]
        public int Duelist1ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Duelist1ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Duelist1ListRef { get; set; }
        [Hidden]
        public int Duelist2ListLength { get; set; }
        [Hidden]
        public int Duelist2ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Duelist2ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Duelist2ListRef { get; set; }
        [Hidden]
        public int Ranger1ListLength { get; set; }
        [Hidden]
        public int Ranger1ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Ranger1ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Ranger1ListRef { get; set; }
        [Hidden]
        public int Ranger2ListLength { get; set; }
        [Hidden]
        public int Ranger2ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Ranger2ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Ranger2ListRef { get; set; }
        [Hidden]
        public int Shadow1ListLength { get; set; }
        [Hidden]
        public int Shadow1ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Shadow1ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Shadow1ListRef { get; set; }
        [Hidden]
        public int Shadow2ListLength { get; set; }
        [Hidden]
        public int Shadow2ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Shadow2ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Shadow2ListRef { get; set; }
        [Hidden]
        public int Witch1ListLength { get; set; }
        [Hidden]
        public int Witch1ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Witch1ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Witch1ListRef { get; set; }
        [Hidden]
        public int Witch2ListLength { get; set; }
        [Hidden]
        public int Witch2ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Witch2ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Witch2ListRef { get; set; }
        [Hidden]
        public int Templar1ListLength { get; set; }
        [Hidden]
        public int Templar1ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Templar1ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Templar1ListRef { get; set; }
        [Hidden]
        public int Templar2ListLength { get; set; }
        [Hidden]
        public int Templar2ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Templar2ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Templar2ListRef { get; set; }
        [Hidden]
        public Int64 AreaKey { get; set; }
        [ExternalReference]
        public WorldAreas AreaRef { get; set; }
        [Hidden]
        public int Scion1ListLength { get; set; }
        [Hidden]
        public int Scion1ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Scion1ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Scion1ListRef { get; set; }
        [Hidden]
        public int Scion2ListLength { get; set; }
        [Hidden]
        public int Scion2ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List Scion2ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> Scion2ListRef { get; set; }

		public DescentRewardChests()
		{

		}
		public DescentRewardChests(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            Marauder1ListLength = inStream.ReadInt32();
            Marauder1ListOffset = inStream.ReadInt32();
            Marauder2ListLength = inStream.ReadInt32();
            Marauder2ListOffset = inStream.ReadInt32();
            Duelist1ListLength = inStream.ReadInt32();
            Duelist1ListOffset = inStream.ReadInt32();
            Duelist2ListLength = inStream.ReadInt32();
            Duelist2ListOffset = inStream.ReadInt32();
            Ranger1ListLength = inStream.ReadInt32();
            Ranger1ListOffset = inStream.ReadInt32();
            Ranger2ListLength = inStream.ReadInt32();
            Ranger2ListOffset = inStream.ReadInt32();
            Shadow1ListLength = inStream.ReadInt32();
            Shadow1ListOffset = inStream.ReadInt32();
            Shadow2ListLength = inStream.ReadInt32();
            Shadow2ListOffset = inStream.ReadInt32();
            Witch1ListLength = inStream.ReadInt32();
            Witch1ListOffset = inStream.ReadInt32();
            Witch2ListLength = inStream.ReadInt32();
            Witch2ListOffset = inStream.ReadInt32();
            Templar1ListLength = inStream.ReadInt32();
            Templar1ListOffset = inStream.ReadInt32();
            Templar2ListLength = inStream.ReadInt32();
            Templar2ListOffset = inStream.ReadInt32();
			AreaKey = inStream.ReadInt64();
            Scion1ListLength = inStream.ReadInt32();
            Scion1ListOffset = inStream.ReadInt32();
            Scion2ListLength = inStream.ReadInt32();
            Scion2ListOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(Marauder1ListLength);
            outStream.Write(Marauder1ListOffset);
            outStream.Write(Marauder2ListLength);
            outStream.Write(Marauder2ListOffset);
            outStream.Write(Duelist1ListLength);
            outStream.Write(Duelist1ListOffset);
            outStream.Write(Duelist2ListLength);
            outStream.Write(Duelist2ListOffset);
            outStream.Write(Ranger1ListLength);
            outStream.Write(Ranger1ListOffset);
            outStream.Write(Ranger2ListLength);
            outStream.Write(Ranger2ListOffset);
            outStream.Write(Shadow1ListLength);
            outStream.Write(Shadow1ListOffset);
            outStream.Write(Shadow2ListLength);
            outStream.Write(Shadow2ListOffset);
            outStream.Write(Witch1ListLength);
            outStream.Write(Witch1ListOffset);
            outStream.Write(Witch2ListLength);
            outStream.Write(Witch2ListOffset);
            outStream.Write(Templar1ListLength);
            outStream.Write(Templar1ListOffset);
            outStream.Write(Templar2ListLength);
            outStream.Write(Templar2ListOffset);
			outStream.Write(AreaKey);
            outStream.Write(Scion1ListLength);
            outStream.Write(Scion1ListOffset);
            outStream.Write(Scion2ListLength);
            outStream.Write(Scion2ListOffset);
		}

		public override int GetSize()
		{
			return 0x7c;
		}
	}
}