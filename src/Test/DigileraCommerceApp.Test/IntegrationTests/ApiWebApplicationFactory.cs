﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using DigileraCommerceApp.WebApi;

namespace DigileraCommerceApp.Test.IntegrationTests;

public class ApiWebApplicationFactory : WebApplicationFactory<WebApi.Program>
{
    public IConfiguration? Configuration { get; private set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("integrationsettings.json")
                .Build();

            config.AddConfiguration(Configuration);
        });
    }
}
