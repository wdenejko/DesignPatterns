open System.Collections.Generic
open Microsoft.FSharp.Reflection
open System.IO
open System.Reflection
open System.Runtime.Serialization
open System.Runtime.Serialization.Formatters.Binary
open System.Text
open System.Xml
open System.Xml.Serialization

[<AbstractClass>]
type ColorPrototype() =
    abstract member Clone: unit -> ColorPrototype 
    abstract member DeepClone: unit -> ColorPrototype

type Color(red : int, green : int, blue : int) =
    inherit ColorPrototype()
    let binFormatter = new BinaryFormatter()
    override this.Clone() =
        printfn "Cloning color RGB: %i %i %i" red green blue
        this.MemberwiseClone() :?> ColorPrototype   
    override this.DeepClone() =
        printfn "Cloning color RGB: %i %i %i" red green blue
        use stream = new MemoryStream()
        binFormatter.Serialize(stream, this)
        let d = stream.Seek((int64)0, SeekOrigin.Begin)
        binFormatter.Deserialize(stream) :?> ColorPrototype

type ColorManager() =
    let colors = new Dictionary<string, Color>()
    member this.Item
        with get(x) = colors.[(x)]
        and set(x, y) value = colors.Add(x, y)
    member this.Add key value =
        colors.Add(key, value)
    member this.Clear =
        colors.Clear()

let colormanager = new ColorManager()
colormanager.Clear
colormanager.Add "red" (new Color(255, 0, 0))
colormanager.Add "green" (new Color(0, 255, 0))
colormanager.Add "blue" (new Color(0, 0, 255))

colormanager.Add "angry" (new Color(255, 54, 0))
colormanager.Add "peace" (new Color(128, 211, 128))
colormanager.Add "flame" (new Color(211, 34, 20))

let color1 = colormanager.["red"].Clone()
let color2 = colormanager.["peace"].Clone()
let color3 = colormanager.["flame"].Clone()
let deepCopy = DeepCopy colormanager
