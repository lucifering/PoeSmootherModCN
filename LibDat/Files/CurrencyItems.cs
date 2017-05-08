using System;
using System.IO;

namespace LibDat.Files
{
	public class CurrencyItems : BaseDat
	{
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        public int StackSize { get; set; }
		public int CurrencyUseType { get; set; }
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int HelpStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString HelpStringData { get; set; }
        [Hidden]
        public Int64 YieldsToKey { get; set; }
        [ExternalReference]
        public BaseItemTypes YieldsToRef { get; set; }
        [Hidden]
        public int DescriptionStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString DescriptionStringData { get; set; }
        [Hidden]
        public Int64 NemesisAchievementKey { get; set; }
        [ExternalReference]
        public AchievementItems NemesisAchievementRef { get; set; }
        public bool Flag1 { get; set; }
        [Hidden]
        public int CategoryStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CategoryStringData { get; set; }
        [Hidden]
        public Int64 NormalAchievementKey { get; set; }
        [ExternalReference]
        public AchievementItems NormalAchievementRef { get; set; }

		public CurrencyItems(BinaryReader inStream)
		{
            ItemKey = inStream.ReadInt64();
			StackSize = inStream.ReadInt32();
			CurrencyUseType = inStream.ReadInt32();
            CodeStringOffset = inStream.ReadInt32();
            HelpStringOffset = inStream.ReadInt32();
            YieldsToKey = inStream.ReadInt64();
            DescriptionStringOffset = inStream.ReadInt32();
            NemesisAchievementKey = inStream.ReadInt64();
			Flag1 = inStream.ReadBoolean();
            CategoryStringOffset = inStream.ReadInt32();
            NormalAchievementKey = inStream.ReadInt64();
        }
		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(ItemKey);
			outStream.Write(StackSize);
			outStream.Write(CurrencyUseType);
            outStream.Write(CodeStringOffset);
            outStream.Write(HelpStringOffset);
            outStream.Write(YieldsToKey);
            outStream.Write(DescriptionStringOffset);
            outStream.Write(NemesisAchievementKey);
			outStream.Write(Flag1);
            outStream.Write(CategoryStringOffset);
            outStream.Write(NormalAchievementKey);
        }

		public override int GetSize()
		{
			return 0x39;
		}
	}
}
