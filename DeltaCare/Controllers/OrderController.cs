using DeltaCare.BAL;
using DeltaCare.BAL.Barcode;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class OrderController : DeltaBaseController
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        #region Get Order Transaction

        [HttpGet("GetAllAccessionPrefixes")]
        public async Task<IActionResult> GetAllAccessionPrefixes()
        {
            var GetAccessionPrefixes = await _orderRepository.GetAllAccessionPrefixes();
            if (GetAccessionPrefixes == null)
                return NotFound(new { Message = "Accession Prefixes not found!" });
            return Ok(new
            {
                GetAccessionPrefixes
            });
        }
        [HttpGet("GenerateAccessionNumber/{siteNo}/{prfx}")]
        public string GenerateAccessionNumber(string siteNo, string prfx)
        {
            var accno = _orderRepository.GenerateAccessionNumber(siteNo, prfx);
            return $"{accno}";
        }
        [HttpGet("GetLastOrdersTransactions")]//GET_NEW_ORD_NO
        public async Task<IActionResult> GetLastOrdersTransactions()
        {
            var GET_ORD_TRNS = _orderRepository.GetLastOrdersTransactions().Result.FirstOrDefault();
            if (GET_ORD_TRNS == null)
                return NotFound(new { Message = "Order not found!" });
            return Ok(new
            {
                GET_ORD_TRNS
            });
        }
        [HttpGet("GetOrdersTransactionsByParams/{PAT_ID}")]//GET_ORD_TRNS (Old)
        public async Task<IActionResult> GetOrdersTransactionsByParams(string PAT_ID, [FromBody] ORD_TRNSModel oRD_TRNSModel)// string ORD_NO)
        {
            var GET_ORD_TRNS = await _orderRepository.GetOrdersTransactionsByParams(PAT_ID, oRD_TRNSModel);
            if (GET_ORD_TRNS == null)
                return NotFound(new { Message = "Order not found!" });
            return Ok(new
            {
                GET_ORD_TRNS
            });
        }

        [HttpGet("GetAllOrdersTransactions")]//GET_ORD_TRNS
        public async Task<IActionResult> GetAllOrdersTransactions()// string ORD_NO)
        {
            var GET_ORD_TRNS = await _orderRepository.GetAllOrdersTransactions();
            if (GET_ORD_TRNS == null)
                return NotFound(new { Message = "Order not found!" });
            return Ok(new
            {
                GET_ORD_TRNS
            });
        }

        [HttpGet("GetOrdersTransactionsDetailsByParams/{PAT_ID}/{ORD_NO}")]//GET_v_ORD_TRANS
       
        public async Task<IActionResult> GetOrdersTransactionsDetailsByParams(string PAT_ID, string ORD_NO)
        {
            var GET_v_ORD_TRANS = _orderRepository.GetOrdersTransactionsDetailsByParams(PAT_ID, ORD_NO).Result.FirstOrDefault();
            //if (GET_v_ORD_TRANS == null)
            //    return NotFound(new { Message = "Order not found!" });
            return Ok(new
            {
                GET_v_ORD_TRANS
            });
        }
        [HttpGet("GetActiveTestsRequestByParams/{PAT_ID}/{ORD_NO}")]//GET_ATR
        public async Task<IActionResult> GetActiveTestsRequestByParams(string PAT_ID, string ORD_NO)
        {
            var GET_ATR = await _orderRepository.GetActiveTestsRequestByParams(PAT_ID, ORD_NO);
            //if (GET_ATR == null)
            //    return NotFound(new { Message = "Order atr not found!" });
            return Ok(new
            {
                GET_ATR
            });
        }
        [HttpGet("GetOrdersDetailsByParams/{PAT_ID}/{ORD_NO}")]
        public async Task<IActionResult> GetOrdersDetailsByParams(string PAT_ID, string ORD_NO)
        {
            var Ord_Dtl = await _orderRepository.GetOrdersDetailsByParams(PAT_ID, ORD_NO);
            if (Ord_Dtl == null)
                return NotFound(new { Message = "Order atr not found!" });
            return Ok(new
            {
                Ord_Dtl
            });
        }
        [HttpGet("GetOrdersDetailsByUnion/{PAT_ID}/{ORD_NO}")]
        public async Task<IActionResult> GetOrdersDetailsByUnion(string PAT_ID, string ORD_NO)
        {
            var Ord_Dtl = await _orderRepository.GetOrdersDetailsByUnion(PAT_ID, ORD_NO);
            //if (Ord_Dtl == null)
             //   return NotFound(new { Message = "Order atr not found!" });
            return Ok(new
            {
                Ord_Dtl
            });
        }
        [HttpGet("GetAllOrderType")]
        public async Task<IActionResult> GetAllOrderType()//GET_Ord_TP
        {
            var GET_Ord_TP = await _orderRepository.GetAllOrderType();
            if (GET_Ord_TP == null)
                return NotFound(new { Message = "Order not found!" });
            return Ok(new
            {
                GET_Ord_TP
            });
        }

        [HttpGet("GetOrderTrackingByOrdNo/{ORD_NO}/{REQ_CODE}")]
        public async Task<IActionResult> GetOrderTrackingByOrdNo(string ORD_NO, string REQ_CODE)//GET_Ord_TP
        {
            var GET_OrdTrc = _orderRepository.GetOrderTrackingByOrdNo(ORD_NO).Result.Where(x => x.REQ_CODE == REQ_CODE).ToList();
            if (GET_OrdTrc == null)
                return NotFound(new { Message = "Order not found!" });
            return Ok(new
            {
                GET_OrdTrc
            });
        }

        [HttpGet("GetOrderTrackingByReqCode/{REQ_CODE}")]
        public async Task<IActionResult> GetOrderTrackingByReqCode(string REQ_CODE)//GET_Ord_TP
        {
            var GET_OrdTrc = await _orderRepository.GetOrderTrackingByReqCode(REQ_CODE);
            if (GET_OrdTrc == null)
                return NotFound(new { Message = "Order not found!" });
            return Ok(new
            {
                GET_OrdTrc
            });
        }

        #endregion

        #region Order Entry Transactions
        [HttpPost("InsertOrdersTransactions")]
        public async Task<ActionResult<int>> InsertOrdersTransactions([FromBody] ORD_TRNSModel oRD_TRNSModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _orderRepository.InsertOrdersTransactions(oRD_TRNSModel));
        }
        [HttpPost("InsertActiveTestsRequest/{SEX}/{CN}/{DOB}/{DRNO}/{LOC}/{PRFX}/{CLN_IND}/{ORD_NO}")]////ADD_ORD_TRNS
        public async Task<ActionResult<int>> InsertActiveTestsRequest([FromBody] Object[] ATRs, string SEX, string CN, DateTime DOB, string DRNO, string LOC, string PRFX, string CLN_IND, string ORD_NO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _orderRepository.InsertActiveTestsRequest(ATRs, SEX, CN, DOB, DRNO, LOC, PRFX, CLN_IND, ORD_NO));
        }
        #endregion

        #region Update Order Entry
        [HttpPut("UpdateOrdersTransactionsDetails")]
        public async Task<ActionResult<int>> UpdateOrdersTransactionsDetails([FromBody] ORD_TRNSModel oRD_TRNSModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _orderRepository.UpdateOrdersTransactionsDetails(oRD_TRNSModel));
        }
        #endregion

        #region Orders Miscellaneuos
        [HttpPut("CancelActiveTestRequest/{ATRID}/{R_STS}/{CNLD}/{Notes}/{SITE_NO}/{U_ID}/{ORD_NO}/{SECT}/{REQ_CODE}")]
        public async Task<ActionResult<int>> CancelActiveTestRequest(int ATRID, string R_STS, string CNLD, string Notes, string SITE_NO, string U_ID, string ORD_NO, string SECT, string REQ_CODE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _orderRepository.CancelActiveTestRequest(ATRID, R_STS, CNLD, Notes, SITE_NO, U_ID, ORD_NO, SECT, REQ_CODE));
        }
        [HttpPut("CollectedActiveTestRequest/{ATR_ID}/{STS}/{DRAWN_DTTM}/{ACCN}/{REQ_CODE}/{ORD_NO}/{SECT}/{REF_LAB}")]
        public async Task<ActionResult<int>> CollectedActiveTestRequest([FromBody] Object[] ATRs, int ATR_ID, string STS, DateTime DRAWN_DTTM, string ACCN, string REQ_CODE, string ORD_NO, string SECT, string REF_LAB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _orderRepository.CollectedActiveTestRequest(ATRs));//, ATR_ID, STS, DRAWN_DTTM, ACCN, REQ_CODE, ORD_NO, SECT, REF_LAB));
        }
        [HttpPut("AddNotesActiveTestsRequest")]
        public async Task<ActionResult<int>> AddNotesActiveTestsRequest([FromBody] ATRModel aTRModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _orderRepository.AddNotesActiveTestsRequest(aTRModel));
        }

        [HttpPut("UpdateIssueInvoince")]
        public async Task<ActionResult<int>> UpdateIssueInvoince([FromBody] ORD_TRNSModel oRD_TRNSModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _orderRepository.UpdateIssueInvoince(oRD_TRNSModel));
        }

        //Multiple Search Methods

        [HttpGet("GetMultipleSearch/{IDNT}/{TEL}/{PAT_ID}/{PAT_NAME}/{DOB}/{SEX}/{ORD_NO}/{ACCN}/{MDL}/{FromDate}/{ToDate}")]
        public async Task<IActionResult> GetMultipleSearch(string IDNT, string TEL, string PAT_ID, string PAT_NAME, DateTime? DOB, string SEX, string ORD_NO, string ACCN,string MDL,DateTime? FromDate,DateTime? ToDate)
        {
            var GetMultipleSearch = await _orderRepository.GetMultipleSearch(IDNT, TEL, PAT_ID, PAT_NAME, DOB, SEX, ORD_NO, ACCN, MDL,FromDate,ToDate);
            if (GetMultipleSearch == null)
                return NotFound(new { Message = "Patient not found!" });
            return Ok(new
            {
                GetMultipleSearch
            });
        }

        [HttpGet("GetMultipleSearchOrders/{PAT_ID}")]
        public async Task<IActionResult> GetMultipleSearchOrders(string PAT_ID)
        {
            var GetMultipleSearch = await _orderRepository.GetMultipleSearchOrders(PAT_ID);
            if (GetMultipleSearch == null)
                return NotFound(new { Message = "Patient not found!" });
            return Ok(new
            {
                GetMultipleSearch
            });
        }


        #endregion

        #region Environmental Order Entry

        [HttpPost("InsertEnvironmentalOrderATR")]
        public async Task<ActionResult<EVOrderATRModel>> InsertEnvironmentalOrderATR(List<EVOrderATRModel> evATRModel)
        {
            try
            {
                var result = await _orderRepository.InsertEVATR(evATRModel);
                return await Task.FromResult<ActionResult<EVOrderATRModel>>(Ok(result));

            }
            catch (Exception ex)
            {
                return await Task.FromResult<ActionResult<EVOrderATRModel>>(StatusCode(500, "Internal Server Error! Please Contact Admin! " + ex.Message));
            }
        }

        [HttpGet("GetAllEVTD/{TCode}")]
        public async Task<ActionResult> GetAllEVTD(string TCode)
        {
            return Ok(await _orderRepository.GetAllEVTD(TCode));
        }



        [HttpPost("InsertEVOrder")]
        public async Task<ActionResult<string>> InsertEVOrder([FromBody] EVOrderModel evOrderModel)
        {
            return Ok(await _orderRepository.InsertEVOrder(evOrderModel));
        }


        [HttpGet("GetAllEVSample")]
        public async Task<ActionResult> GetAllEVSample()
        {
            return Ok(await _orderRepository.GetAllEVSample());
        }


        [HttpGet("GetPatientId")]
        public async Task<ActionResult> GetPatientId()
        {
            return Ok(await _orderRepository.GetPatientNextId());
        }


        [HttpGet("GetAllClients")]
        public async Task<ActionResult> GetAllClients()
        {
            return Ok(await _orderRepository.GetAllClients());
        }

        [HttpPost("GetForEnvironmentalOrderATR")]
        public async Task<ActionResult> GetForEnvironmentalOrderATR([FromBody] EVOrderATRModel evOrder)
        {
            return Ok(await _orderRepository.GetForEnvironmentalOrderATRData(evOrder));
        }


        [HttpGet("GetAllEnvironmentalOrder/{pSize}")]
        public async Task<ActionResult> GetAllEnvironmentalOrder(string pSize)
        {
            return Ok(await _orderRepository.GetAllEnvironmentalOrder(pSize));
        }

        [HttpGet("GetEnvironmentalOrderList/{PendingDays}/{isPending}")]
        public async Task<ActionResult> GetEnvironmentalOrderList(int PendingDays,bool isPending)
        {
            return Ok(await _orderRepository.GetEnvironmentalOrderList(PendingDays, isPending));
        }

        [HttpPost("GetEVOrderPatientDetails")]
        public async Task<ActionResult<EVDetailModel>> GetEVOrderPatientDetails([FromBody] EvResultDetailModel evResultModel)
        {
            return Ok(await _orderRepository.GetEVOrderPatientDetails(evResultModel));
        }

        [HttpGet("GetAllEVPatientSearch/{OrderNo}")]
        public async Task<ActionResult> GetAllEVPatientSearch(string OrderNo)
        {
            return Ok(await _orderRepository.GetAllEVPatientSearch(OrderNo));
        }

        [HttpPost("GetEVPrint")]
        public IActionResult GetEVPrint([FromBody] EvListPrintModel evPrintModel)
        {
            string b64QRCode = _orderRepository.GetEVPrint(evPrintModel.OrderNo);
            var _cls = new Response();
            _cls.messages = b64QRCode;
            _cls.responsecode = 200;
            return Ok(JsonConvert.SerializeObject(_cls));   
        }

        [HttpPost("UpdateInvoice")]
        public async Task<ActionResult> UpdateInvoice([FromBody] EvListPrintModel evPrintModel)
        {
            return Ok(await _orderRepository.UpdateInvoiceNo(evPrintModel.OrderNo));
        }

        [HttpPost("GetEVMultiInvoicePrint")]
        public IActionResult GetEVMultiInvoicePrint([FromBody] EvListMultiInvoicePrintModel evPrintModel)
        {
            string b64QRCode = _orderRepository.GetEVMultiInvoicePrint(evPrintModel);
            var _cls = new Response();
            _cls.messages = b64QRCode;
            _cls.responsecode = 200;
            return Ok(JsonConvert.SerializeObject(_cls));
        }

        [HttpPost("GetAllMultiInvoice")]
        public async Task<ActionResult> GetAllMultiInvoice([FromBody] EvListMultiInvoicePrintModel evPrintModel)
        {
            return Ok(await _orderRepository.GetEVMultiInvoice(evPrintModel));
        }


        [HttpGet("GetSignedUser")]
        public async Task<ActionResult> GetSignedUser()
        {
            return Ok(await _orderRepository.GetSignUser());
        }

        [HttpGet("GetEVOTP")]
        public async Task<ActionResult> GetEVOTP()
        {
            return Ok(await _orderRepository.GetEVOTP());
        }

        #endregion

        #region Cytogenetic Order


        [HttpGet("GetAllCytogeneticOrder")]
        public async Task<ActionResult> GetAllCytogeneticOrder()
        {
            return Ok(await _orderRepository.GetAllCytogeneticOrder());
        }

        [HttpPost("GetForCytogeneticOrderATR")]
        public async Task<ActionResult> GetForCytogeneticOrderATR([FromBody] CGOrderATRModel cgOrder)
        {
            return Ok(await _orderRepository.GetForCytogeneticOrderATRData(cgOrder));
        }


        [HttpPost("InsertCGOrder")]
        public async Task<ActionResult<int>> InsertCGOrder([FromBody] CGOrderModel cgOrderModel)
        {
            return Ok(await _orderRepository.InsertCGOrder(cgOrderModel));
        }

        [HttpPost("InsertCGOrderATR")]
        public async Task<ActionResult<CGOrderATRModel>> InsertCGOrderATR(List<CGOrderATRModel> cgATRModel)
        {
            try
            {
                var result = await _orderRepository.InsertCGATR(cgATRModel);
                return await Task.FromResult<ActionResult<CGOrderATRModel>>(Ok(result));

            }
            catch (Exception ex)
            {
                return await Task.FromResult<ActionResult<CGOrderATRModel>>(StatusCode(500, "Internal Server Error! Please Contact Admin! " + ex.Message));
            }
        }


        [HttpGet("GetAllCGTD/{TCode}")]
        public async Task<ActionResult> GetAllCGTD(string TCode)
        {
            return Ok(await _orderRepository.GetAllCGTD(TCode));
        }



        #endregion

    }
}
