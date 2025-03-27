using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using static DeltaCare.Entity.Model.EVSetUpModel;

namespace DeltaCare.BAL
{
    public class TDRepository : ITDRepository
    {
        private readonly IDataRepository _datarepository;
        public TDRepository(IDataRepository dataRepository)
        {
            _datarepository = dataRepository;
        }


        public async Task<IEnumerable<TestDirectoryModel>> GetAllTestDirectory()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TestDirectoryModel>(SPConstant.SP_ManageTestDirectory, parameterCollection)).ToList();
        }

        public async Task<int> InsertTestDirectory(TestDirectoryModel testDirectoryModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            testDirectoryModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<TestDirectoryModel>(testDirectoryModel);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.SP_ManageTestDirectory, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateTestDirectory(int Id, TestDirectoryModel testDirectoryModel)
        {
            testDirectoryModel.TD_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            testDirectoryModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<TestDirectoryModel>(testDirectoryModel);
            IEnumerable<UserFLModel> result = await _datarepository.ExecuteQueryAsync<UserFLModel>(SPConstant.SP_ManageTestDirectory, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else

            {
                return 0;
            }
        }

        public async Task<int> UpdateTestDirectoryList(IEnumerable<PriceMasterListModel> testDirectoryList)
        {
            int updatedRecordsCount = 0;
            foreach (var testDirectoryModel in testDirectoryList)
            {
                testDirectoryModel.QueryType = (int)QueryTypeEnum.Update;
                IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(testDirectoryModel);


                var result = await _datarepository.ExecuteQueryAsync<UserFLModel>(SPConstant.SP_GetTestsByDivision, parameterCollection);
                if (result != null && result.Any())
                {
                    updatedRecordsCount++;
                }
            }
            return updatedRecordsCount;
        }

        public async Task<int> DeleteTestDirectory(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "Td_id", "QueryType");
            IEnumerable<SiteModel> result = await _datarepository.ExecuteQueryAsync<SiteModel>(SPConstant.SP_ManageTestDirectory, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<IEnumerable<TDComboModel>> GetTestDirectoryCombosByTable(string TableName, string ID)
        {
            //nt queryType = (int)QueryTypeEnum.GetAll;
            TDComboModel objTDModel = new TDComboModel();
            objTDModel.TableName = TableName;
            objTDModel.ID = ID;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<TDComboModel>(objTDModel);
            return (await _datarepository.ExecuteQueryAsync<TDComboModel>(SPConstant.SP_CombosTestDirectory, parameterCollection)).ToList();
        }

        public async Task<IEnumerable<TDModel>> GetTestDirectoryByTCode(string TCODE)//GET_TD
        {
            int queryType = (int)QueryTypeEnum.Search;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(TCODE, queryType, "TCODE", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TDModel>(SPConstant.SP_ManageTestDirectory, parameterCollection)).ToList();//.Where(x => x.TCODE.Trim() == TCODE.Trim()).ToList();
        }
        public async Task<IEnumerable<TD_GTDModel>> GetGroupTestsDetailedandTestDirectory(string REQ_CODE)//GET_V_TD_GTD
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TD_GTDModel>(SPConstant.SP_GroupTestsDetailedandTestDirectory, parameterCollection))
                .Where(x => x.REQ_CODE == REQ_CODE //&& x.DTNO != ""
                ).ToList();
        }
        public async Task<IEnumerable<TD_GTDModel>> GetGroupTestsDetailedandTestDirectoryParams(string REQ_CODE, string TCODE)//GET_V_TD_GTD_TCODE
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TD_GTDModel>(SPConstant.SP_GroupTestsDetailedandTestDirectory, parameterCollection))
                .Where(x => x.REQ_CODE.Trim() == REQ_CODE.Trim() && x.TCODE.Trim() == TCODE.Trim() //&& x.DTNO.Trim() != "".Trim()
                ).ToList();
        }
        public async Task<IEnumerable<TD_GTModel>> GetTestDirectoryandGroupTestsByTCODE(string TCODE)//usp_TD_GT
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(TCODE, queryType, "TCODE", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TD_GTModel>(SPConstant.SP_TestDirectoryandGrouptTest, parameterCollection)).ToList();
            // .Where(x => x.TCODE.Trim() == TCODE.Trim()).ToList();
        }

        #region AP Test Definition
        public async Task<int> InsertAPTestDefinition(APTestDefinitionModel apTestDefinitionModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            apTestDefinitionModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<APTestDefinitionModel>(apTestDefinitionModel);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.SP_ManageAPTestDefinition, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateAPTestDefinition(int Id, APTestDefinitionModel apTestDefinitionModel)
        {
            apTestDefinitionModel.TD_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            apTestDefinitionModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<APTestDefinitionModel>(apTestDefinitionModel);
            IEnumerable<APTestDefinitionModel> result = await _datarepository.ExecuteQueryAsync<APTestDefinitionModel>(SPConstant.SP_ManageAPTestDefinition, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else

            {
                return 0;
            }
        }

        public async Task<int> DeleteAPTestDefinition(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "TD_ID", "QueryType");
            IEnumerable<APTestDefinitionModel> result = await _datarepository.ExecuteQueryAsync<APTestDefinitionModel>(SPConstant.SP_ManageAPTestDefinition, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<APTestDefinitionModel>> GetAllAPTestDefinition()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<APTestDefinitionModel>(SPConstant.SP_ManageAPTestDefinition, parameterCollection)).ToList();
        }

        #endregion

        #region EV Test Definition and Profile
        public async Task<int> InsertEVTestDefinition(EVTestDefinitionModel evTestDefinitionModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            evTestDefinitionModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<EVTestDefinitionModel>(evTestDefinitionModel);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.SP_ManageEVTestDefinition, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateEVTestDefinition(int Id, EVTestDefinitionModel evTestDefinitionModel)
        {
            evTestDefinitionModel.TD_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            evTestDefinitionModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<EVTestDefinitionModel>(evTestDefinitionModel);
            IEnumerable<EVTestDefinitionModel> result = await _datarepository.ExecuteQueryAsync<EVTestDefinitionModel>(SPConstant.SP_ManageEVTestDefinition, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else

            {
                return 0;
            }
        }

        public async Task<int> DeleteEVTestDefinition(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "TD_ID", "QueryType");
            IEnumerable<EVTestDefinitionModel> result = await _datarepository.ExecuteQueryAsync<EVTestDefinitionModel>(SPConstant.SP_ManageEVTestDefinition, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //public async Task<IEnumerable<EVTestDefinitionModel>> GetAllEVTestDefinition(int PageNumber, int RowsOfPage)
        //{
        //    int queryType = (int)QueryTypeEnum.GetAll;
        //    IList<QueryParameterForSqlMapper> parameterCollection = new List<QueryParameterForSqlMapper>
        //{
        //    new QueryParameterForSqlMapper
        //    {
        //        Name = "@QueryType",
        //        Value = queryType
        //    },
        //    new QueryParameterForSqlMapper
        //    {
        //        Name = "@PageNumber",
        //        Value = PageNumber
        //    },
        //    new QueryParameterForSqlMapper
        //    {
        //        Name = "@RowsOfPage",
        //        Value = RowsOfPage
        //    }
        //    };
        //    return (await _datarepository.ExecuteQueryAsync<EVTestDefinitionModel>(SPConstant.SP_ManageEVTestDefinition, parameterCollection)).ToList();
        //}

        public async Task<IEnumerable<EVTestDefinitionModel>> GetAllEVTestDefinition()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVTestDefinitionModel>(SPConstant.SP_ManageEVTestDefinition, parameterCollection)).ToList();
        }

        public async Task<EVTestDefinitionModel> GetEVDefinitionById(int id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(id, queryType, "TD_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVTestDefinitionModel>(SPConstant.SP_ManageEVTestDefinition, parameters)).FirstOrDefault();

        }
        public async Task<int> InserEVReferenceRanges(List<EVReferenceRangeModel> eVReferenceRangeModel)
        {
            if (eVReferenceRangeModel == null || !eVReferenceRangeModel.Any())
                throw new ArgumentException("The list cannot be null or empty.", nameof(eVReferenceRangeModel));


            // Create tasks to insert each item in parallel
            var tasks = eVReferenceRangeModel.Select(async item =>
            {
                var parameterCollection = new List<QueryParameterForSqlMapper>
                {
                new QueryParameterForSqlMapper { Name = "@TCODE", Value = item.TCODE },
                new QueryParameterForSqlMapper { Name = "@TEST_ID", Value = item.TEST_ID },
                new QueryParameterForSqlMapper { Name = "@S_TYPE", Value = item.S_TYPE },
                new QueryParameterForSqlMapper { Name = "@LHF", Value = item.LHF },
                new QueryParameterForSqlMapper { Name = "@REF_LOW", Value = item.REF_LOW },
                new QueryParameterForSqlMapper { Name = "@REF_HIGH", Value = item.REF_HIGH },
                new QueryParameterForSqlMapper { Name = "@QueryType", Value = (int)QueryTypeEnum.Insert }
                };

                // Execute insert query and return affected rows for each item
                var result = await _datarepository.ExecuteQueryAsync<int>(SPConstant.SP_ManageEVRefereceRange, parameterCollection);
                return result.FirstOrDefault();
            });

            // Wait for all tasks to complete and sum the results for the total count of rows affected
            var results = await Task.WhenAll(tasks);
            return results.Sum();
        }

        public async Task<int> UpdateEVReferenceRanges(List<EVReferenceRangeModel> referenceRanges)
        {
            if (referenceRanges == null || !referenceRanges.Any())
                throw new ArgumentException("The list cannot be null or empty.", nameof(referenceRanges));

            var tasks = referenceRanges.Select(async item =>
            {
                var parameters = new List<QueryParameterForSqlMapper>
                {
                new QueryParameterForSqlMapper { Name = "@TCODE", Value = item.TCODE },
                new QueryParameterForSqlMapper { Name = "@TEST_ID", Value = item.TEST_ID },
                new QueryParameterForSqlMapper { Name = "@S_TYPE", Value = item.S_TYPE },
                new QueryParameterForSqlMapper { Name = "@LHF", Value = item.LHF },
                new QueryParameterForSqlMapper { Name = "@REF_LOW", Value = item.REF_LOW },
                new QueryParameterForSqlMapper { Name = "@REF_HIGH", Value = item.REF_HIGH },
                new QueryParameterForSqlMapper { Name = "@QueryType", Value = (int)QueryTypeEnum.Update } // Adjust based on actual intent
                };

                // Execute the stored procedure and return affected rows for each item
                var result = await _datarepository.ExecuteQueryAsync<int>(SPConstant.SP_ManageEVRefereceRange, parameters);
                return result.ToList();
            });

            // Wait for all tasks to complete and sum the results to get the total affected row count
            var results = await Task.WhenAll(tasks);
            return results.Count();
        }


        public async Task<IEnumerable<EVReferenceRangeModel>> FetchEVTDByReferenceRange(string tCode)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(tCode, queryType, "TCODE", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVReferenceRangeModel>(SPConstant.SP_ManageEVRefereceRange, parameterCollection)).ToList();
        }

        public async Task<EVReferenceRangeModel> FetchEVTDReferenceRangeBySType(string sType)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(sType, queryType, "S_TYPE", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVReferenceRangeModel>(SPConstant.SP_ManageEVRefereceRange, parameterCollection)).FirstOrDefault();
        }

        public async Task<int> DeleteEVReferenceRange(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "EV_REFRNG_ID", "QueryType");
            IEnumerable<EVTestDefinitionModel> result = await _datarepository.ExecuteQueryAsync<EVTestDefinitionModel>(SPConstant.SP_ManageEVRefereceRange, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<IEnumerable<EVReferenceRangeModel>> GetAllEVReferenceRange(string sType)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(sType, queryType, "@SEARCH", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVReferenceRangeModel>(SPConstant.SP_ManageEVRefereceRange, parameters)).ToList();
        }
        public async Task<int> InserEVTestDefinitionProfile(List<EVProfileGTDModel> eVProfileGTDModels)
        {
            if (eVProfileGTDModels == null || !eVProfileGTDModels.Any())
                throw new ArgumentException("The list cannot be null or empty.", nameof(eVProfileGTDModels));


            // Create tasks to insert each item in parallel
            var tasks = eVProfileGTDModels.Select(async item =>
            {
                var parameterCollection = new List<QueryParameterForSqlMapper>
                {
                    new QueryParameterForSqlMapper { Name = "@TCODE", Value = item.TCODE },
                    new QueryParameterForSqlMapper { Name = "@GTDTCODE", Value = item.GTDTCODE },
                    new QueryParameterForSqlMapper { Name = "@PROFILE_FULLNAME", Value = item.PROFILE_FULLNAME },
                    new QueryParameterForSqlMapper { Name = "@PNDG", Value = item.PNDG },
                    new QueryParameterForSqlMapper { Name = "@QueryType", Value = (int)QueryTypeEnum.Insert }
                };

                // Execute insert query and return affected rows for each item
                var result = await _datarepository.ExecuteQueryAsync<int>(SPConstant.SP_ManageEVTestDefinitionProfile, parameterCollection);
                return result.FirstOrDefault();
            });

            // Wait for all tasks to complete and sum the results for the total count of rows affected
            var results = await Task.WhenAll(tasks);
            return results.Sum();
        }

        public async Task<int> UpdateEVTestDefinitionProfile(List<EVProfileGTDModel> eVProfileGTDModels)
        {
            if (eVProfileGTDModels == null || !eVProfileGTDModels.Any())
                throw new ArgumentException("The list cannot be null or empty.", nameof(eVProfileGTDModels));

            var tasks = eVProfileGTDModels.Select(async item =>
            {
                var parameters = new List<QueryParameterForSqlMapper>
                {
                    new QueryParameterForSqlMapper { Name = "@TCODE", Value = item.TCODE },
                    new QueryParameterForSqlMapper { Name = "@GTDTCODE", Value = item.GTDTCODE },
                    new QueryParameterForSqlMapper { Name = "@PROFILE_FULLNAME", Value = item.PROFILE_FULLNAME },
                    new QueryParameterForSqlMapper { Name = "@PNDG", Value = item.PNDG },
                    new QueryParameterForSqlMapper { Name = "@QueryType", Value = (int)QueryTypeEnum.Update } // Adjust based on actual intent
                };

                // Execute the stored procedure and return affected rows for each item
                var result = await _datarepository.ExecuteQueryAsync<int>(SPConstant.SP_ManageEVTestDefinitionProfile, parameters);
                return result.ToList();
            });

            // Wait for all tasks to complete and sum the results to get the total affected row count
            var results = await Task.WhenAll(tasks);
            return results.Count();
        }
        public async Task<IEnumerable<EVTestDefinitionProfileModel>> GetAllEVTestDefinitionProfile()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVTestDefinitionProfileModel>(SPConstant.SP_ManageEVTestDefinitionProfile, parameterCollection)).ToList();
        }

        public async Task<IEnumerable<EVProfileGTDModel>> FetchEVProfileFromGTD(string tCode)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(tCode, queryType, "TCODE", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVProfileGTDModel>(SPConstant.SP_ManageEVTestDefinitionProfile, parameterCollection)).ToList();
        }

        public async Task<EVProfileGTDModel> FetchProfileByGTDTCode(string gtdTCode)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(gtdTCode, queryType, "GTDTCODE", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVProfileGTDModel>(SPConstant.SP_ManageEVTestDefinitionProfile, parameterCollection)).FirstOrDefault();
        }

        public async Task<IEnumerable<EVProfileGTDModel>> GetAllEVProfiles(string search)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(search, queryType, "@SEARCH", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVProfileGTDModel>(SPConstant.SP_ManageEVTestDefinitionProfile, parameters)).ToList();
        }

        public async Task<int> DeleteEVProfile(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "GTD_ID", "QueryType");
            IEnumerable<EVProfileGTDModel> result = await _datarepository.ExecuteQueryAsync<EVProfileGTDModel>(SPConstant.SP_ManageEVTestDefinitionProfile, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> InsertAnlMethod(ANLMethodModel aNLMethod)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            aNLMethod.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ANLMethodModel>(aNLMethod);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.SP_ManageANLMethod, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateAnlMethod(int Id, ANLMethodModel aNLMethod)
        {
            aNLMethod.ANL_MTHD_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            aNLMethod.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<ANLMethodModel>(aNLMethod, "QueryType"); ;
            IEnumerable<ANLMethodModel> result = await _datarepository.ExecuteQueryAsync<ANLMethodModel>(SPConstant.SP_ManageANLMethod, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DeleteAnlMethod(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            var parameters = ParameterGenerator.CreateParameterList(Id, queryType, "ANL_MTHD_ID ", "QueryType");
            IEnumerable<ANLMethodModel> result = await _datarepository.ExecuteQueryAsync<ANLMethodModel>(SPConstant.SP_ManageANLMethod, parameters);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<ANLMethodModel>> GetAllANLMethod()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ANLMethodModel>(SPConstant.SP_ManageANLMethod, parameters)).ToList();
        }

        public async Task<ANLMethodModel> GetANLMethodById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Id, queryType, "ANL_MTHD_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<ANLMethodModel>(SPConstant.SP_ManageANLMethod, parameters)).FirstOrDefault();
        }

        public async Task<int> InsertEVSubHeader(EVSubHeaderModel eVSubHeader)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            eVSubHeader.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<EVSubHeaderModel>(eVSubHeader);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.SP_ManageEVSubHeader, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }


        public async Task<int> UpdateEVSubHeader(int Id, EVSubHeaderModel eVSubHeader)
        {
            eVSubHeader.EV_SUBHDR_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            eVSubHeader.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<EVSubHeaderModel>(eVSubHeader, "QueryType"); ;
            IEnumerable<EVSubHeaderModel> result = await _datarepository.ExecuteQueryAsync<EVSubHeaderModel>(SPConstant.SP_ManageEVSubHeader, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        public async Task<int> DeleteEVSubHeader(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            var parameters = ParameterGenerator.CreateParameterList(Id, queryType, "EV_SUBHDR_ID ", "QueryType");
            IEnumerable<EVSubHeaderModel> result = await _datarepository.ExecuteQueryAsync<EVSubHeaderModel>(SPConstant.SP_ManageEVSubHeader, parameters);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<IEnumerable<EVSubHeaderModel>> GetAllEVSubHeader()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVSubHeaderModel>(SPConstant.SP_ManageEVSubHeader, parameterCollection)).ToList();
        }
        public async Task<EVSubHeaderModel> GetEVSubHeaderById(int Id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Id, queryType, "EV_SUBHDR_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<EVSubHeaderModel>(SPConstant.SP_ManageEVSubHeader, parameters)).FirstOrDefault();
        }

        #endregion

        #region CG Test Defintion and Profile
        public async Task<int> InsertCGTestDefinition(CGTestDefinitionModel cgTestDefinitionModel)
        {
            int queryType = (int)QueryTypeEnum.Insert;
            cgTestDefinitionModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<CGTestDefinitionModel>(cgTestDefinitionModel);
            DataSet getDataDto = _datarepository.ExecuteQuery(SPConstant.SP_ManageCGTestDefinition, parameterCollection);
            var getData = CommonHelper.ConvertDataTableToList<returnDataDto>(getDataDto.Tables[0]).FirstOrDefault();
            return await Task.FromResult(getData.returnData);
        }

        public async Task<int> UpdateCGTestDefinition(int Id, CGTestDefinitionModel cgTestDefinitionModel)
        {
            cgTestDefinitionModel.TD_ID = Id;
            int queryType = (int)QueryTypeEnum.Update;
            cgTestDefinitionModel.QueryType = queryType;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<CGTestDefinitionModel>(cgTestDefinitionModel);
            IEnumerable<CGTestDefinitionModel> result = await _datarepository.ExecuteQueryAsync<CGTestDefinitionModel>(SPConstant.SP_ManageCGTestDefinition, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else

            {
                return 0;
            }
        }

        public async Task<int> DeleteCGTestDefinition(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "TD_ID", "QueryType");
            IEnumerable<CGTestDefinitionModel> result = await _datarepository.ExecuteQueryAsync<CGTestDefinitionModel>(SPConstant.SP_ManageCGTestDefinition, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<CGTestDefinitionModel>> GetAllCGTestDefinition()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CGTestDefinitionModel>(SPConstant.SP_ManageCGTestDefinition, parameterCollection)).ToList();
        }

        public async Task<CGTestDefinitionModel> GetCGDefinitionById(int id)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(id, queryType, "TD_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CGTestDefinitionModel>(SPConstant.SP_ManageCGTestDefinition, parameters)).FirstOrDefault();
        }

        public async Task<(int RowsInserted, int RowsUpdated)> ManageCGTestDefinitionProfileAsync(List<CGProfileGTDModel> cgProfileGTDModels)
        {
            if (cgProfileGTDModels == null || cgProfileGTDModels.Count == 0)
            {
                throw new ArgumentException("The input list cannot be null or empty.", nameof(cgProfileGTDModels));
            }

            // Prepare the TVP as a DataTable
            var tvp = CreateCGProfileDataTable(cgProfileGTDModels);

            // Create parameters for the stored procedure
            var parameters = new List<QueryParameterForSqlMapper>
        {
            new QueryParameterForSqlMapper
            {
                Name = TableParamConstant.Param_CGPROFILEDATA,
                Value = tvp,
                //TypeName = TVP_TYPE_NAME,
                DbType = DbType.Object // Ensure this matches your IDataRepository implementation
            }
        };

            // Execute the stored procedure and handle the result
            var result = await _datarepository.ExecuteQueryAsync<QueryResult>(SPConstant.
                SP_MANAGE_CG_TEST_DEFINITION_PROFILE,
                parameters
            );

            if (result == null || !result.Any())
            {
                return (0, 0);
            }

            // Assume the stored procedure returns a single row with the counts
            var firstResult = result.FirstOrDefault();
            return firstResult != null ? (firstResult.RowsInserted, firstResult.RowsUpdated) : (0, 0);
        }

        private DataTable CreateCGProfileDataTable(IEnumerable<CGProfileGTDModel> models)
        {
            var table = new DataTable();
            table.Columns.Add("GTD_ID", typeof(int));
            table.Columns.Add("GTNO", typeof(string));
            table.Columns.Add("REQ_CODE", typeof(string));
            table.Columns.Add("GTDTCODE", typeof(string));
            table.Columns.Add("DTNO", typeof(int));
            table.Columns.Add("PROFILE_FULLNAME", typeof(string));
            table.Columns.Add("PNDG", typeof(string));

            foreach (var model in models)
            {
                table.Rows.Add(
                    model.GTD_ID,
                    model.GTNO,
                    model.REQ_CODE,
                    model.GTDTCODE,
                    model.DTNO,
                    model.PROFILE_FULLNAME,
                    model.PNDG
                );
            }

            return table;
        }

        public async Task<IEnumerable<CGTestDefinitionProfileModel>> GetAllCGTestDefinitionProfile()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CGTestDefinitionProfileModel>(SPConstant.SP_ManageCGTestDefinitionProfile, parameterCollection)).ToList();
        }
        public async Task<IEnumerable<CGProfileGTDModel>> FetchCGProfileFromGTD(string tCode)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(tCode, queryType, "TCODE", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CGProfileGTDModel>(SPConstant.SP_ManageCGTestDefinitionProfile, parameterCollection)).ToList();
        }
        public async Task<CGProfileGTDModel> FetchCGProfileByGTDTCode(string gtdTCode)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(gtdTCode, queryType, "GTDTCODE", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CGProfileGTDModel>(SPConstant.SP_ManageCGTestDefinitionProfile, parameterCollection)).FirstOrDefault();
        }

        public async Task<IEnumerable<CGProfileGTDModel>> GetAllCGProfiles(string search)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(search, queryType, "@SEARCH", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<CGProfileGTDModel>(SPConstant.SP_ManageCGTestDefinitionProfile, parameters)).ToList();
        }

        public async Task<int> DeleteCGProfile(int Id)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Id, queryType, "GTD_ID", "QueryType");
            IEnumerable<CGProfileGTDModel> result = await _datarepository.ExecuteQueryAsync<CGProfileGTDModel>(SPConstant.SP_ManageCGTestDefinitionProfile, parameterCollection);
            if (result != null && result.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region Test Dirrectory Add Reference
        public async Task<IEnumerable<TDDivision>> GetAllTDDiv()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TDDivision>(SPConstant.SP_ManageTD, parameters)).ToList();
        }

        public async Task<IEnumerable<TDDivision>> GetAllSectByDiv(int Div)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Div, queryType, "DIV", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TDDivision>(SPConstant.SP_ManageTD, parameters)).ToList();
        }

        public async Task<IEnumerable<TDModel>> GetTestDirectory(int Div)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Div, queryType, "DIV", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TDModel>(SPConstant.SP_ManageTestDirectory, parameterCollection)).ToList();
        }

        public async Task<IEnumerable<TDModel>> GetAllTestsByDivision(int Div)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(Div, queryType, "DIV", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TDModel>(SPConstant.SP_GetTestsByDivision, parameterCollection)).ToList();
        }

        public async Task<IEnumerable<TDReferenceRangeModel>> GetAllReferenceRange()
        {
            int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(queryType, "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TDReferenceRangeModel>(SPConstant.SP_ManageRefRng, parameters)).ToList();
        }

        public async Task<int> InsertReferenceRange(List<TDReferenceRangeModel> refRange)
        {
            int rowsAffected = 0;
            try
            {
                DeleteRefRangeByTCode(refRange[0].TCODE.ToString());
                DataTable dtreferenceRang = CommonHelper.ToDataTable(refRange);
                if (dtreferenceRang.Columns.Contains("QueryType"))
                    dtreferenceRang.Columns.Remove("QueryType");
                if (dtreferenceRang.Columns.Contains("REFID"))
                    dtreferenceRang.Columns.Remove("REFID");
                rowsAffected = await _datarepository.ExecuteDataTable(SPConstant.SP_InsertTDReferenceRange, dtreferenceRang, SPConstant.type_Td_ReferenceRange);
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }

            return rowsAffected;
        }


        private int DeleteRefRangeByTCode(string TCode)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(TCode, queryType, "@TCODE", "QueryType");
            return _datarepository.ExecuteQueryAsync<int>(SPConstant.SP_ManageRefRng, parameters).Result.ToList()[0];
        }
        #endregion

        #region Test Directory - Profile
        public async Task<IEnumerable<TDProfileModel>> GetTestDirectoryProfile()
        {
            try
            {
                int queryType = (int)QueryTypeEnum.GetAll;
                IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(queryType, "QueryType");
                var S = (await _datarepository.ExecuteQueryAsync<TDProfileModel>(SPConstant.SP_ManageTestDirectoryProfile, parameterCollection)).ToList();
                return S;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<TDGTDModel>> GetTestDirectoryGTD(string testId)
        {
            int queryType = (int)QueryTypeEnum.GetById;
            IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(testId, queryType, "TEST_ID", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TDGTDModel>(SPConstant.SP_ManageTestDirectoryProfile, parameterCollection)).ToList();
        }

        public async Task<IEnumerable<TDProfileModel>> GetTestDirectoryByDProfile()
        {
            try
            {
                int queryType = (int)QueryTypeEnum.GetByName;
                IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList(string.Empty, queryType, "SEARCH", "QueryType");

                var S = (await _datarepository.ExecuteQueryAsync<TDProfileModel>(SPConstant.SP_ManageTestDirectoryProfile, parameterCollection)).ToList();
                return S;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<TDProfileModel>> GetSearchTestDirectoryByDProfile(string Search)
        {
            int queryType = (int)QueryTypeEnum.GetByName;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(Search, queryType, "SEARCH", "QueryType");
            return (await _datarepository.ExecuteQueryAsync<TDProfileModel>(SPConstant.SP_ManageTestDirectoryProfile, parameters)).ToList();
        }


        public async Task<int> InsertTestDirectoryProfile(List<GTDModel> tdProfileList)
        {
            int rowsAffected = 0;
            try
            {
                int iCounter = 0;
                List<InsertGTDModel> tdProfileInsertModels = new List<InsertGTDModel>();
                foreach (var profile in tdProfileList)
                {
                    var tdProfileInsertModel = new InsertGTDModel();
                    tdProfileInsertModel.GTNO = profile.GTNO;
                    tdProfileInsertModel.GRP_NO = profile.GRP_NO;
                    tdProfileInsertModel.REQ_CODE = profile.REQ_CODE;
                    tdProfileInsertModel.DTNO = profile.DTNO;
                    tdProfileInsertModel.TCODE = profile.TCODE;
                    tdProfileInsertModel.FULL_NAME = profile.FULL_NAME;
                    tdProfileInsertModel.PNDG = profile.PNDG;
                    tdProfileInsertModel.S_TYPE = profile.S_TYPE;
                    tdProfileInsertModel.MDL = profile.MDL;
                    tdProfileInsertModel.RSTP = profile.RSTP;
                    tdProfileInsertModel.S = profile.S;
                    tdProfileInsertModel.GP = profile.GP;
                    tdProfileInsertModel.SEQ = profile.SEQ;
                    tdProfileInsertModels.Add(tdProfileInsertModel);
                    iCounter++;
                }

                DeleteProfileByTestId(tdProfileList[0].GTNO.ToString());
                DataTable dtTdProfile = CommonHelper.ToDataTable(tdProfileInsertModels);
                if (dtTdProfile.Columns.Contains("QueryType"))
                    dtTdProfile.Columns.Remove("QueryType");

                rowsAffected = await _datarepository.ExecuteDataTable(SPConstant.Sp_InsertTestDirectoryProfile, dtTdProfile, SPConstant.type_InsertTDProfile);
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }

            return rowsAffected;
        }

        private int DeleteProfileByTestId(string TestId)
        {
            int queryType = (int)QueryTypeEnum.Delete;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(TestId, queryType, "@TEST_ID", "QueryType");
            return _datarepository.ExecuteQueryAsync<int>(SPConstant.SP_ManageTestDirectoryProfile, parameters).Result.ToList()[0];
        }

        #endregion
    }
}
