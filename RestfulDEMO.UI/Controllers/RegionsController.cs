using Microsoft.AspNetCore.Mvc;
using RestfulDEMO.UI.Models;
using RestfulDEMO.UI.Models.Dto;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace RestfulDEMO.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly string baseServer;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            this.baseServer = "https://localhost:7008";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            try
            {
                // Get all regions from our API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessagee = await client.GetAsync($"{this.baseServer}/api/Regions");
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
                RequestUri = new Uri($"{this.baseServer}/api/Regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                // Redirect to Index-Regions on success
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Returns the 'Edit' view to edit the selected region

            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDto>($"{this.baseServer}/api/Regions/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{this.baseServer}/api/Regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                // Redirect to Edit-Regions on success
                return RedirectToAction("Index", "Regions");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"{this.baseServer}/api/Regions/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                // Redirect to Index-Regions on success
                return RedirectToAction("Index", "Regions");
            } 
            catch (Exception ex)
            {
                // Log exception
            }
            return View();
        }
    }
}
