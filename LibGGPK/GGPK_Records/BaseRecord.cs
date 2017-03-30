namespace LibGGPK
{
    using System.IO;

    /// <summary>
    /// Every record has a Length, just a common class for all the records
    /// </summary>
    public class BaseRecord
    {
        /// <summary>
        /// Length of the entire record in bytes
        /// </summary>
        protected uint Length;

        /// <summary>
        /// Offset in pack file where record begins
        /// </summary>
        public long RecordBegin;

        protected virtual void Read(BinaryReader br)
        {
        }
    }
}