using System;
using System.IO;

namespace LibDat.Files
{
    public class MicrotransactionSocialFrameVariations : BaseDat
	{
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }

        public MicrotransactionSocialFrameVariations(BinaryReader inStream)
		{
            ItemKey = inStream.ReadInt64();
        }

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(ItemKey);
        }

		public override int GetSize()
		{
			return 0x08;
		}
	}
}