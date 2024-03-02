using Microsoft.AspNetCore.Mvc;
using RestfulDEMO.UI.Models;
using RestfulDEMO.UI.Models.Dto;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace RestfulDEMO.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            try
            {
                // Get all regions from our API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessagee = await client.GetAsync("https://localhost:7008/api/Regions");
                httpResponseMessagee.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessagee.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

                //ViewBag.Repsonse = stringResponseBody;

            } catch (Exception ex)
            {
                // Log exception
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            // Returns the 'Add' view to add a new region
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionVewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7008/api/Regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                // Redirect to Index-Regions on success
                RedirectToAction("Index", "Regions");
            }

            return View();
        }
    }
}
