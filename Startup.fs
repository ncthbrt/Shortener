﻿namespace WimtShortener
open WimtShortener.Controllers.Settings
open WimtShortener.Storage
open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection 
open Microsoft.Extensions.Logging
open Microsoft.AspNetCore.Http

type Startup private () =

    new (env: IHostingEnvironment) as this =
        Startup() then

        let builder =
            ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional = false, reloadOnChange = true)
                .AddJsonFile((sprintf "appsettings.%s.json" (env.EnvironmentName)), optional = true)
                .AddEnvironmentVariables()

        this.Configuration <- builder.Build()



    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddMvc()  |> ignore
        services.AddCors() |> ignore

        services
            .Configure<ShortnerOptions>(this.Configuration.GetSection "Shortener")
            .Configure<TableStorageSettings>(this.Configuration.GetSection "Storage")
        |> ignore
        

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment, loggerFactory: ILoggerFactory) =

        loggerFactory.AddConsole(this.Configuration.GetSection("Logging")) |> ignore
        loggerFactory.AddDebug() |> ignore        

        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore                   

        app.UseCors(fun builder -> builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials()|> ignore) |> ignore
        app.Map(PathString("/create"), fun builder -> builder.UseDefaultFiles().UseStaticFiles()  |> ignore) |> ignore
        
        app.UseMvc() |> ignore
        

    member val Configuration : IConfigurationRoot = null with get, set
