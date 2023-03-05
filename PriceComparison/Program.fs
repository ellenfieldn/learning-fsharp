﻿open FSharp.Data

type Store = { Name:string; Url:string; Selector:string }
type Offer = { Store:Store; Price:float }

let stores = [ 
    { 
        Name = "Amazon"; 
        Url = "https://www.amazon.com/insta360-Link-Noise-Canceling-Microphones-Specialized/dp/B0B826KS4B/ref=sr_1_2?crid=301JRVCED656J&keywords=insta360%2Blink&qid=1676588334&sprefix=insta360%2Blink%2Caps%2C87&sr=8-2&ufe=app_do%3Aamzn1.fos.ac2169a1-b668-44b9-8bd0-5ec63b24bcb5&th=1"; 
        Selector = "#corePrice_feature_div .a-offscreen" 
    }; 
    { 
        Name = "Newegg";
        Url = "https://www.newegg.com/p/1EF-00J0-00005?Item=1EF-00J0-00005&nm_mc=AFC-RAN-COM&cm_mmc=afc-ran-com-_-Honey+%28new%29&utm_medium=affiliate&utm_campaign=afc-ran-com-_-Honey+%28new%29&utm_source=afc-Honey+%28new%29&AFFID=3268168&AFFNAME=Honey+%28new%29&ACRID=1&ASUBID=8600657499830672294-a8794109276156331618&ASID=&ranMID=44583&ranEAID=3268168&ranSiteID=pfLaClXlf20-pJ6ix442W6AJOk8sV43eew";
        Selector = ".product-buy-box .price-current" 
    };
    { 
        Name = "Sweetwater";
        Url = "https://www.sweetwater.com/c1003--Video_Cameras?highlight=LinkWebcam&mrkgadid=&mrkgcl=28&mrkgen=gpla&mrkgbflag=1&mrkgcat=drums&percussion&acctid=21700000001645388&dskeywordid=92700073370090877&lid=92700073370090877&ds_s_kwgid=58700007963105270&ds_s_inventory_feed_id=97700000007215323&dsproductgroupid=1795338701694&product_id=LinkWebcam&prodctry=US&prodlang=en&channel=online&storeid=&device=c&network=u&matchtype=&adpos=largenumber&locationid=9009746&creative=615094225367&targetid=pla-1795338701694&campaignid=17962511191&awsearchcpc=&&&gclid=Cj0KCQiAxbefBhDfARIsAL4XLRqYvaGnLPFY5bhsXDvqT6h20WAIBxJjXNyEsDsTy9Cl3rkqInAUASoaApUhEALw_wcB&gclsrc=aw.ds"
        Selector = ".product-price--final price" 
    }; ]
    
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
    let offers = 
        stores
        |> List.map (fun store -> { Store = store; Price=GetPrice store })
        |> List.sortBy (fun offer -> offer.Price)

    List.iter (fun offer -> printfn $"{offer.Store.Name} Price: {offer.Price}") offers
    0 // return an integer exit code