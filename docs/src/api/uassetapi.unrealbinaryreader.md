# UnrealBinaryReader

Namespace: UAssetAPI

Any binary reader used in the parsing of Unreal file types.

```csharp
public class UnrealBinaryReader : System.IO.BinaryReader, System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BinaryReader](https://docs.microsoft.com/en-us/dotnet/api/system.io.binaryreader) → [UnrealBinaryReader](./uassetapi.unrealbinaryreader.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Properties

### **BaseStream**

```csharp
public Stream BaseStream { get; }
```

#### Property Value

[Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

## Constructors

### **UnrealBinaryReader(Stream)**

```csharp
public UnrealBinaryReader(Stream stream)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

## Methods

### **ReverseIfBigEndian(Byte[])**

```csharp
protected Byte[] ReverseIfBigEndian(Byte[] data)
```

#### Parameters

`data` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **ReadInt16()**

```csharp
public short ReadInt16()
```

#### Returns

[Int16](https://docs.microsoft.com/en-us/dotnet/api/system.int16)<br>

### **ReadUInt16()**

```csharp
public ushort ReadUInt16()
```

#### Returns

[UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

### **ReadInt32()**

```csharp
public int ReadInt32()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ReadUInt32()**

```csharp
public uint ReadUInt32()
```

#### Returns

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **ReadInt64()**

```csharp
public long ReadInt64()
```

#### Returns

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **ReadUInt64()**

```csharp
public ulong ReadUInt64()
```

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **ReadSingle()**

```csharp
public float ReadSingle()
```

#### Returns

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **ReadDouble()**

```csharp
public double ReadDouble()
```

#### Returns

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **ReadString()**

```csharp
public string ReadString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ReadFString(FSerializedNameHeader)**

```csharp
public FString ReadFString(FSerializedNameHeader nameHeader)
```

#### Parameters

`nameHeader` [FSerializedNameHeader](./uassetapi.io.fserializednameheader.md)<br>

#### Returns

[FString](./uassetapi.unrealtypes.fstring.md)<br>

### **ReadNameMapString(FSerializedNameHeader, UInt32&)**

```csharp
public FString ReadNameMapString(FSerializedNameHeader nameHeader, UInt32& hashes)
```

#### Parameters

`nameHeader` [FSerializedNameHeader](./uassetapi.io.fserializednameheader.md)<br>

`hashes` [UInt32&](https://docs.microsoft.com/en-us/dotnet/api/system.uint32&)<br>

#### Returns

[FString](./uassetapi.unrealtypes.fstring.md)<br>

### **ReadNameBatch(Boolean, UInt64&, List`1&)**

```csharp
public void ReadNameBatch(bool VerifyHashes, UInt64& HashVersion, List`1& nameMap)
```

#### Parameters

`VerifyHashes` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`HashVersion` [UInt64&](https://docs.microsoft.com/en-us/dotnet/api/system.uint64&)<br>

`nameMap` [List`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1&)<br>

### **ReadCustomVersionContainer(ECustomVersionSerializationFormat, List&lt;CustomVersion&gt;, Usmap)**

```csharp
public List<CustomVersion> ReadCustomVersionContainer(ECustomVersionSerializationFormat format, List<CustomVersion> oldCustomVersionContainer, Usmap Mappings)
```

#### Parameters

`format` [ECustomVersionSerializationFormat](./uassetapi.unversioned.ecustomversionserializationformat.md)<br>

`oldCustomVersionContainer` [List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

`Mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>

#### Returns

[List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
