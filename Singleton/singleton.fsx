type Product private () =
  let mutable state = 0 
  static let instance = Product()

  // Lazy<T>
  // let static Instance : Lazy<T> = ...
  static member Instance = instance
  
  member this.DoSomething() = 
    state <- state + 1
    printfn "Doing something for the %i time" state
    ()

Product.Instance.DoSomething()
Product.Instance.DoSomething()
Product.Instance.DoSomething()

// Singleton as a module instead of as a class
module Singleton =
  type Product internal () =
    let mutable state = 0

    member this.DoSomething() =
      state <- state + 1
      printfn "Doing something for the %i time" state

  let Instance = Product()