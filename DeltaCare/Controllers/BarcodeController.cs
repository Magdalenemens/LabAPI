using DeltaCare.BAL;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Immutable;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeController : DeltaBaseController
    {
        private readonly IBarcodeRepository _barcodeRepository;
        public BarcodeController(
            IBarcodeRepository barcodeRepository)
        {
            _barcodeRepository = barcodeRepository;
        }

        [HttpGet("GenerateBarcode/{ORD_NO}")]//GET_BARCODE
        public async Task<IActionResult> GenerateBarcode(string ORD_NO)
        {
            if (string.IsNullOrEmpty(ORD_NO))
                return BadRequest();
            var GET_BARCODE = await _barcodeRepository.GenerateBarcode(ORD_NO);
            if (GET_BARCODE == null)
                return NotFound(new { Message = "Barcode not found!" });
            return Ok(new
            {
                GET_BARCODE
            });
        }

        [HttpGet("GetBarcode/{accn}")]
        public IActionResult Get(string accn)
        {
            string b64BCode = _barcodeRepository.GetBarCode(accn);
            var _cls = new Response();
            _cls.messages = b64BCode;
            _cls.responsecode = 200;
            return Ok(JsonConvert.SerializeObject(_cls));
        }


        [HttpPost("GetQRcode")]
        public IActionResult GetQR([FromBody] QRListQRModel qrListQRSearch)
        {
            string data = qrListQRSearch.accn.Replace("-", "");
            string b64QRCode = _barcodeRepository.GenerateQR(data);
            var _cls = new Response();
            _cls.messages = b64QRCode;
            _cls.responsecode = 200;
            return Ok(JsonConvert.SerializeObject(_cls));
        }

        [HttpPost("GetCodePDF")]
        public IActionResult GetCodePDF([FromBody] QRListQRModel qrListQRSearch)
        {
            string data = qrListQRSearch.accn.Replace("-", "");
            string b64QRCode = _barcodeRepository.GetCodePDF(data);
            var _cls = new Response();
            _cls.messages = b64QRCode;
            _cls.responsecode = 200;
            return Ok(JsonConvert.SerializeObject(_cls));
        }

    }
}
