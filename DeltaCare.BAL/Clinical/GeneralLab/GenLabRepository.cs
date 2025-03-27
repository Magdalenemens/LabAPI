using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DeltaCare.BAL
{
    public class GenLabRepository : IGenLabRepository
    {
        private readonly IDataRepository _dataRepository;
        private readonly IOrderRepository _orderRepository;
        public GenLabRepository(IDataRepository dataRepository, IOrderRepository orderRepository)
        {
            _dataRepository = dataRepository;
            _orderRepository = orderRepository;

        }

        public async Task<IEnumerable<ARFModel>> GetAllAccnActiveResultsFile()
        {
            int queryType = (int)QueryTypeEnum.Search;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<ARFModel>(SPConstant.SP_ManageActiveResultsFile, parameterCollection)).ToList();
        }
        public async Task<IEnumerable<ARFModel>> GetAccnActiveResultsFileList(string ACCN)
        {
            ARFModel aRFModel = new ARFModel();
            aRFModel.ACCN = ACCN;
            int queryType = (int)QueryTypeEnum.Search;
            aRFModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ARFModel>(aRFModel);
            return (await _dataRepository.ExecuteQueryAsync<ARFModel>(SPConstant.SP_ManageActiveResultsFile, parameterCollection)).ToList();
        }

        public async Task<int> UpdateActiveResultsFileGenLab(Object[] ARFs, string ACCN, string REQ_CODE, string LHF, int ARF_ID, string ORD_NO, string RESULT)
        {

            BadRequestResult badRequest = new BadRequestResult();
            var ARF = ARFs.ToList();
            ARFModel aRFModel = new ARFModel();
            int retValue = 0;
            foreach (var objARFs in ARF)
            {
                string jsonString = JsonSerializer.Serialize(objARFs);
                var arfJson = JsonObject.Parse(jsonString);

                aRFModel.ACCN = arfJson["ACCN"].ToString();
                aRFModel.REQ_CODE = arfJson["REQ_CODE"].ToString();
                aRFModel.LHF = arfJson["LHF"].ToString();
                aRFModel.ARF_ID = Convert.ToInt32(arfJson["ARF_ID"].ToString());
                aRFModel.ORD_NO = arfJson["ORD_NO"].ToString();
                aRFModel.R_STS = arfJson["R_STS"].ToString() == "RSA" ? "RS" : arfJson["R_STS"].ToString();
                aRFModel.RESULT = arfJson["RESULT"].ToString();
                aRFModel.R_ID = arfJson["R_ID"].ToString();


                if (aRFModel == null)
                    return badRequest.StatusCode;
                if (aRFModel.ARF_ID == 0) return badRequest.StatusCode;

                int queryType = (int)QueryTypeEnum.Update;
                aRFModel.QueryType = queryType;
                IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ARFModel>(aRFModel, "QueryType");
                IEnumerable<ARFModel> result = await _dataRepository.ExecuteQueryAsync<ARFModel>(SPConstant.SP_ManageActiveResultsFile, parameterCollection);
                if (result != null && result.Any())
                {
                    retValue = 1;
                    ResultModifiedModel resultModifiedModel = new ResultModifiedModel();
                    if (arfJson["R_STS"].ToString() == "RSA")
                    {
                        aRFModel.PAT_ID = arfJson["PAT_ID"].ToString();
                        resultModifiedModel.PAT_ID = aRFModel.PAT_ID;
                        resultModifiedModel.ACCN = aRFModel.ACCN;
                        resultModifiedModel.TCODE = aRFModel.REQ_CODE;
                        //resultModifiedModel.CRESULT = aRFModel.PAT_ID;
                        resultModifiedModel.CV_ID = aRFModel.R_ID;
                        resultModifiedModel.RESULT = aRFModel.RESULT;
                        resultModifiedModel.CVER_DTTM=DateTime.Now;
                        resultModifiedModel.VER_DTTM = DateTime.Now;
                        resultModifiedModel.V_ID = aRFModel.R_ID;

                        await InsertResultModified(resultModifiedModel);
                    }
                }
                else
                    retValue = 0;
            }
            return await Task.FromResult(retValue);
        }


        public async Task<int> UpdateNotesActiveResultsFileGenLab(int ARF_ID, string ACCN, string NOTES)
        {

            BadRequestResult badRequest = new BadRequestResult();

            ARFModel aRFModel = new ARFModel();
            int retValue = 0;


            aRFModel.ACCN = ACCN;
            aRFModel.ARF_ID = ARF_ID;
            aRFModel.NOTES = NOTES;
            if (aRFModel == null)
                return badRequest.StatusCode;
            if (aRFModel.ARF_ID == 0) return badRequest.StatusCode;

            int queryType = (int)QueryTypeEnum.Update;
            aRFModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ARFModel>(aRFModel, "QueryType");
            IEnumerable<ARFModel> result = await _dataRepository.ExecuteQueryAsync<ARFModel>(SPConstant.SP_ManageActiveResultsFile, parameterCollection);
            if (result != null && result.Any())
                retValue = 1;
            else
                retValue = 0;

            return await Task.FromResult(retValue);
        }

        public async Task<ARTemplateModel> GetAlphaResponsesByCD(string CD)
        {
            int queryType = (int)QueryTypeEnum.Search;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(CD, queryType, "CD", "QueryType");
            return (await _dataRepository.ExecuteQueryAsync<ARTemplateModel>(SPConstant.SP_AlphaResponses, parameterCollection)).FirstOrDefault();
        }
        public async Task<AVTemplateModel> GetAlphaValuesByCode(string TCODE, string RESVAL)
        {
            int queryType = (int)QueryTypeEnum.Search;
            AVTemplateModel aVTemplateModel = new AVTemplateModel();
            aVTemplateModel.QueryType = queryType;
            aVTemplateModel.TCODE = TCODE;
            aVTemplateModel.RESVAL = RESVAL;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<AVTemplateModel>(aVTemplateModel);
            return (await _dataRepository.ExecuteQueryAsync<AVTemplateModel>(SPConstant.SP_AlphaValues, parameterCollection)).FirstOrDefault();
        }
        public async Task<IVTemplateModel> GetInterpretiveValuesByCode(string TCODE, string SEX, decimal rsultvalue)
        {
            int queryType = (int)QueryTypeEnum.Search;
            IVTemplateModel iVTemplateModel = new IVTemplateModel();
            iVTemplateModel.QueryType = queryType;
            iVTemplateModel.TCODE = TCODE;
            iVTemplateModel.SEX = SEX;
            iVTemplateModel.rsultvalue = rsultvalue;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<IVTemplateModel>(iVTemplateModel);
            return (await _dataRepository.ExecuteQueryAsync<IVTemplateModel>(SPConstant.SP_AInterpretiveValues, parameterCollection)).FirstOrDefault();
        }

        public async Task<IEnumerable<ARFModel>> GetAccnActiveResultsFileInterp(string ACCN, string TCODE)
        {
            ARFModel aRFModel = new ARFModel();
            aRFModel.ACCN = ACCN;
            aRFModel.TCODE = TCODE;
            int queryType = (int)QueryTypeEnum.Search;
            aRFModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ARFModel>(aRFModel);
            return (await _dataRepository.ExecuteQueryAsync<ARFModel>(SPConstant.SP_ManageActiveResultsFile, parameterCollection)).ToList();
        }


        #region Result Modified
        public async Task<int> InsertResultModified(ResultModifiedModel resultModifiedModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            resultModifiedModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ResultModifiedModel>(resultModifiedModel);
            DataSet getDataDto = _dataRepository.ExecuteQuery(SPConstant.SP_ManageResultsModified, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }
        public async Task<int> UpdateResultModified(string PAT_ID, string ACCN, string TCODE, string CRESULT, string CV_ID, string RESULT, string V_ID)
        {

            BadRequestResult badRequest = new BadRequestResult();

            ResultModifiedModel resultModifiedModel = new ResultModifiedModel();
            int retValue = 0;


            resultModifiedModel.ACCN = ACCN;
            resultModifiedModel.PAT_ID = PAT_ID;
            resultModifiedModel.TCODE = TCODE;
            resultModifiedModel.CRESULT = CRESULT;
            resultModifiedModel.CV_ID = CV_ID;
            resultModifiedModel.RESULT = RESULT;
            resultModifiedModel.V_ID = V_ID;

            if (resultModifiedModel == null)
                return badRequest.StatusCode;
            if (string.IsNullOrEmpty(resultModifiedModel.ACCN)) return badRequest.StatusCode;

            int queryType = (int)QueryTypeEnum.Update;
            resultModifiedModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ResultModifiedModel>(resultModifiedModel, "QueryType");
            IEnumerable<ResultModifiedModel> result = await _dataRepository.ExecuteQueryAsync<ResultModifiedModel>(SPConstant.SP_ManageResultsModified, parameterCollection);
            if (result != null && result.Any())
                retValue = 1;
            else
                retValue = 0;

            return await Task.FromResult(retValue);
        }
        #endregion
    }
}
