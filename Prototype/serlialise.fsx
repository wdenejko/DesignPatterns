#r "System.Xml.dll"
#r "System.Runtime.Serialization.dll"

open Microsoft.FSharp.Reflection
open System.IO
open System.Reflection
open System.Runtime.Serialization
open System.Runtime.Serialization.Formatters.Binary
open System.Runtime.Serialization.Json
open System.Text
open System.Xml
open System.Xml.Serialization

let toString = System.Text.Encoding.ASCII.GetString
let toBytes (x : string) = System.Text.Encoding.ASCII.GetBytes x

// #region Json

let serializeJson<'a> (x : 'a) = 
    let jsonSerializer = new DataContractJsonSerializer(typedefof<'a>)

    use stream = new MemoryStream()
    jsonSerializer.WriteObject(stream, x)
    toString <| stream.ToArray()

let deserializeJson<'a> (json : string) =
    let jsonSerializer = new DataContractJsonSerializer(typedefof<'a>)

    use stream = new MemoryStream(toBytes json)
    jsonSerializer.ReadObject(stream) :?> 'a

// #endregion

// #region XML

let serializeXml<'a> (x : 'a) =
    let xmlSerializer = new DataContractSerializer(typedefof<'a>)

    use stream = new MemoryStream()
    xmlSerializer.WriteObject(stream, x)
    toString <| stream.ToArray()

let deserializeXml<'a> (xml : string) =
    let xmlSerializer = new DataContractSerializer(typedefof<'a>)

    use stream = new MemoryStream(toBytes xml)
    xmlSerializer.ReadObject(stream) :?> 'a

// #endregion

// #region Binary

let serializeBinary<'a> (x :'a) =
    let binFormatter = new BinaryFormatter()

    use stream = new MemoryStream()
    binFormatter.Serialize(stream, x)
    stream.ToArray()

let deserializeBinary<'a> (arr : byte[]) =
    let binFormatter = new BinaryFormatter()

    use stream = new MemoryStream(arr)
    binFormatter.Deserialize(stream) :?> 'a

// #endregion

[<DataContract>]
type Person =
    {
        [<field : DataMember>]
        Name    : string;

        [<field : DataMember>]
        Age     : int
    }

let person = { Name = "Yan Cui"; Age = 99 }

// json serialize a record type
let json = serializeJson person
let personClone = deserializeJson<Person> json

// xml serialize a record type
let xml = serializeXml person
let personClone2 = deserializeXml<Person> xml

// binary serialize a record type
let arr = serializeBinary person
let personClone3 = deserializeBinary<Person> arr

type SingleCaseDU = SingleCaseDU  of string

let du = SingleCaseDU("test")

// json serialize single-case DU
let duJson = serializeJson du
let duClone = deserializeJson<SingleCaseDU> duJson

// xml serialize single-case DU
let duXml = serializeXml du
let duClone2 = deserializeXml<SingleCaseDU> duXml

// binary serialize single-case DU
let duArr = serializeBinary du
let duClone3 = deserializeBinary<SingleCaseDU> duArr

[<KnownType("GetKnownTypes")>]
type MultiCaseDU =
    | Case1 of string
    | Case2
    static member GetKnownTypes() =
        typedefof<MultiCaseDU>.GetNestedTypes(BindingFlags.Public ||| BindingFlags.NonPublic) 
        |> Array.filter FSharpType.IsUnion

let case1 = Case1("test")
let case2 = Case2

// json serialize multi-case DU
let case1Json = serializeJson case1
let case2Json = serializeJson case2

let case1Clone = deserializeJson<MultiCaseDU> case1Json
let case2Clone = deserializeJson<MultiCaseDU> case2Json

// xml serialize multi-case DU
let case1xml = serializeXml case1
let case2xml = serializeXml case2

let case1Clone2 = deserializeXml<MultiCaseDU> case1xml
let case2Clone2 = deserializeXml<MultiCaseDU> case2xml

// binary serialize multi-case DU
let case1Arr = serializeBinary case1
let case2Arr = serializeBinary case2

let case1Clone3 = deserializeBinary<MultiCaseDU> case1Arr
let case2Clone3 = deserializeBinary<MultiCaseDU> case2Arr