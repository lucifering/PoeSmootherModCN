using System.IO;

namespace LibDat.Files
{
	public class ShopPaymentPackage : BaseDat
	{
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int LabelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString LabelStringData { get; set; }
        public int Coins { get; set; }
		public int Price { get; set; }
        public int HexU04 { get; set; }
        public bool U05 { get; set; }

		public ShopPaymentPackage(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            LabelStringOffset = inStream.ReadInt32();
			Coins = inStream.ReadInt32();
			Price = inStream.ReadInt32();
            HexU04 = inStream.ReadInt32(); //todo: check this
            // https://code.google.com/p/libggpk/source/diff?spec=svn1016e34d0c6a4aaefc25d1ba11f0fced12f9f475&r=1016e34d0c6a4aaefc25d1ba11f0fced12f9f475&format=side&path=/LibDat/Files/ShopPaymentPackage.cs
            U05 = inStream.ReadBoolean();
        }

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(LabelStringOffset);
			outStream.Write(Coins);
			outStream.Write(Price);
            outStream.Write(HexU04);
            outStream.Write(U05);
        }

		public override int GetSize()
		{
			return 0x15;
		}
	}
}