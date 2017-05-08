using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibDat.Files
{
    public class DescentExiles : BaseDat
    {
        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public Int64 AreaKey { get; set; }
        [ExternalReference]
        public WorldAreas AreaRef { get; set; }
        public Int64 U03 { get; set; }
        [Hidden]
        public Int64 VarietyKey { get; set; }
        [ExternalReference]
        public MonsterVarieties VarietyRef { get; set; }
        public int U07 { get; set; }

        public DescentExiles(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
            AreaKey = inStream.ReadInt64();
            U03 = inStream.ReadInt64();
            VarietyKey = inStream.ReadInt64();
            U07 = inStream.ReadInt32();
        }

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
            outStream.Write(AreaKey);
            outStream.Write(U03);
            outStream.Write(VarietyKey);
            outStream.Write(U07);
        }

		public override int GetSize()
		{
			return 0x20;
		}
   }
}
