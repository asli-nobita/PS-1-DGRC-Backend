using DGRC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace DGRC.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            User registration = new User();
            string dateTime = DateTime.Now.ToString("d", new CultureInfo("en-US"));
            ViewData["TimeStamp"] = dateTime;
            return View(registration);
        }

        [HttpPost]
        public async Task<ActionResult> IndexAsync(User user)
        {
            string baseAddress = "http://10.133.20.158:8097";
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            using var client = new HttpClient(handler) { BaseAddress = new Uri(baseAddress) };
            var apiUrl = $"{baseAddress}/api/User/Registration";
            var jsonData = System.Text.Json.JsonSerializer.Serialize(user);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<Root>(jsonResponse);

            if (response.IsSuccessStatusCode && responseObj?.Status == true)
            {
                ViewBag.AlertType = "success";
                ViewBag.AlertTitle = "Success!";
                ViewBag.AlertMessage = responseObj.Message;
            }
            else
            {
                ViewBag.AlertType = "error";
                ViewBag.AlertTitle = "Error!";
                ViewBag.AlertMessage = responseObj?.Message;
            }

            return View();
        }
    }
}