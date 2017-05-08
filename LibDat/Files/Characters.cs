using System;
using System.IO;

namespace LibDat.Files
{
	public class Characters : BaseDat
	{
        [Hidden]
        public int MetadataStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MetadataStringData { get; set; }
        [Hidden]
        public int NameStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString NameStringData { get; set; }
        [Hidden]
        public int AnimatedObjectStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AnimatedObjectStringData { get; set; }
        [Hidden]
        public int ActorStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ActorStringData { get; set; }
        public int BaseMaxLife { get; set; }
		public int BaseMaxMana { get; set; }
		public int WeaponSpeed { get; set; }
		public int MinDamage { get; set; }
		public int MaxDamage { get; set; }
		public int MaxAttackDistance { get; set; } // possibly wrong
        [Hidden]
        public int IconStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString IconStringData { get; set; }
        public int U06 { get; set; }
		public int BaseStrength { get; set; }
		public int BaseDexterity { get; set; }
		public int BaseIntelligence { get; set; }
        [Hidden]
        public int KnownSkillsListLength { get; set; }
        [Hidden]
        public int KnownSkillsListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List KnownSkillsListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<GrantedEffectsPerLevel> KnownSkillsListRef { get; set; }
        [Hidden]
        public int DescriptionStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString DescriptionStringData { get; set; }
        [Hidden]
        public Int64 StartingGemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes StartingGemRef { get; set; }
        public int U13 { get; set; }
		public int U14 { get; set; }
		public int U15 { get; set; }
		public int U16 { get; set; }
        public int U17 { get; set; }
        [Hidden]
        public int AudioStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AudioStringData { get; set; }

		public Characters(BinaryReader inStream)
		{
            MetadataStringOffset = inStream.ReadInt32();
            NameStringOffset = inStream.ReadInt32();
            AnimatedObjectStringOffset = inStream.ReadInt32();
            ActorStringOffset = inStream.ReadInt32();
			BaseMaxLife = inStream.ReadInt32();
			BaseMaxMana = inStream.ReadInt32();
			WeaponSpeed = inStream.ReadInt32();
			MinDamage = inStream.ReadInt32();
			MaxDamage = inStream.ReadInt32();
			MaxAttackDistance = inStream.ReadInt32();
            IconStringOffset = inStream.ReadInt32();
			U06 = inStream.ReadInt32();
			BaseStrength = inStream.ReadInt32();
			BaseDexterity = inStream.ReadInt32();
			BaseIntelligence = inStream.ReadInt32();
			KnownSkillsListLength = inStream.ReadInt32();
			KnownSkillsListOffset = inStream.ReadInt32();
            DescriptionStringOffset = inStream.ReadInt32();
			StartingGemKey = inStream.ReadInt64();
			U13 = inStream.ReadInt32();
			U14 = inStream.ReadInt32();
			U15 = inStream.ReadInt32();
			U16 = inStream.ReadInt32();
            U17 = inStream.ReadInt32();
            AudioStringOffset = inStream.ReadInt32();
		}

		public override void Save(System.IO.BinaryWriter outStream)
		{
            outStream.Write(MetadataStringOffset);
            outStream.Write(NameStringOffset);
            outStream.Write(AnimatedObjectStringOffset);
            outStream.Write(ActorStringOffset);
			outStream.Write(BaseMaxLife);
			outStream.Write(BaseMaxMana);
			outStream.Write(WeaponSpeed);
			outStream.Write(MinDamage);
			outStream.Write(MaxDamage);
			outStream.Write(MaxAttackDistance);
            outStream.Write(IconStringOffset);
			outStream.Write(U06);
			outStream.Write(BaseStrength);
			outStream.Write(BaseDexterity);
			outStream.Write(BaseIntelligence);
			outStream.Write(KnownSkillsListLength);
			outStream.Write(KnownSkillsListOffset);
            outStream.Write(DescriptionStringOffset);
			outStream.Write(StartingGemKey);
			outStream.Write(U13);
			outStream.Write(U14);
			outStream.Write(U15);
			outStream.Write(U16);
            outStream.Write(U17);
            outStream.Write(AudioStringOffset);
		}

		public override int GetSize()
		{
			return 0x68;
		}

        public override string ToString()
        {
            return NameStringData.ToString() + " [" + Key.ToString() + "]";
        }
	}
}
