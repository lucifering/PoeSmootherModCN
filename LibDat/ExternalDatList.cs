using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Linq;
using LibDat.Files;

namespace LibDat
{
    /// <summary>
    /// Represents a list of BaseDat, built from an UInt64List
    /// </summary>
    public class ExternalDatList<T> : BaseData
    {
        /// <summary>
        /// Number of elements in the list
        /// </summary>
        public int ListLength { get; set; }
        /// <summary>
        /// The list of BaseDat
        /// </summary>
        public List<BaseDat> Data { get; set; }

        public ExternalDatList()
        {
        }

        /// <summary>
        /// Useless here
        /// </summary>
        /// <param name="outStream"></param>
        public override void Save(BinaryWriter outStream)
        {
            //TODO
        }

        public override string ToString()
        {
            if (Data.Count == 0) return "";
            StringBuilder sb = new StringBuilder();
            foreach (var s in Data)
            {
                sb.Append(s.ToString()).Append("\n");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        public string ToStringNoKey()
        {
            if (Data.Count == 0) return "";
            StringBuilder sb = new StringBuilder();
            foreach (var s in Data.OrderBy(x=>x.Key))
            {
                sb.Append(s.ToStringNoKey()).Append("\n");
            }
            return sb.ToString();
        }

        public string ToStringWiki()
        {
            if (Data.Count == 0) return "";
            StringBuilder sb = new StringBuilder();
            var SortedList = Data;
            if (this.GetType().GetGenericArguments()[0].Name == "BaseItemTypes")
            {
                SortedList = SortedList.OrderBy(x => (x as BaseItemTypes).InheritsFromStringData.ToString()).ToList();
            }
            foreach (var s in SortedList)
            {
                sb.Append(": ").Append(s.ToStringWiki()).Append("\n");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}