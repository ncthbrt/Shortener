namespace WimtShortener.Util
open System
open System.Text
open System.Linq
open System.Security.Cryptography
open FSharp.Collections

module Hashing= 
    let private hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes("IT IS CHRISTMAS TIME AGAIN"))
    let CreateHash (url:string):string=                     
        let replace (substr: string) (replacement: string)= fun (str: string) -> str.Replace(substr,replacement)
        let replaceSymbols (x : string) = x |> replace "/" "S" |> replace "+" "P" |> replace "-" "M" |> replace "=" "E"
        let substr (length : int32) = fun (str:string) -> str.[0..length]

        url+DateTime.UtcNow.Ticks.ToString() 
        |> Encoding.UTF8.GetBytes 
        |> hmac.ComputeHash 
        |> Convert.ToBase64String
        |> substr(4)
        |> replaceSymbols
