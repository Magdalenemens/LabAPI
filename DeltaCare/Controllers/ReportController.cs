using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

[Route("api/reports")]
[ApiController]
public class ReportController : ControllerBase
{

    private readonly IConfiguration _configuration;
    public ReportController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

   // [HttpPost("reportserver")]
    //[AllowAnonymous]
    //public async Task<IActionResult> GetSSRSReport(string ssrsUrl)
    //{
    //    try
    //    {
    //        // Validate input
    //        if (string.IsNullOrWhiteSpace(ssrsUrl))
    //        {
    //            return BadRequest("Invalid SSRS URL.");
    //        }

    //        // Check if SSRS is running on localhost
    //        bool isLocalhost = ssrsUrl.Contains("localhost", StringComparison.OrdinalIgnoreCase) ||
    //                           ssrsUrl.Contains("127.0.0.1");

    //        HttpClientHandler handler = new HttpClientHandler();

    //        if (isLocalhost)
    //        {
    //            handler.UseDefaultCredentials = true; // Use Windows authentication for localhost
    //        }
    //        else
    //        {
    //            // Read SSRS credentials from appsettings.json
    //            string username = _configuration["SSRS:Username"];
    //            string password = _configuration["SSRS:Password"];
    //            string domain = _configuration["SSRS:Domain"];

    //            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(domain))
    //            {
    //                return StatusCode(500, "SSRS credentials are missing in the configuration.");
    //            }

    //            handler.Credentials = new NetworkCredential(username, password, domain);
    //            handler.PreAuthenticate = true; // Ensures NTLM authentication upfront
    //        }

    //        using (var client = new HttpClient(handler))
    //        {
    //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));

    //            // Call SSRS report URL
    //            HttpResponseMessage response = await client.GetAsync(ssrsUrl);

    //            if (!response.IsSuccessStatusCode)
    //            {
    //                return StatusCode((int)response.StatusCode, $"Failed to fetch SSRS report. Status Code: {response.StatusCode}");
    //            }

    //            // Read report data
    //            var reportData = await response.Content.ReadAsByteArrayAsync();
    //            return File(reportData, "application/pdf", "SSRS_Report.pdf");
    //        }
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        return StatusCode(500, $"HTTP request error: {ex.Message}");
    //    }
    //    catch (SecurityTokenMalformedException ex)
    //    {
    //        return StatusCode(500, $"Authentication token error: {ex.Message}");
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, $"Unexpected error: {ex.Message}");
    //    }
    //}    

    [HttpPost("reportserver")]
    public async Task<IActionResult> GetSSRSReport([FromBody] ReportRequest request)
    {
        try
        {
            if (request == null || string.IsNullOrWhiteSpace(request.SsrsUrl))
            {
                return BadRequest("Invalid SSRS URL.");
            }

            bool isLocalhost = request.SsrsUrl.Contains("localhost", StringComparison.OrdinalIgnoreCase) ||
                               request.SsrsUrl.Contains("127.0.0.1");

            HttpClientHandler handler = new HttpClientHandler();

            if (isLocalhost)
            {
                handler.UseDefaultCredentials = true;
            }
            else
            {
                string username = _configuration["SSRS:Username"];
                string password = _configuration["SSRS:Password"];
                string domain = _configuration["SSRS:Domain"];

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(domain))
                {
                    return StatusCode(500, "SSRS credentials are missing in the configuration.");
                }

                handler.Credentials = new NetworkCredential(username, password, domain);
                handler.PreAuthenticate = true;
            }

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));

                HttpResponseMessage response = await client.GetAsync(request.SsrsUrl);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, $"Failed to fetch SSRS report. Status Code: {response.StatusCode}");
                }

                var reportData = await response.Content.ReadAsByteArrayAsync();
                return File(reportData, "application/pdf", "SSRS_Report.pdf");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Unexpected error: {ex.Message}");
        }
    }

    // Create a model class for the request
    public class ReportRequest
    {
        public string SsrsUrl { get; set; }
    }


}

