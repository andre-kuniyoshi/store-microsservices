﻿using AspNetCoreMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Client.AspNetCore;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace AspNetCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
            => _httpClientFactory = httpClientFactory;

        [HttpGet("~/")]
        public ActionResult Index() => View();

        [Authorize, HttpPost("~/")]
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            // For scenarios where the default authentication handler configured in the ASP.NET Core
            // authentication options shouldn't be used, a specific scheme can be specified here.
            var token = await HttpContext.GetTokenAsync(OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken);

            using var client = _httpClientFactory.CreateClient();

            using var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5001/Test/api");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var response = await client.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            return View(model: await response.Content.ReadAsStringAsync(cancellationToken));
        }
    }
}