namespace WimtShortener.Controllers
open WimtShortener.Storage
open Microsoft.AspNetCore.Mvc

type Result = {result: string;}

[<RouteAttribute("api/shortener")>] 
type public TestApiController () =
    inherit Controller()
    
    
    [<HttpGetAttribute("{url}")>]
    member public this.Get (code:string) =        
        this.Ok({ result= UrlStorage.getUrl "" code })
        


