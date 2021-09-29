﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UAssetAPI
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class FObjectExportFieldAttribute : Attribute
    {
        internal int DisplayingIndex = 0;
        internal FObjectExportFieldAttribute(int displayingIndex)
        {
            DisplayingIndex = displayingIndex;
        }
    }

    /// <summary>
    /// UObject resource type for objects that are contained within this package and can be referenced by other packages.
    /// </summary>
    public class Export : FObjectResource, ICloneable
    {
        ///<summary>Location of this export's class (import/other export). 0 = this export is a UClass</summary>
        [FObjectExportField(2)]
        public FPackageIndex ClassIndex;
        ///<summary>Location of this export's parent class (import/other export). 0 = this export is not derived from UStruct</summary>
        [FObjectExportField(3)]
        public FPackageIndex SuperIndex;
        ///<summary>Location of this export's template (import/other export). 0 = there is some problem</summary>
        [FObjectExportField(4)]
        public FPackageIndex TemplateIndex;
        ///<summary>The object flags for the UObject represented by this resource. Only flags that match the RF_Load combination mask will be loaded from disk and applied to the UObject.</summary>
        [FObjectExportField(5)]
        public EObjectFlags ObjectFlags;
        ///<summary>The number of bytes to serialize when saving/loading this export's UObject.</summary>
        [FObjectExportField(6)]
        public long SerialSize;
        ///<summary>The location (into the FLinker's underlying file reader archive) of the beginning of the data for this export's UObject. Used for verification only.</summary>
        [FObjectExportField(7)]
        public long SerialOffset;
        ///<summary>Was this export forced into the export table via OBJECTMARK_ForceTagExp?</summary>
        [FObjectExportField(8)]
        public bool bForcedExport;
        ///<summary>Should this export not be loaded on clients?</summary>
        [FObjectExportField(9)]
        public bool bNotForClient;
        ///<summary>Should this export not be loaded on servers?</summary>
        [FObjectExportField(10)]
        public bool bNotForServer;
        ///<summary>If this object is a top level package (which must have been forced into the export table via OBJECTMARK_ForceTagExp), this is the GUID for the original package file. Deprecated</summary>
        [FObjectExportField(11)]
        public Guid PackageGuid;
        ///<summary>If this export is a top-level package, this is the flags for the original package</summary>
        [FObjectExportField(12)]
        public EPackageFlags PackageFlags;
        ///<summary>Should this export be always loaded in editor game?</summary>
        [FObjectExportField(13)]
        public bool bNotAlwaysLoadedForEditorGame;
        ///<summary>Is this export an asset?</summary>
        [FObjectExportField(14)]
        public bool bIsAsset;

        /// <summary>
        /// The export table must serialize as a fixed size, this is used to index into a long list, which is later loaded into the array. -1 means dependencies are not present. These are contiguous blocks, so CreateBeforeSerializationDependencies starts at FirstExportDependency + SerializationBeforeSerializationDependencies.
        /// </summary>
        [FObjectExportField(15)]
        public int FirstExportDependency;
        /// <summary>
        /// The export table must serialize as a fixed size, this is used to index into a long list, which is later loaded into the array. -1 means dependencies are not present. These are contiguous blocks, so CreateBeforeSerializationDependencies starts at FirstExportDependency + SerializationBeforeSerializationDependencies.
        /// </summary>
        [FObjectExportField(16)]
        public int SerializationBeforeSerializationDependencies;
        /// <summary>
        /// The export table must serialize as a fixed size, this is used to index into a long list, which is later loaded into the array. -1 means dependencies are not present. These are contiguous blocks, so CreateBeforeSerializationDependencies starts at FirstExportDependency + SerializationBeforeSerializationDependencies.
        /// </summary>
        [FObjectExportField(17)]
        public int CreateBeforeSerializationDependencies;
        /// <summary>
        /// The export table must serialize as a fixed size, this is used to index into a long list, which is later loaded into the array. -1 means dependencies are not present. These are contiguous blocks, so CreateBeforeSerializationDependencies starts at FirstExportDependency + SerializationBeforeSerializationDependencies.
        /// </summary>
        [FObjectExportField(18)]
        public int SerializationBeforeCreateDependencies;
        /// <summary>
        /// The export table must serialize as a fixed size, this is used to index into a long list, which is later loaded into the array. -1 means dependencies are not present. These are contiguous blocks, so CreateBeforeSerializationDependencies starts at FirstExportDependency + SerializationBeforeSerializationDependencies.
        /// </summary>
        [FObjectExportField(19)]
        public int CreateBeforeCreateDependencies;
        
        /// <summary>
        /// Miscellaneous, unparsed export data, stored as a byte array.
        /// </summary>
        public byte[] Extras;

        /// <summary>
        /// The asset that this export is parsed with.
        /// </summary>
        public UAsset Asset;

        public Export(UAsset asset, byte[] extras)
        {
            Asset = asset;
            Extras = extras;
        }

        public Export()
        {

        }

        public virtual void Read(BinaryReader reader, int nextStarting = 0)
        {

        }

        public virtual void Write(BinaryWriter writer)
        {

        }

        private static FieldInfo[] _allFields = null;
        private static void InitAllFields()
        {
            if (_allFields != null) return;
            _allFields = typeof(Export).GetFields().Where(fld => fld.IsDefined(typeof(FObjectExportFieldAttribute), true)).OrderBy(fld => ((FObjectExportFieldAttribute[])fld.GetCustomAttributes(typeof(FObjectExportFieldAttribute), true))[0].DisplayingIndex).ToArray();
        }

        public static FieldInfo[] GetAllObjectExportFields()
        {
            InitAllFields();

            return _allFields;
        }

        public static string[] GetAllFieldNames()
        {
            InitAllFields();

            string[] allFieldNames = new string[_allFields.Length];
            for (int i = 0; i < _allFields.Length; i++)
            {
                allFieldNames[i] = _allFields[i].Name;
            }
            return allFieldNames;
        }

        public override string ToString()
        {
            InitAllFields();

            var sb = new StringBuilder();
            foreach (var info in _allFields)
            {
                var value = info.GetValue(this) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value.ToString());
            }
            return sb.ToString();
        }

        public object Clone()
        {
            var res = (Export)MemberwiseClone();
            res.Extras = (byte[])this.Extras.Clone();
            res.PackageGuid = new Guid(this.PackageGuid.ToByteArray());
            return res;
        }

        /// <summary>
        /// Creates a child export instance with the same export details as the current export.
        /// </summary>
        /// <typeparam name="T">The type of child export to create.</typeparam>
        /// <returns>An instance of the child export type provided with the export details copied over.</returns>
        public T ConvertToChildExport<T>() where T : Export, new()
        {
            InitAllFields();

            Export res = new T();
            res.Asset = this.Asset;
            res.Extras = this.Extras;
            res.ObjectName = this.ObjectName;
            res.OuterIndex = this.OuterIndex;
            foreach (var info in _allFields)
            {
                info.SetValue(res, info.GetValue(this));
            }
            return (T)res;
        }
    }
}