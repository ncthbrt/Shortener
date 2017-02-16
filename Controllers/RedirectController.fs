namespace WimtShortener.Controllers.Redirects
open WimtShortener.Controllers.Settings
open WimtShortener.Storage
open Microsoft.AspNetCore.Mvc
open FSharp.Collections
open Microsoft.Extensions.Options
           

[<RouteAttribute("")>] 
type public RedirectController (options:IOptions<ShortnerOptions>, tableOptions: IOptions<TableStorageSettings>) =
    inherit Controller()   

    [<HttpGetAttribute("{code}")>]
    member public this.Get ([<FromRouteAttribute>]code:string) : IActionResult =                
        let url = UrlStorage.GetUrl tableOptions.Value code
        match url with
        | Some urlStr -> printfn "output url: %s" urlStr;  this.Redirect(urlStr) :> IActionResult 
        | None -> this.NotFound() :> IActionResult 
        
    [<HttpGetAttribute>]
    member public this.Get () = this.Redirect(options.Value.DefaultUrl)

        
    
    


        


        


