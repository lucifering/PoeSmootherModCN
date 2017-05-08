using System;
using System.IO;

namespace LibDat.Files
{
	public class ItemisedVisualEffect : BaseDat
	{
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        [Hidden]
        public Int64 VisualEffectKey { get; set; }
        [ExternalReference]
        public ItemVisualEffect VisualEffectRef { get; set; }
        [Hidden]
        public Int64 VisualIdentityKey { get; set; }
        [ExternalReference]
        public ItemVisualIdentity VisualIdentityRef { get; set; }
        [Hidden]
        public Int64 VisualIdentity2Key { get; set; }
        [ExternalReference]
        public ItemVisualIdentity VisualIdentity2Ref { get; set; }
        [Hidden]
		public int StatsListLength { get; set; }
        [Hidden]
        public int StatsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List StatsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Stats> StatsListRef { get; set; }
        [Hidden]
        public int U06ListLength { get; set; }
        [Hidden]
        public int U06ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U06ListData { get; set; }
        [Hidden]
        public int GemsListLength { get; set; }
        [Hidden]
        public int GemsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List GemsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<BaseItemTypes> GemsListRef { get; set; }
        public bool Flag0 { get; set; }
        [Hidden]
        public int VariationIdListLength { get; set; }
        [Hidden]
        public int VariationIdListOffset { get; set; }
        [ResourceOnly]
        public UInt32List VariationIdListData { get; set; }

		public ItemisedVisualEffect(BinaryReader inStream)
		{
            ItemKey = inStream.ReadInt64();
            VisualEffectKey = inStream.ReadInt64();
			VisualIdentityKey = inStream.ReadInt64();
			VisualIdentity2Key = inStream.ReadInt64();
			StatsListLength = inStream.ReadInt32();
			StatsListOffset = inStream.ReadInt32();
			U06ListLength = inStream.ReadInt32();
			U06ListOffset = inStream.ReadInt32();
			GemsListLength = inStream.ReadInt32();
			GemsListOffset = inStream.ReadInt32();
			Flag0 = inStream.ReadBoolean();
			VariationIdListLength = inStream.ReadInt32();
			VariationIdListOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(ItemKey);
            outStream.Write(VisualEffectKey);
			outStream.Write(VisualIdentityKey);
			outStream.Write(VisualIdentity2Key);
			outStream.Write(StatsListLength);
			outStream.Write(StatsListOffset);
			outStream.Write(U06ListLength);
			outStream.Write(U06ListOffset);
			outStream.Write(GemsListLength);
			outStream.Write(GemsListOffset);
			outStream.Write(Flag0);
			outStream.Write(VariationIdListLength);
			outStream.Write(VariationIdListOffset);
		}

		public override int GetSize()
		{
			return 0x41;
		}
    }
}