using System.IO;

namespace LibDat.Files
{
	public class CharacterAudioEvents : BaseDat
	{
		// It appears that the sound path strings are now prefixed with one or more 4-byte integers
		//   if the corresponding 'PrefixCount' is not 0. This file doesn't contain any translatable
		//   strings so it doesn't really matter.

        [Hidden]
        public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int MarauderListLength { get; set; }
        [Hidden]
        public int MarauderListOffset { get; set; }
        [ResourceOnly]
        public IndirectStringList MarauderListData { get; set; }
        [Hidden]
        public int RangerListLength { get; set; }
        [Hidden]
        public int RangerListOffset { get; set; }
        [ResourceOnly]
        public IndirectStringList RangerListData { get; set; }
        [Hidden]
        public int WitchListLength { get; set; }
        [Hidden]
        public int WitchListOffset { get; set; }
        [ResourceOnly]
        public IndirectStringList WitchListData { get; set; }
        [Hidden]
        public int DuelistListLength { get; set; }
        [Hidden]
        public int DuelistListOffset { get; set; }
        [ResourceOnly]
        public IndirectStringList DuelistListData { get; set; }
        [Hidden]
        public int ShadowListLength { get; set; }
        [Hidden]
        public int ShadowListOffset { get; set; }
        [ResourceOnly]
        public IndirectStringList ShadowListData { get; set; }
        [Hidden]
        public int TemplarListLength { get; set; }
        [Hidden]
        public int TemplarListOffset { get; set; }
        [ResourceOnly]
        public IndirectStringList TemplarListData { get; set; }
        [Hidden]
        public int ScionListLength { get; set; }
        [Hidden]
        public int ScionListOffset { get; set; }
        [ResourceOnly]
        public IndirectStringList ScionListData { get; set; }
        public int U00 { get; set; }
		public int U01 { get; set; }
		public int U02 { get; set; } // Sound for all classes?


		public CharacterAudioEvents(BinaryReader inStream)
		{
            CodeStringOffset = inStream.ReadInt32();
			MarauderListLength = inStream.ReadInt32();
			MarauderListOffset = inStream.ReadInt32();
			RangerListLength = inStream.ReadInt32();
			RangerListOffset = inStream.ReadInt32();
			WitchListLength = inStream.ReadInt32();
			WitchListOffset = inStream.ReadInt32();
			DuelistListLength = inStream.ReadInt32();
			DuelistListOffset = inStream.ReadInt32();
			ShadowListLength = inStream.ReadInt32();
			ShadowListOffset = inStream.ReadInt32();
			TemplarListLength = inStream.ReadInt32();
			TemplarListOffset = inStream.ReadInt32();
			ScionListLength = inStream.ReadInt32();
			ScionListOffset = inStream.ReadInt32();
			U00 = inStream.ReadInt32();
			U01 = inStream.ReadInt32();
			U02 = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(CodeStringOffset);
			outStream.Write(MarauderListLength);
			outStream.Write(MarauderListOffset);
			outStream.Write(RangerListLength);
			outStream.Write(RangerListOffset);
			outStream.Write(WitchListLength);
			outStream.Write(WitchListOffset);
			outStream.Write(DuelistListLength);
			outStream.Write(DuelistListOffset);
			outStream.Write(ShadowListLength);
			outStream.Write(ShadowListOffset);
			outStream.Write(TemplarListLength);
			outStream.Write(TemplarListOffset);
			outStream.Write(ScionListLength);
			outStream.Write(ScionListOffset);
			outStream.Write(U00);
			outStream.Write(U01);
			outStream.Write(U02);
		}

		public override int GetSize()
		{
			return 72;
		}
	}
}
