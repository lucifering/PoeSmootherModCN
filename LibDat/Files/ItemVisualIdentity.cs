using System;
using System.IO;

namespace LibDat.Files
{
	public class ItemVisualIdentity : BaseDat
	{
		[Hidden]
		public int CodeStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString CodeStringData { get; set; }
        [Hidden]
        public int IdentityStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString IdentityStringData { get; set; }
        [Hidden]
        public int AnimatedObjectStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AnimatedObjectStringData { get; set; }
        [Hidden]
        public Int64 SoundEffectKey { get; set; }
        [ExternalReference]
        public SoundEffects SoundEffectRef { get; set; }
        public int HexFlavourTextHash { get; set; }
        [Hidden]
        public int EffectStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString EffectStringData { get; set; }
        [Hidden]
        public int MarauderModelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MarauderModelStringData { get; set; }
        [Hidden]
        public int RangerModelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString RangerModelStringData { get; set; }
        [Hidden]
        public int WitchModelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString WitchModelStringData { get; set; }
        [Hidden]
        public int DuelistModelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString DuelistModelStringData { get; set; }
        [Hidden]
        public int TemplarModelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString TemplarModelStringData { get; set; }
        [Hidden]
        public int ShadowModelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ShadowModelStringData { get; set; }
        [Hidden]
        public int ScionModelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ScionModelStringData { get; set; }
        [Hidden]
        public int U10StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U10StringData { get; set; }
        [Hidden]
        public int U11StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U11StringData { get; set; }
        [Hidden]
        public int U12StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U12StringData { get; set; }
        [Hidden]
        public int U13StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U13StringData { get; set; }
        [Hidden]
        public int U14StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U14StringData { get; set; }
        [Hidden]
        public int U15StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U15StringData { get; set; }
        [Hidden]
        public int U16StringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString U16StringData { get; set; }
        public int U17 { get; set; }
		public int U18 { get; set; }
        [Hidden]
        public int AchievementListLength { get; set; }
        [Hidden]
        public int AchievementListOffset { get; set; } //todo: list of achievement items
        [Hidden]
        [ResourceOnly]
        public UInt64List AchievementListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<AchievementItems> AchievementListRef { get; set; }
        [Hidden]
        public int AnimatedModelStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AnimatedModelStringData { get; set; }

		public ItemVisualIdentity(BinaryReader inStream)
		{
			CodeStringOffset = inStream.ReadInt32();
			IdentityStringOffset = inStream.ReadInt32();
			AnimatedObjectStringOffset = inStream.ReadInt32();
            SoundEffectKey = inStream.ReadInt64();
			HexFlavourTextHash = inStream.ReadInt32();
            EffectStringOffset = inStream.ReadInt32();
			MarauderModelStringOffset = inStream.ReadInt32();
			RangerModelStringOffset = inStream.ReadInt32();
			WitchModelStringOffset = inStream.ReadInt32();
			DuelistModelStringOffset = inStream.ReadInt32();
			TemplarModelStringOffset = inStream.ReadInt32();
			ShadowModelStringOffset = inStream.ReadInt32();
            ScionModelStringOffset = inStream.ReadInt32();
			U10StringOffset = inStream.ReadInt32();
			U11StringOffset = inStream.ReadInt32();
			U12StringOffset = inStream.ReadInt32();
			U13StringOffset = inStream.ReadInt32();
			U14StringOffset = inStream.ReadInt32();
			U15StringOffset = inStream.ReadInt32();
			U16StringOffset = inStream.ReadInt32();
			U17 = inStream.ReadInt32();
			U18 = inStream.ReadInt32();
			AchievementListLength = inStream.ReadInt32();
			AchievementListOffset = inStream.ReadInt32();
            AnimatedModelStringOffset = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
            throw new Exception("out of date");
			outStream.Write(CodeStringOffset);
			outStream.Write(IdentityStringOffset);
			outStream.Write(AnimatedObjectStringOffset);
			outStream.Write(MarauderModelStringOffset);
			outStream.Write(RangerModelStringOffset);
			outStream.Write(WitchModelStringOffset);
			outStream.Write(DuelistModelStringOffset);
			outStream.Write(TemplarModelStringOffset);
			outStream.Write(ShadowModelStringOffset);
            outStream.Write(ScionModelStringOffset);
			outStream.Write(U10StringOffset);
			outStream.Write(U11StringOffset);
			outStream.Write(U12StringOffset);
			outStream.Write(U13StringOffset);
			outStream.Write(U14StringOffset);
			outStream.Write(U15StringOffset);
			outStream.Write(U16StringOffset);
			outStream.Write(U17);
			outStream.Write(U18);
			outStream.Write(AchievementListLength);
			outStream.Write(AchievementListOffset);
            outStream.Write(AnimatedModelStringOffset);
		}

		public override int GetSize()
		{
			return 0x68;
		}

        public override string ToString()
        {
            return CodeStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}