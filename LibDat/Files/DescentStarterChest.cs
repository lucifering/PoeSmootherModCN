using System;
using System.IO;

namespace LibDat.Files
{
	public class DescentStarterChest : BaseDat
	{
		[Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public Int64 ClassKey { get; set; }
        [ExternalReference]
        public Characters ClassRef { get; set; }
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        [Hidden]
        public int SocketsStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString SocketsStringData { get; set; }
        [Hidden]
        public Int64 AreaKey { get; set; }
        [ExternalReference]
        public WorldAreas AreaRef { get; set; }

		public DescentStarterChest()
		{
			
		}
		public DescentStarterChest(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			ClassKey = inStream.ReadInt64();
            ItemKey = inStream.ReadInt64();
			SocketsStringOffset = inStream.ReadInt32();
			AreaKey = inStream.ReadInt64();

		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(ClassKey);
            outStream.Write(ItemKey);
			outStream.Write(SocketsStringOffset);
			outStream.Write(AreaKey);
		}

		public override int GetSize()
		{
			return 0x20;
		}
	}
}