using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace LibDat
{
    /// <summary>
    /// Property or field represents an offset to a unicode string in the data section of the .dat file
    /// </summary>
    public class StringIndex : System.Attribute { }
    /// <summary>
    /// Property or field represents an offset to unknown data in the data section of the .dat file. These entries are not yet explored and are probably incorrect.
    /// </summary>
    public class DataIndex : System.Attribute { }
    /// <summary>
    /// Property or field represents an offset in the resources section of the .dat file
    /// </summary>
    public class ResourceOnly : System.Attribute { }
    /// <summary>
    /// Property or field is not shown on the table
    /// </summary>
    public class Hidden : System.Attribute { }
    /// <summary>
    /// Property or field is a reference to a key in an external dat
    /// </summary>
    public class ExternalReference : System.Attribute { }
    /// <summary>
    /// Property or field is a reference to a list of keys in an external dat
    /// </summary>
    public class ExternalReferenceList : System.Attribute { }

    public class UserStringIndex : StringIndex
    {
    }


    public abstract class BaseDat : IComparable
    {
        /// <summary>
        /// Identifier of the record.
        /// </summary>
        public UInt32 Key { get; set; }
        /// <summary>
        /// Save this record to the specified stream. Stream position is not preserved.
        /// </summary>
        /// <param name="outStream">Stream to write contents to</param>
        public abstract void Save(BinaryWriter outStream);
        /// <summary>
        /// Represents the number of bytes this record will read or write to the DAT file
        /// </summary>
        /// <returns>Number of bytes this record will take in its native format</returns>
        public abstract int GetSize();
        /// <summary>
        /// Reads the resources section of the file (after 0xBBbbBBbbBBbbBBbb)
        /// </summary>
        /// <returns></returns>
        public virtual void ReadResources(BinaryReader inStream, long dataTableBegin)
        {
            foreach (var propInfo in this.GetType().GetProperties())
            {
                if (propInfo.GetCustomAttributes(false).Any(n => n is ResourceOnly))
                {
                    if (propInfo.PropertyType == typeof(UnicodeString))
                    {
                        var propOffset = this.GetType().GetProperties().Where(x => x.Name == propInfo.Name.Replace("StringData", "StringOffset")).FirstOrDefault();
                        UnicodeString newVal = new UnicodeString(inStream, (int)(propOffset.GetValue(this, null)), dataTableBegin, false);
                        propInfo.SetValue(this, newVal, null);
                    }
                    else if (propInfo.PropertyType == typeof(UInt64List))
                    {
                        var propOffset = this.GetType().GetProperties().Where(x => x.Name == propInfo.Name.Replace("ListData", "ListOffset")).FirstOrDefault();
                        var propLength = this.GetType().GetProperties().Where(x => x.Name == propInfo.Name.Replace("ListData", "ListLength")).FirstOrDefault();
                        UInt64List newVal = new UInt64List(inStream, (int)(propOffset.GetValue(this, null)), dataTableBegin, (int)(propLength.GetValue(this, null)));
                        propInfo.SetValue(this, newVal, null);
                    }
                    else if (propInfo.PropertyType == typeof(UInt32List))
                    {
                        var propOffset = this.GetType().GetProperties().Where(x => x.Name == propInfo.Name.Replace("ListData", "ListOffset")).FirstOrDefault();
                        var propLength = this.GetType().GetProperties().Where(x => x.Name == propInfo.Name.Replace("ListData", "ListLength")).FirstOrDefault();
                        UInt32List newVal = new UInt32List(inStream, (int)(propOffset.GetValue(this, null)), dataTableBegin, (int)(propLength.GetValue(this, null)));
                        propInfo.SetValue(this, newVal, null);
                    }
                    else if (propInfo.PropertyType == typeof(Int32List))
                    {
                        var propOffset = this.GetType().GetProperties().Where(x => x.Name == propInfo.Name.Replace("ListData", "ListOffset")).FirstOrDefault();
                        var propLength = this.GetType().GetProperties().Where(x => x.Name == propInfo.Name.Replace("ListData", "ListLength")).FirstOrDefault();
                        Int32List newVal = new Int32List(inStream, (int)(propOffset.GetValue(this, null)), dataTableBegin, (int)(propLength.GetValue(this, null)));
                        propInfo.SetValue(this, newVal, null);
                    }
                    else if (propInfo.PropertyType == typeof(IndirectStringList))
                    {
                        var propOffset = this.GetType().GetProperties().Where(x => x.Name == propInfo.Name.Replace("ListData", "ListOffset")).FirstOrDefault();
                        var propLength = this.GetType().GetProperties().Where(x => x.Name == propInfo.Name.Replace("ListData", "ListLength")).FirstOrDefault();
                        IndirectStringList newVal = new IndirectStringList(inStream, (int)(propOffset.GetValue(this, null)), dataTableBegin, (int)(propLength.GetValue(this, null)));
                        propInfo.SetValue(this, newVal, null);
                    }
                }
            }
        }
        /// <summary>
        /// Resolve external references in the record
        /// </summary>
        public virtual void ResolveReferences()
        {
            foreach (var propInfo in this.GetType().GetProperties())
            {
                if (propInfo.GetCustomAttributes(false).Any(n => n is ExternalReference))
                {
                    var foreignKey = this.GetType().GetProperties().Where(x => x.Name == propInfo.Name.Replace("Ref", "Key")).FirstOrDefault();
                    Int64 fkValue;
                    if (foreignKey.GetValue(this, null) is Int64)
                        fkValue = (Int64)(foreignKey.GetValue(this, null));
                    else
                        fkValue = (Int32)(foreignKey.GetValue(this, null));
                    BaseDat newVal = ReferenceManager.Instance.AllDats[propInfo.PropertyType.Name + ".dat"].Where(x => x.Key == fkValue).FirstOrDefault();
                    propInfo.SetValue(this, newVal, null);
                }
                if (propInfo.GetCustomAttributes(false).Any(n => n is ExternalReferenceList))
                {
                    var foreignKey = this.GetType().GetProperties().Where(x => x.Name == propInfo.Name.Replace("ListRef", "ListData")).FirstOrDefault();
                    object untypedFkValues = foreignKey.GetValue(this, null);
                    if (untypedFkValues is UInt64List)
                    {
                        UInt64List fkValues = (untypedFkValues as UInt64List);
                        List<BaseDat> newVals = new List<BaseDat>();
                        Type genericDatType = propInfo.PropertyType.GetGenericArguments()[0];
                        var newExternalDatList = propInfo.PropertyType.GetConstructor(new Type[0]).Invoke(new Object[0]);
                        foreach (UInt64 fkValue in fkValues.Data)
                        {
                            BaseDat newVal = ReferenceManager.Instance.AllDats[genericDatType.Name + ".dat"].Where(x => x.Key == fkValue).FirstOrDefault();
                            newVals.Add(newVal);
                        }
                        newExternalDatList.GetType().GetProperty("Data").SetValue(newExternalDatList, newVals, null);
                        propInfo.SetValue(this, newExternalDatList, null);
                    }
                    else if (untypedFkValues is UInt32List)
                    {
                        UInt32List fkValues = (untypedFkValues as UInt32List);
                        List<BaseDat> newVals = new List<BaseDat>();
                        Type genericDatType = propInfo.PropertyType.GetGenericArguments()[0];
                        var newExternalDatList = propInfo.PropertyType.GetConstructor(new Type[0]).Invoke(new Object[0]);
                        foreach (UInt32 fkValue in fkValues.Data)
                        {
                            BaseDat newVal = ReferenceManager.Instance.AllDats[genericDatType.Name + ".dat"].Where(x => x.Key == fkValue).FirstOrDefault();
                            newVals.Add(newVal);
                        }
                        newExternalDatList.GetType().GetProperty("Data").SetValue(newExternalDatList, newVals, null);
                        propInfo.SetValue(this, newExternalDatList, null);
                    }
                    else
                    {
                    }
                }
            }
        }


        public virtual int CompareTo(object obj)
        {
            BaseDat other = (BaseDat)obj;
            return this.ToString().CompareTo(other.ToString());
        }

        public virtual string ToStringWiki()
        {
            return ToString();
        }

        public virtual string ToStringNoKey()
        {
            return ToString();
        }
    }
}