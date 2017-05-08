using System.IO;

namespace LibDat.Files
{
	public class ShopCategory : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int LabelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LabelStringData { get; set; }
        [Hidden]
        public int IconStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString IconStringData { get; set; }
        [Hidden]
        public int CategoryStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CategoryStringData { get; set; }
        [Hidden]
        public int IconUrlStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString IconUrlStringData { get; set; }
        [Hidden]
        public int ErrorImageStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ErrorImageStringData { get; set; }

		public ShopCategory()
		{
			
		}
		public ShopCategory(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            LabelStringOffset = inStream.ReadInt32();
            IconStringOffset = inStream.ReadInt32();
            CategoryStringOffset = inStream.ReadInt32();
            IconUrlStringOffset = inStream.ReadInt32();
            ErrorImageStringOffset = inStream.ReadInt32();
        }

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(LabelStringOffset);
            outStream.Write(IconStringOffset);
            outStream.Write(CategoryStringOffset);
            outStream.Write(IconUrlStringOffset);
            outStream.Write(ErrorImageStringOffset);
        }

		public override int GetSize()
		{
			return 0x18;
		}
	}
}