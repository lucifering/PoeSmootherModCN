using System;
using System.IO;

namespace LibDat.Files
{
	public class WeaponTypes : BaseDat
	{
        [Hidden]
        public Int64 BaseItemKey { get; set; }
        [ExternalReference]
        public BaseItemTypes BaseItemRef { get; set; }
        public int CriticalChance { get; set; }
		public int WeaponSpeed { get; set; }
		public int DamageMin { get; set; }
		public int DamageMax { get; set; }
		public int RangeMax { get; set; }
		public int RangeMin { get; set; }

		public WeaponTypes(BinaryReader inStream)
		{
			BaseItemKey = inStream.ReadInt64();
			CriticalChance = inStream.ReadInt32();
			WeaponSpeed = inStream.ReadInt32();
			DamageMin = inStream.ReadInt32();
			DamageMax = inStream.ReadInt32();
			RangeMax = inStream.ReadInt32();
			RangeMin = inStream.ReadInt32();
		}

		public override void Save(BinaryWriter outStream)
		{
			outStream.Write(BaseItemKey);
			outStream.Write(CriticalChance);
			outStream.Write(WeaponSpeed);
			outStream.Write(DamageMin);
			outStream.Write(DamageMax);
			outStream.Write(RangeMax);
			outStream.Write(RangeMin);
		}

		public override int GetSize()
		{
			return 0x20;
		}
	}
}