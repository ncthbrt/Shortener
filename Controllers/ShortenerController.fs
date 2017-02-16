namespace WimtShortener.Controllers.Shortner
open WimtShortener.Storage
open WimtShortener.Controllers.Settings
open Microsoft.AspNetCore.Mvc
open FSharp.Collections
open Microsoft.Extensions.Options
open System.Text.RegularExpressions


type ShortnerResult = { url: string; }
type ShortnerInput = { url: string; }


[<RouteAttribute("api/shortener")>] 
type public ShortnerController (options: IOptions<ShortnerOptions>, tableOptions: IOptions<TableStorageSettings>) =
    inherit Controller()   

    member private  this.IsMatch url =             
        Regex.IsMatch(url, options.Value.AllowedUrls)

    [<HttpGetAttribute("{code}")>]
    member public this.Get ([<FromRouteAttribute>]code:string) =                
        let url = UrlStorage.GetUrl tableOptions.Value code        
        match url with
        | Some urlStr -> this.Ok({ url=urlStr  }) :> IActionResult 
        | None -> this.NotFound() :> IActionResult 
        

    [<HttpPostAttribute>]
    member public this.Post ([<FromBodyAttribute>]input:ShortnerInput) =        
        if (this.IsMatch input.url) then 
            let code = UrlStorage.SetUrl tableOptions.Value input.url        
            match code with 
            |  Some codeVal -> this.Ok({ url= options.Value.BaseUrl+codeVal  }) :> IActionResult 
            // Return insuffient storage result as it seems likely we've run out of available hashes
            |  None -> this.StatusCode(507,"Error: Url Hash collided too many times. We should probably add an extra digit to the code") :> IActionResult
        else 
            this.BadRequest("Please only use a whitelisted url") :> IActionResult


    [<HttpPostAttribute("{name}")>]
    member public this.PostExplicit ([<FromBodyAttribute>]input:ShortnerInput) ([<FromRouteAttribute>] name:string) =        
        if (this.IsMatch input.url) then 
            if(UrlStorage.SetUrlWithName tableOptions.Value input.url name) then        
                this.Ok({ url= options.Value.BaseUrl+name  }) :> IActionResult 
            else 
                this.BadRequest("Name already has been assigned to another url") :> IActionResult
        else
            this.BadRequest("Please only use a whitelisted url") :> IActionResult

        
        
    
    


        


        


