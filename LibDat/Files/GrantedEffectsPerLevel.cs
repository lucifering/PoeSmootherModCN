using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibDat.Files
{
	public class GrantedEffectsPerLevel : BaseDat
	{
        [Hidden]
        public Int64 GrantedEffectsKey { get; set; }
        [ExternalReference]
        public GrantedEffects GrantedEffectsRef { get; set; }
		public int Level { get; set; }
        [Hidden]
        public int StatsListListLength { get; set; }
        [Hidden]
        public int StatsListListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List StatsListListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Stats> StatsListListRef { get; set; }
        public int Stat1Amount { get; set; }
		public int Stat2Amount { get; set; }
		public int Stat3Amount { get; set; }
		public int Stat4Amount { get; set; }
		public int Stat5Amount { get; set; }
		public int Stat6Amount { get; set; }
		public int Stat7Amount { get; set; }
		public int Stat8Amount { get; set; }
        [Hidden]
        public Int64 ActiveSkillKey { get; set; }
        [ExternalReference]
        public ActiveSkills ActiveSkillRef { get; set; }
        public int RequiredLevel1 { get; set; }
		public int ManaCostMultiplier { get; set; }
		public int RequiredLevel2 { get; set; }
        public int RequiredLevel3 { get; set; }
        [Hidden]
        public int QualityEffectListLength { get; set; }
        [Hidden]
        public int QualityEffectListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List QualityEffectListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Stats> QualityEffectListRef { get; set; }
        [Hidden]
        public int QualityAmountListLength { get; set; }
        [Hidden]
        public int QualityAmountListOffset { get; set; }
        [ResourceOnly]
        public UInt32List QualityAmountListData { get; set; }
        public int CriticalChancePercent { get; set; }
		public int ManaCost { get; set; }
        public int DamageEffectivenessPercent { get; set; }
		public int U26 { get; set; }
		public int U27 { get; set; }
		public int U28 { get; set; }
        [Hidden]
        public int U29ListLength { get; set; }
        [Hidden]
        public int U29ListOffset { get; set; }
        [Hidden]
        [ResourceOnly]
        public UInt64List U29ListData { get; set; }
        [ExternalReferenceList]
        public ExternalDatList<Stats> U29ListRef { get; set; }
        public bool Flag0 { get; set; }

		public GrantedEffectsPerLevel(BinaryReader inStream)
		{
            GrantedEffectsKey = inStream.ReadInt64();
			Level = inStream.ReadInt32();
			StatsListListLength = inStream.ReadInt32();
            StatsListListOffset = inStream.ReadInt32();
			Stat1Amount = inStream.ReadInt32();
			Stat2Amount = inStream.ReadInt32();
			Stat3Amount = inStream.ReadInt32();
			Stat4Amount = inStream.ReadInt32();
			Stat5Amount = inStream.ReadInt32();
			Stat6Amount = inStream.ReadInt32();
			Stat7Amount = inStream.ReadInt32();
			Stat8Amount = inStream.ReadInt32();
			ActiveSkillKey = inStream.ReadInt64();
			RequiredLevel1 = inStream.ReadInt32();
			ManaCostMultiplier = inStream.ReadInt32();
			RequiredLevel2 = inStream.ReadInt32();
			RequiredLevel3 = inStream.ReadInt32();
			QualityEffectListLength = inStream.ReadInt32();
			QualityEffectListOffset = inStream.ReadInt32();
			QualityAmountListLength = inStream.ReadInt32();
			QualityAmountListOffset = inStream.ReadInt32();
			CriticalChancePercent = inStream.ReadInt32();
			ManaCost = inStream.ReadInt32();
            DamageEffectivenessPercent = inStream.ReadInt32();
			U26 = inStream.ReadInt32();
			U27 = inStream.ReadInt32();
			U28 = inStream.ReadInt32();
			U29ListLength = inStream.ReadInt32();
			U29ListOffset = inStream.ReadInt32();
			Flag0 = inStream.ReadBoolean();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(GrantedEffectsKey);
			outStream.Write(Level);
			outStream.Write(StatsListListLength);
            outStream.Write(StatsListListOffset);
			outStream.Write(Stat1Amount);
			outStream.Write(Stat2Amount);
			outStream.Write(Stat3Amount);
			outStream.Write(Stat4Amount);
			outStream.Write(Stat5Amount);
			outStream.Write(Stat6Amount);
			outStream.Write(Stat7Amount);
			outStream.Write(Stat8Amount);
			outStream.Write(ActiveSkillKey);
			outStream.Write(RequiredLevel1);
			outStream.Write(ManaCostMultiplier);
			outStream.Write(RequiredLevel2);
			outStream.Write(RequiredLevel3);
			outStream.Write(QualityEffectListLength);
			outStream.Write(QualityEffectListOffset);
			outStream.Write(QualityAmountListLength);
			outStream.Write(QualityAmountListOffset);
			outStream.Write(CriticalChancePercent);
			outStream.Write(ManaCost);
            outStream.Write(DamageEffectivenessPercent);
			outStream.Write(U26);
			outStream.Write(U27);
			outStream.Write(U28);
			outStream.Write(U29ListLength);
			outStream.Write(U29ListOffset);
			outStream.Write(Flag0);
		}

		public override int GetSize()
		{
			return 0x7D;
		}

        public override string ToString()
        {
            GrantedEffects ge = ReferenceManager.Instance.AllDats["GrantedEffects.dat"].Where(x => (x as GrantedEffects).Key == GrantedEffectsKey).FirstOrDefault() as GrantedEffects;
            return ge.EffectNameStringData.ToString() + " [" + Key.ToString() + "]";
        }
    }
}