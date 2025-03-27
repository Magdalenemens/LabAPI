using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DeltaCare.BAL
{
    public class CentralReceivingRepository : ICentralReceivingRepository
    {
        private readonly IDataRepository _dataRepository;
        private readonly IOrderRepository _orderRepository;
        public CentralReceivingRepository(IDataRepository dataRepository, IOrderRepository orderRepository)
        {
            _dataRepository = dataRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<ORD_DTLModel>> GetOrdersDetailsByAccn(string ACCN, string STS)
        {
            ORD_DTLModel oRD_DTLModel = new ORD_DTLModel();
            int queryType = (int)QueryTypeEnum.GetById;
            oRD_DTLModel.QueryType = queryType;
            oRD_DTLModel.ACCN = ACCN;
            oRD_DTLModel.STS = STS;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ORD_DTLModel>(oRD_DTLModel);
            return (await _dataRepository.ExecuteQueryAsync<ORD_DTLModel>(SPConstant.SP_ManageOrdersDetails, parameterCollection)).ToList();
        }
        public async Task<int> CentralReceivingOrders(Object[] ORDs, string ACCN)
        {

            BadRequestResult badRequest = new BadRequestResult();

            foreach (var objORDs in ORDs)
            {
                string jsonString = JsonSerializer.Serialize(objORDs);
                var atrJson = JsonObject.Parse(jsonString);
                // aTRModel.REQ_CODE = atrJson["reQ_CODE"].ToString();


                //return badRequest.StatusCode;
                //Udpate Order Details
                ORD_DTLModel oRD_DTLModel = new ORD_DTLModel();
                oRD_DTLModel.ACCN = ACCN;
                //oRD_DTLModel.ATRID = ATR_ID;
                oRD_DTLModel.REQ_CODE = atrJson["reQ_CODE"].ToString();
                oRD_DTLModel.RCVD_DTTM = DateTime.Now;// Convert.ToDateTime(atrJson["RCVD_DTTM"]);
                oRD_DTLModel.STS = "CR";
                oRD_DTLModel.R_STS = "O";
                //string STS = "CR";
                oRD_DTLModel.SECT = atrJson["sect"].ToString();
                oRD_DTLModel.ATRID = Convert.ToInt32(atrJson["atrid"].ToString());

                oRD_DTLModel.ORD_NO = atrJson["orD_NO"].ToString();
                int updatedValue_ORD_DETL = await _orderRepository.UpdateOrdersDetails(oRD_DTLModel);

                //
                ORD_TRCModel oRD_TRCModel = new ORD_TRCModel();
                oRD_TRCModel.SITE_NO = oRD_DTLModel.SITE_NO;
                oRD_TRCModel.ORD_NO = oRD_DTLModel.ORD_NO;

                oRD_TRCModel.STS = oRD_DTLModel.STS;
                oRD_TRCModel.SECT = oRD_DTLModel.SECT;
                oRD_TRCModel.REQ_CODE = oRD_DTLModel.REQ_CODE;
                oRD_TRCModel.ACT_DTTM = DateTime.Now;
                oRD_TRCModel.U_ID = "999";

                int insertOrdersTrackings = await _orderRepository.InsertOrdersTracking(oRD_TRCModel);

            }
            //return insertOrdersTrackings;
            return await Task.FromResult(badRequest.StatusCode);

        }

        public async Task<int> UpdateCentralReceiving(Object[] ORDs, string ACCN, string REQ_CODE, string SECT, int ATRID, string ORD_NO, string SITE_NO, string U_ID)
        {

            BadRequestResult badRequest = new BadRequestResult();

            foreach (var objORDs in ORDs)
            {

                string jsonString = JsonSerializer.Serialize(objORDs);
                var atrJson = JsonObject.Parse(jsonString);
                // aTRModel.REQ_CODE = atrJson["reQ_CODE"].ToString();
            }

            //return badRequest.StatusCode;
            //Udpate Order Details
            ORD_DTLModel oRD_DTLModel = new ORD_DTLModel();
            oRD_DTLModel.ACCN = ACCN;
            //oRD_DTLModel.ATRID = ATR_ID;
            oRD_DTLModel.REQ_CODE = REQ_CODE;// atrJson["reQ_CODE"].ToString();
            oRD_DTLModel.RCVD_DTTM = DateTime.Now;// Convert.ToDateTime(atrJson["RCVD_DTTM"]);
            oRD_DTLModel.STS = "CR";
            oRD_DTLModel.R_STS = "O";
            oRD_DTLModel.RCVD_ID = U_ID;
            //string STS = "CR";
            oRD_DTLModel.SECT = SECT;// atrJson["sect"].ToString();
            oRD_DTLModel.ATRID = ATRID;// Convert.ToInt32(atrJson["atrid"].ToString());

            oRD_DTLModel.ORD_NO = ORD_NO;// atrJson["orD_NO"].ToString();
            int updatedValue_ORD_DETL = await _orderRepository.UpdateOrdersDetails(oRD_DTLModel);

            //
            ORD_TRCModel oRD_TRCModel = new ORD_TRCModel();
            oRD_TRCModel.SITE_NO = oRD_DTLModel.SITE_NO;
            oRD_TRCModel.ORD_NO = oRD_DTLModel.ORD_NO;

            oRD_TRCModel.STS = oRD_DTLModel.STS;
            oRD_TRCModel.SECT = oRD_DTLModel.SECT;
            oRD_TRCModel.REQ_CODE = oRD_DTLModel.REQ_CODE;
            oRD_TRCModel.ACT_DTTM = DateTime.Now;
            oRD_TRCModel.SITE_NO = SITE_NO;
            oRD_TRCModel.U_ID = U_ID;

            int insertOrdersTrackings = await _orderRepository.InsertOrdersTracking(oRD_TRCModel);


            //return insertOrdersTrackings;
            return await Task.FromResult(badRequest.StatusCode);

        }

    }
}
