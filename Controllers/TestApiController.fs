namespace WimtShortener.Controllers

open Microsoft.AspNetCore.Mvc

type Result = {result: string;}

[<RouteAttribute("api/test")>] 
type public TestApiController () =
    inherit Controller()
    
    
    [<HttpGetAttribute("{url}")>]
    member public this.Get (url:string) =        
        this.Ok({ result=url })
        


