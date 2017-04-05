namespace LibGGPK
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Simplifies the task of creating records from data read in from the pack file
    /// </summary>
    internal static class RecordFactory
    {
        /// <summary>
        /// Reads a single record from the specified stream and creates the appropriate record type.
        /// </summary>
        /// <param name="br">Stream pointing at a record</param>
        /// <returns></returns>
        public static BaseRecord ReadRecord(BinaryReader br)
        {
            uint length = br.ReadUInt32();
            byte[] rb = br.ReadBytes(4);
            string tag = Encoding.ASCII.GetString(rb);

            switch (tag)
            {
                case FileRecord.Tag:
                    return new FileRecord(length, br);

                case GGPKRecord.Tag:
                    return new GGPKRecord(length, br);

                case FreeRecord.Tag:
                    return new FreeRecord(length, br);

                case DirectoryRecord.Tag:
                    return new DirectoryRecord(length, br);
            }

            
            throw new Exception(string.Format("Invalid tag:{0}-{1}-{2}-{3}", rb[0].ToString("X2"), rb[1].ToString("X2"), rb[2].ToString("X2"), rb[3].ToString("X2")));
        }
    }
}