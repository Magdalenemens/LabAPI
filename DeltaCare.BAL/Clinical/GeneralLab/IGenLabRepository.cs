using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL
{
    public interface IGenLabRepository
    {
        Task<IEnumerable<ARFModel>> GetAllAccnActiveResultsFile();
        Task<IEnumerable<ARFModel>> GetAccnActiveResultsFileList(string ACCN);
        Task<int> UpdateActiveResultsFileGenLab(Object[] ARFs, string ACCN, string REQ_CODE, string LHF, int ARF_ID, string ORD_NO ,string RESULT);
        Task<int> UpdateNotesActiveResultsFileGenLab(int ARF_ID, string ACCN, string NOTES);
        

        Task<ARTemplateModel> GetAlphaResponsesByCD(string CD);
        Task<AVTemplateModel> GetAlphaValuesByCode(string TCODE, string RESVAL);
        Task<IVTemplateModel> GetInterpretiveValuesByCode(string TCODE, string SEX, decimal rsultvalue);

        Task<IEnumerable<ARFModel>> GetAccnActiveResultsFileInterp(string ACCN, string TCODE);

        //Available in Order Repository
        //UpdateActiveResultsFile(ARFModel aRFModel);


        Task<int> InsertResultModified(ResultModifiedModel resultModifiedModel);
        Task<int> UpdateResultModified(string PAT_ID, string ACCN, string TCODE, string CRESULT, string CV_ID, string RESULT, string V_ID);
    }
}
