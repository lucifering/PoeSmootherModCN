using System;
using System.IO;

namespace LibDat.Files
{
	public class SkillGems : BaseDat
	{
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        [Hidden]
        public Int64 EffectKey { get; set; }
        [ExternalReference]
        public GrantedEffects EffectRef { get; set; }
        public int Str { get; set; }
		public int Dex { get; set; }
		public int Int { get; set; }
        [Hidden]
        public int TagsListLength { get; set; }
        [Hidden]
        public int TagsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List TagsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<GemTags> TagsListRef { get; set; }

		public SkillGems(BinaryReader inStream)
		{
			ItemKey = inStream.ReadInt64();
			EffectKey = inStream.ReadInt64();
			Str = inStream.ReadInt32();
			Dex = inStream.ReadInt32();
			Int = inStream.ReadInt32();
			TagsListLength = inStream.ReadInt32();
			TagsListOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(ItemKey);
			outStream.Write(EffectKey);
			outStream.Write(Str);
			outStream.Write(Dex);
			outStream.Write(Int);
			outStream.Write(TagsListLength);
			outStream.Write(TagsListOffset);
		}

		public override int GetSize()
		{
			return 0x24;
		}
	}
}