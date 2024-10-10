using DigileraCommerceApp.Helpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DigileraCommerceApp.Test.IntegrationTests;

public abstract class BaseIntegrationTests
{
    protected static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonHelper.Deserialize<T>(content);
    }

    protected HttpRequestMessage CreateGetRequest(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri)
        {
        };

        return request;
    }

    protected HttpRequestMessage CreatePostRequest(string uri, object body)
    {
        string jsonData = JsonSerializer.Serialize(body);
        var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
        };

        return request;
    }

    protected async Task AssertResponseStatusAsync(HttpResponseMessage response, params HttpStatusCode[] expectedStatusCodes)
    {
        Assert.Contains(response.StatusCode, expectedStatusCodes);
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.NotNull(responseString);
    }
}