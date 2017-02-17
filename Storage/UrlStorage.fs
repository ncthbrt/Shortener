namespace WimtShortener.Storage
open Microsoft.WindowsAzure.Storage.Table
open Microsoft.WindowsAzure.Storage
open System
open System.Data
open WimtShortener.Util.Hashing

type public TableStorageSettings () =             
    member val ConnectionString="" with get,set                
    member val Limit = 3 with get,set            

type UrlTableEntity (code,url) = 
    inherit TableEntity("urls",code)    
    new ()= UrlTableEntity(null,null)
    member val public Url = url with get,set
    member val public Code = code with get, set


module UrlStorage =            
    let private createCloudTableClient (storageAccount:CloudStorageAccount): CloudTableClient =  storageAccount.CreateCloudTableClient()
    let private getTableReference (tableClient:CloudTableClient):CloudTable =  tableClient.GetTableReference("urls")
    let private createTableIfNotExists (tableReference:CloudTable):CloudTable =
        tableReference.CreateIfNotExistsAsync().Result |> ignore
        tableReference
        
    let private buildGetQueryFromCode (code:string)= 
        TableOperation.Retrieve<UrlTableEntity>("urls",Uri.EscapeDataString(code))

    let private buildInsertQueryFromCode (url:string) (code:string) = 
        TableOperation.Insert(UrlTableEntity(url=url, code=code))
        
    let private getAndCreateTableIfNotExists (settings:TableStorageSettings) =     
        settings.ConnectionString
            |> CloudStorageAccount.Parse 
            |> createCloudTableClient
            |> getTableReference
            |> createTableIfNotExists            
       
    let private tryInsertIfNotExists (table: CloudTable) nextHash url: bool =         
        try
            let result = table.ExecuteAsync(buildInsertQueryFromCode url nextHash).Result
            result.HttpStatusCode/100 = 2                
        with 
            | _ -> false

    
    let rec private tryCreateAndStoreHash (table: CloudTable) (url:string) count limit: string Option=  
        if count > limit then 
            None
        else
            let nextHash = CreateHash url
            if (tryInsertIfNotExists table nextHash url) then
                nextHash |> Some
            else
                tryCreateAndStoreHash table url (count+1) limit
            
    let SetUrl (settings:TableStorageSettings) (url:string): string Option = 
        let table = settings |> getAndCreateTableIfNotExists
        tryCreateAndStoreHash table url 0 settings.Limit

    let SetUrlWithName (settings:TableStorageSettings) (url:string) (name:string): bool = 
        let table = settings |> getAndCreateTableIfNotExists
        (tryInsertIfNotExists table name url)
        

    let GetUrl (settings:TableStorageSettings) (code:string):string Option = 
        let table= settings |> getAndCreateTableIfNotExists
        try
            let result = table.ExecuteAsync(buildGetQueryFromCode code).Result
            printfn "The result is %d" result.HttpStatusCode |> ignore            
            if ((result.HttpStatusCode/100) = 2) then
                (result.Result :?> UrlTableEntity).Url |> Some
            else
                None
        with 
            | e -> printfn "Error ocurred: %s" (e.ToString()); None