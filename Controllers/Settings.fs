namespace WimtShortener.Controllers.Settings

type public ShortnerOptions () =             
    member val BaseUrl="" with get,set        
    member val DefaultUrl="" with get,set            
    member val AllowedUrls="*" with get,set