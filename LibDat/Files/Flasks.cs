using System;
using System.IO;

namespace LibDat.Files
{
	public class Flasks : BaseDat
	{
        [Hidden]
        public Int64 ItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes ItemRef { get; set; }
        [Hidden]
        public int NameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NameStringData { get; set; }
        public int FlaskType { get; set; }
        public int _local_flask_life_to_recover { get; set; }
        public int _local_flask_mana_to_recover { get; set; }
        public int _local_flask_deciseconds_to_recover { get; set; }
        [Hidden]
        public Int64 BuffKey { get; set; }
        [ExternalReference]
        public BuffDefinitions BuffRef { get; set; }
        [Hidden]
        public int U06ListLength { get; set; }
        [Hidden]
        public int U06ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U06ListData { get; set; }

		public Flasks(BinaryReader inStream)
		{
			ItemKey = inStream.ReadInt64();
            NameStringOffset = inStream.ReadInt32();
			FlaskType = inStream.ReadInt32();
            _local_flask_life_to_recover = inStream.ReadInt32();
            _local_flask_mana_to_recover = inStream.ReadInt32();
            _local_flask_deciseconds_to_recover = inStream.ReadInt32();
			BuffKey = inStream.ReadInt64();
			U06ListLength = inStream.ReadInt32();
			U06ListOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(ItemKey);
            outStream.Write(NameStringOffset);
			outStream.Write(FlaskType);
            outStream.Write(_local_flask_life_to_recover);
            outStream.Write(_local_flask_mana_to_recover);
            outStream.Write(_local_flask_deciseconds_to_recover);
			outStream.Write(BuffKey);
			outStream.Write(U06ListLength);
			outStream.Write(U06ListOffset);
		}

		public override int GetSize()
		{
			return 0x2C;
		}
	}
}