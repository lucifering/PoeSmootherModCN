using System;
using System.IO;

namespace LibDat.Files
{
    public class MicrotransactionPortalVariations : BaseDat
	{
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        [Hidden]
        public int AnimatedObjectStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AnimatedObjectStringData { get; set; }

        public MicrotransactionPortalVariations(BinaryReader inStream)
		{
            ItemKey = inStream.ReadInt64();
            AnimatedObjectStringOffset = inStream.ReadInt32();
        }

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(ItemKey);
            outStream.Write(AnimatedObjectStringOffset);
        }

		public override int GetSize()
		{
			return 0x0C;
		}
	}
}