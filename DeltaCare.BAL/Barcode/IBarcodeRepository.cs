using DeltaCare.Entity.Model;

namespace DeltaCare.BAL
{
    public interface IBarcodeRepository
    {
        Task<IEnumerable<BarcodeModel>> GenerateBarcode(string ORD_NO); //GET_BARCODE
        string GetBarCode(string accn);

        string GenerateQR(string Data);

        string GetCodePDF(string Data);

    }
}
