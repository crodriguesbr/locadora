using Locadora.Web.Models;
using Locadora.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Locadora.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkNyaXN0aWFubyIsImVtYWlsIjoiY3Jpc3RpYW5vbnJAZ21haWwuY29tIiwicm9sZSI6IkFkbWluaXN0cmFkb3IiLCJuYmYiOjE2MjgxMTc2MjMsImV4cCI6MTYyODEyMTIyMywiaWF0IjoxNjI4MTE3NjIzfQ.Z4JZgG9jtwfKaNFvl1rOxDtWozwCe10oB6hZhjXgknk");

            var response = await httpClient.PostAsJsonAsync("http://localhost:5000/Cliente",
                new ClienteDto()
                {
                    Cpf = "12345678901",
                    Nome = "Cristiano Rodrigues",
                    DataNascimento = DateTime.Today
                });

            if (response.IsSuccessStatusCode)
            {
                //
            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
