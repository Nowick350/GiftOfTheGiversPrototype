using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace GiftOfTheGiversAzureFunction
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("GenerateTaxCertificate")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", "get")]
            HttpRequestData req)
        {
            _logger.LogInformation("Donation tax certificate function was triggered.");

            string donorName = "Test Donor";
            decimal amount = 0;

            if (req.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                    if (!string.IsNullOrWhiteSpace(requestBody))
                    {
                        var donation = JsonSerializer.Deserialize<DonationRequest>(
                            requestBody,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });

                        if (donation != null)
                        {
                            donorName = donation.DonorName ?? "Test Donor";
                            amount = donation.Amount;
                        }
                    }
                }
                catch
                {
                    _logger.LogWarning("Could not read donation information. Using dummy values.");
                }
            }

            string certificateNumber =
                "DUMMY-" + DateTime.Now.ToString("yyyyMMddHHmmss");

            var response = req.CreateResponse(HttpStatusCode.OK);

            response.Headers.Add("Content-Type", "application/json");

            var certificate = new
            {
                message = "Dummy tax certificate generated successfully.",
                donorName = donorName,
                donationAmount = amount,
                certificateNumber = certificateNumber,
                date = DateTime.Now.ToString("yyyy-MM-dd"),
                organisation = "Gift of the Givers Foundation"
            };

            await response.WriteStringAsync(
                JsonSerializer.Serialize(certificate));

            return response;
        }
    }

    public class DonationRequest
    {
        public string? DonorName { get; set; }
        public decimal Amount { get; set; }
    }
}