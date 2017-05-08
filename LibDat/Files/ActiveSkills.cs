using System;
using System.IO;

namespace LibDat.Files
{
	public class ActiveSkills : BaseDat
	{
        [Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
		public int NameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NameStringData { get; set; }
        [Hidden]
		public int DescriptionStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString DescriptionStringData { get; set; }
        [Hidden]
		public int AlternateNameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AlternateNameStringData { get; set; }
        [Hidden]
		public int IconPathStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString IconPathStringData { get; set; }
        [Hidden]
        public int U00ListLength { get; set; }
        [Hidden]
        public int U00ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U00ListData { get; set; }
        public int CastTime { get; set; }
        [Hidden]
        public int U01ListLength { get; set; }
        [Hidden]
        public int U01ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U01ListData { get; set; }
        [Hidden]
        public int U02ListLength { get; set; }
        [Hidden]
        public int U02ListOffset { get; set; }
        [ResourceOnly]
        public UInt32List U02ListData { get; set; }
        [Hidden]
		public int WebsiteDescriptionStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString WebsiteDescriptionStringData { get; set; }
        [Hidden]
		public int WebsiteImageStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString WebsiteImageStringData { get; set; }
        public bool Flag0 { get; set; }
        [Hidden]
        public Int64 GrantedEffectKey { get; set; }
        [ExternalReference]
        public GrantedEffects GrantedEffectRef { get; set; }
        public bool Flag1 { get; set; }
        [Hidden]
        public int MetadataStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MetadataStringData { get; set; }

		public ActiveSkills(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			NameStringOffset = inStream.ReadInt32();
			DescriptionStringOffset = inStream.ReadInt32();
			AlternateNameStringOffset = inStream.ReadInt32();
			IconPathStringOffset = inStream.ReadInt32();
			U00ListLength = inStream.ReadInt32();
			U00ListOffset = inStream.ReadInt32();
			CastTime = inStream.ReadInt32();
			U01ListLength = inStream.ReadInt32();
			U01ListOffset = inStream.ReadInt32();
			U02ListLength = inStream.ReadInt32();
			U02ListOffset = inStream.ReadInt32();
			WebsiteDescriptionStringOffset = inStream.ReadInt32();
			WebsiteImageStringOffset = inStream.ReadInt32();
			Flag0 = inStream.ReadBoolean();
			GrantedEffectKey = inStream.ReadInt64();
			Flag1 = inStream.ReadBoolean();
			MetadataStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(CodeStringOffset);
			outStream.Write(NameStringOffset);
			outStream.Write(DescriptionStringOffset);
			outStream.Write(AlternateNameStringOffset);
			outStream.Write(IconPathStringOffset);
			outStream.Write(U00ListLength);
			outStream.Write(U00ListOffset);
			outStream.Write(CastTime);
			outStream.Write(U01ListLength);
			outStream.Write(U01ListOffset);
			outStream.Write(U02ListLength);
			outStream.Write(U02ListOffset);
			outStream.Write(WebsiteDescriptionStringOffset);
			outStream.Write(WebsiteImageStringOffset);
			outStream.Write(Flag0);
			outStream.Write(GrantedEffectKey);
			outStream.Write(Flag1);
			outStream.Write(MetadataStringOffset);
		}

		public override int GetSize()
		{
			return 0x46;
		}

        public override string ToString()
        {
            return NameStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}
