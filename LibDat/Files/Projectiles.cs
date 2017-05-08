using System.IO;

namespace LibDat.Files
{
	public class Projectiles : BaseDat
	{
        [Hidden]
        public int MetadataStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString MetadataStringData { get; set; }
        [Hidden]
        public int AnimatedObjectStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString AnimatedObjectStringData { get; set; }
        [Hidden]
        public int ProjectileStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ProjectileStringData { get; set; }
        [Hidden]
        public int ProjectileExplosionStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ProjectileExplosionStringData { get; set; }
        public int U04 { get; set; }
        [Hidden]
        public int ParticleStringOffset { get; set; }
        [ResourceOnly]
        public UnicodeString ParticleStringData { get; set; }
        public bool Flag0 { get; set; }
		public int U06 { get; set; }
		public bool Flag1 { get; set; }

		public Projectiles(BinaryReader inStream)
		{
            MetadataStringOffset = inStream.ReadInt32();
            AnimatedObjectStringOffset = inStream.ReadInt32();
            ProjectileStringOffset = inStream.ReadInt32();
            ProjectileExplosionStringOffset = inStream.ReadInt32();
			U04 = inStream.ReadInt32();
            ParticleStringOffset = inStream.ReadInt32();
			Flag0 = inStream.ReadBoolean();
			U06 = inStream.ReadInt32();
			Flag1 = inStream.ReadBoolean();
		}

		public override void Save(BinaryWriter outStream)
		{
            outStream.Write(MetadataStringOffset);
            outStream.Write(AnimatedObjectStringOffset);
            outStream.Write(ProjectileStringOffset);
            outStream.Write(ProjectileExplosionStringOffset);
			outStream.Write(U04);
            outStream.Write(ParticleStringOffset);
			outStream.Write(Flag0);
			outStream.Write(U06);
			outStream.Write(Flag1);
		}

		public override int GetSize()
		{
			return 0x1E;
		}
	}
}