using System;
using System.IO;

namespace LibDat
{
    public abstract class BaseData : IComparable
	{
		/// <summary>
		/// Save this record to the specified stream. Stream position is not preserved.
		/// </summary>
		/// <param name="outStream">Stream to write contents to</param>
		public abstract void Save(BinaryWriter outStream);

        public virtual int CompareTo(object obj)
        {
            BaseData other = (BaseData)obj;
            return this.ToString().CompareTo(other.ToString());
        }
    }
}