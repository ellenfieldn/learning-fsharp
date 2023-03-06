open FSharp.Data
open FSharp.Json
open System.IO

type Store = { Name:string; Url:string; Selector:string }
type Offer = { Store:Store; Price:float }

let removeDollarSign (x: string) = x.Replace("$", "")

let GetPrice store = 
    let doc = HtmlDocument.Load(store.Url)
    let price = 
        doc.CssSelect(store.Selector) 
        |> List.head
        |> fun a -> a.InnerText()
        |> removeDollarSign
        |> float
    price

[<EntryPoint>]
let main argv =
    let fileContents = File.ReadAllText("input.json")
    let stores = Json.deserialize fileContents

    let offers = 
        stores
        |> List.map (fun store -> { Store = store; Price=GetPrice store })
        |> List.sortBy (fun offer -> offer.Price)

    List.iter (fun offer -> printfn $"{offer.Store.Name} Price: {offer.Price}") offers
    0 // return an integer exit code