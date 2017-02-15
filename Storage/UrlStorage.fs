namespace WimtShortener.Storage
open Microsoft.WindowsAzure.Storage.Table

module UrlStorage =        
    
    let getUrl client code:string = "http://testurl.com/api/abc"