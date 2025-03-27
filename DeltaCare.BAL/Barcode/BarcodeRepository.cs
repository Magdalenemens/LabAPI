using BarcodeGenerator;
using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Hosting;
using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace DeltaCare.BAL.Barcode
{
    public class BarcodeRepository : IBarcodeRepository
    {
        private readonly IDataRepository _dataRepository;
        private readonly IWebHostEnvironment _env;
        public BarcodeRepository(IDataRepository dataRepository, IWebHostEnvironment env)
        {
            _dataRepository = dataRepository; _env = env;
        }
        public async Task<IEnumerable<BarcodeModel>> GenerateBarcode(string ORD_NO)//GET_BARCODE
        {
            //int queryType = (int)QueryTypeEnum.GetAll;
            IList<QueryParameterForSqlMapper> parameters = ParameterGenerator.CreateParameterList(ORD_NO, "ORD_NO");
            return (await _dataRepository.ExecuteQueryAsync<BarcodeModel>(SPConstant.SP_GenerateBarcode, parameters)).ToList();
        }

        public string GetBarCode(string accn)
        {
            return Generate128.GetBarCodeB64(accn, SPConstant.codeWidth);
        }

        public string GenerateQR(string Data)
        {
            // Generate the QR code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Drawing.Bitmap qrCodeImage = qrCode.GetGraphic(20);
            byte[] data;
            using (MemoryStream m = new MemoryStream())
            {
                qrCodeImage.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                data = m.ToArray();
            }


            return Convert.ToBase64String(data);
        }

        public string GetCodePDF(string Data)
        {
            string codeFilePath = _env.ContentRootPath + "\\Images\\".Replace("~\\", ""); 
            string blankFile = codeFilePath + @"\Blank.png";
            string[] accn = Data.Split(",");
            accn = accn.Distinct().ToArray();
            if (accn.Length > 0)
            {
                bool imagePath = System.IO.Directory.Exists(codeFilePath);

                if (!imagePath)
                    System.IO.Directory.CreateDirectory(codeFilePath);

                bool blankExists = System.IO.Directory.Exists(blankFile);

                if (!blankExists)
                {
                    byte[] blankBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAUcAAADjCAYAAAAWs9YnAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAMlSURBVHhe7dghjkIxAEXRMvvfCIoFYFBsAEvQILDYzxdNmMlcg4VzTPs2cNN0s6wGAH / 8zBOAX8QRIIgjQBBHgCCOAEEcAYI4AgRxBAjiCBDEESCII0AQR4AgjgBBHAGCOAIEcQQI4ggQxBEgiCNAEEeAII4AQRwBgjgCBHEECOIIEMQRIIgjQBBHgCCOAEEcAYI4AgRxBAjiCBDEESCII0AQR4AgjgBBHAGCOAIEcQQI4ggQxBEgiCNAEEeAII4AQRwBgjgCBHEECOIIEMQRIIgjQBBHgCCOAEEcAYI4AgRxBAjiCBDEESCII0AQR4AgjgBBHAGCOAIEcQQI4ggQxBEgiCNAEEeAII4AQRwBgjgCBHEECOIIEMQRIIgjQBBHgCCOAEEcAYI4AgRxBAjiCBDEESCII0AQR4AgjgBBHAGCOAIEcQQI4ggQxBEgiCNAEEeAII4AQRwBgjgCBHEECOIIEMQR3nQ9HcflsJuLTyWO8KbH / TbO++1cfKrNspp3ACYvR4AgjgBBHAGCOAIEcQQI4ggQxBEgiCNAEEeAII4AQRwBgjgCBHEECOIIEMQRIIgjQBBHgCCOAEEcAYI4AgRxBAjiCBDEESCII0AQR4AgjgBBHAGCOAIEcQQI4ggQxBEgiCNA + Oo4Xk / HcTns5gJ4 + eo4Pu63cd5v5wJ42SyreQdg8ucIEMQRIIgjQBBHgCCOAEEcAYI4AgRxBAjiCBDEESCII0AQR4AgjgBBHAGCOAIEcQQI4ggQxBEgiCNAEEeAII4AQRwBgjgCBHEECOIIEMQRIIgjQBBHgCCOAEEcAYI4AgRxBAjiCBDEESCII0AQR4AgjgBBHAGCOAIEcQQI4ggQxBEgiCNAEEeAII4AQRwBgjgCBHEECOIIEMQRIIgjQBBHgCCOAEEcAYI4AgRxBAjiCBDEESCII0AQR4AgjgBBHAGCOAIEcQQI4ggQxBEgiCNAEEeAII4AQRwBgjgCBHEECOIIEMQRIIgjQBBHgCCOAEEcAYI4AgRxBAjiCBDEESCII0AQR4AgjgBBHAGCOAIEcQQI4ggQxBEgiCPAP2M8AcyvIWzHkadNAAAAAElFTkSuQmCC");
                    using var blankWriter = new BinaryWriter(File.OpenWrite(blankFile));
                    blankWriter.Write(blankBytes);
                    blankWriter.Dispose();
                }

                foreach (var item in accn)
                {
                    if (item != string.Empty)
                        GenerateCombinationCode(item);
                }   
 
                byte[] pdfBytes = MergeImg2Pdf(codeFilePath);
                return Convert.ToBase64String(pdfBytes);
            }
            else
            {
                return string.Empty;
            }
        }
        private void DisplayQRCodeImage(string imagePath)
        {
            try
            {
                // Check if the file exists
                if (System.IO.File.Exists(imagePath))
                {
                    // Use the default image viewer to open and display the QR code image
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = imagePath,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
                else
                {
                    Console.WriteLine("QR code image not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void GenerateCombinationCode(string Accn)
        {
            #region Directory 

            string codeFilePath = _env.ContentRootPath + "\\Images\\".Replace("~\\", "");       

            #endregion

            #region FileNames

            string barCodeFile = codeFilePath + Accn + "bar.png";
            string qrCodeFile = codeFilePath + Accn + "qr.png";
            string preFinalFile = codeFilePath + Accn + "barqr.png";
            string FinalFile = codeFilePath + Accn + "FinalQRCode.png";
            string blankFile = codeFilePath + @"\Blank.png";

            #endregion

            #region Barcode

            byte[] barBytes = Convert.FromBase64String(GetBarCode(Accn));
            using var barWriter = new BinaryWriter(File.OpenWrite(barCodeFile));
            barWriter.Write(barBytes);
            barWriter.Dispose();

            #endregion

            #region Generate the QR code

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Accn, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            byte[] data;
            using (MemoryStream m = new MemoryStream())
            {
                qrCodeImage.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                data = m.ToArray();
            }


            using var qrWriter = new BinaryWriter(File.OpenWrite(qrCodeFile));
            qrWriter.Write(data);
            qrWriter.Dispose();

            #endregion

            #region Combine Bar & QR Code

            System.Drawing.Image img1 = System.Drawing.Image.FromFile(barCodeFile);
            System.Drawing.Image img2 = System.Drawing.Image.FromFile(qrCodeFile);

            System.Drawing.Image img1Plus = ResizeImage(img1, 190, 40);
            System.Drawing.Image img2Plus = ResizeImage(img2, 40, 40);

            List<System.Drawing.Image> fileList = new List<System.Drawing.Image>();
            fileList.Add(img1Plus);
            fileList.Add(img2Plus);
            Bitmap bitmap1 = MergeImages(fileList);
            bitmap1.Save(preFinalFile, System.Drawing.Imaging.ImageFormat.Png);

            #endregion

            #region final Bar & QR Code with Patient Details


            img1 = System.Drawing.Image.FromFile(blankFile);
            img2Plus = System.Drawing.Image.FromFile(preFinalFile);

            img1Plus = ResizeImage(img1, 50, 30);
            fileList = new List<System.Drawing.Image>();
            fileList.Add(img1Plus);
            fileList.Add(img2Plus);
            Bitmap bitmap2 = MergeImages(fileList);
            bitmap2.Save(FinalFile, System.Drawing.Imaging.ImageFormat.Png);


            string firstText = "1021688";       /// need to get from DB
            string secondText = "TALAL AL";     /// need to get from DB
            string secondTextUp = "M 26 Y";     /// need to get from DB

            string thirdText = "01_24_19_100";  /// need to get from DB
            string fourthText = "16/08  18:16"; /// need to get from DB

            PointF firstLocation = new PointF(10f, 10f);
            PointF secondLocation = new PointF(10f, 20f);
            PointF secondUpLocation = new PointF(100f, 10f);
            PointF thirdLocation = new PointF(50f, 100f);
            PointF fourthLocation = new PointF(50f, 115f);

            Bitmap newBitmap;
            using (var bitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(FinalFile))//load the image file
            {
                using (System.Drawing.Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 8))
                    {
                        graphics.DrawString(firstText, arialFont, Brushes.Black, firstLocation);
                        graphics.DrawString(secondText, arialFont, Brushes.Black, secondLocation);
                        graphics.DrawString(secondTextUp, arialFont, Brushes.Black, secondUpLocation);
                        graphics.DrawString(thirdText, arialFont, Brushes.Black, thirdLocation);
                        graphics.DrawString(fourthText, arialFont, Brushes.Black, fourthLocation);
                    }
                }
                newBitmap = new Bitmap(bitmap);
            }

            newBitmap.Save(FinalFile);//save the image file
            newBitmap.Dispose();

            #endregion
        }


        public static Bitmap MergeImages(List<System.Drawing.Image> images)
        {
            int outputImageWidth = 0;
            int outputImageHeight = 1;
            foreach (var image in images)
            {
                outputImageHeight += image.Height;
                if (image.Width > outputImageWidth)
                {
                    outputImageWidth = image.Width;
                }
            }

            Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(outputImage))
            {
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.Clear(System.Drawing.Color.White);
                graphics.DrawImage(images[0], new Rectangle(new Point(), images[0].Size), new Rectangle(new Point(), images[0].Size), GraphicsUnit.Pixel);
                graphics.DrawImage(images[1], new Rectangle(new Point(0, images[0].Height + 1), images[1].Size), new Rectangle(new Point(), images[1].Size), GraphicsUnit.Pixel);
            }

            return outputImage;
        }


        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private byte[] MergeImg2Pdf(string CodePath)
        {
            try
            {
                Document document = Document.Create(container =>
                {
                    var headerStyle = TextStyle.Default.FontFamily("Calibri");
                    _ = container
                    .Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(0.5f, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(headerStyle);

                        #region Page Header

                        string madharshaNameEn = "DelaCare Lab";
                        string madharshaNameAr = "معمل دلتا كير";
                        string madharshaAddress = "Riyadh";

                        page.Header()
                               .PaddingBottom(10)
                               .Border(0.5f)
                               .Background("#010141")
                               .Table(table =>
                               {
                                   table.ColumnsDefinition(columns =>
                                   {
                                       columns.ConstantColumn(20, Unit.Millimetre);
                                       columns.ConstantColumn(120, Unit.Millimetre);
                                   });

                                   //table.Cell().Row(1).Column(1).BorderRight(0).BorderBottom(0).PaddingLeft(5).PaddingTop(3).AlignLeft().Image(Path.GetFullPath("~/ProfilePics/profile.png").Replace("~\\", ""));
                                   table.Cell().Row(1).Column(2).Border(0).BorderBottom(0).PaddingRight(0).PaddingTop(3).PaddingLeft(60).AlignCenter().AlignTop().Text(madharshaNameEn).FontColor(Colors.White).FontSize(12).Bold();
                                   table.Cell().Row(1).Column(2).Border(0).BorderBottom(0).PaddingRight(0).PaddingTop(23).PaddingLeft(60).AlignCenter().AlignTop().Text(madharshaNameAr).FontColor(Colors.White).FontSize(12).Bold();
                                   table.Cell().Row(1).Column(2).Border(0).BorderBottom(0).PaddingRight(0).PaddingTop(39).PaddingLeft(60).AlignCenter().AlignTop().Text(madharshaAddress).FontColor(Colors.White).FontSize(8).Bold();
                               });

                        #endregion

                        page.Content()
                                .Height(700)
                                .Border(0.0f)
                                .Border(0.5f).PaddingLeft(3).PaddingLeft(15).PaddingTop(2.5f)
                                .Table(table =>
                                {

                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(5, Unit.Millimetre);
                                        columns.ConstantColumn(25, Unit.Millimetre);
                                        columns.ConstantColumn(100, Unit.Millimetre);

                                    });

                                    uint i = 0;


                                    #region Headers

                                    i++;
                                    table.Cell().Row(i).Column(1).Border(0.0f).Background("#C0C0C0").AlignMiddle().AlignCenter().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("#");
                                    });
                                    table.Cell().Row(i).Column(2).Border(0.0f).Background("#C0C0C0").AlignMiddle().AlignCenter().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("ACCN");
                                    });
                                    table.Cell().Row(i).Column(3).Border(0.0f).Background("#C0C0C0").AlignMiddle().AlignCenter().PaddingTop(5).PaddingBottom(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(8));
                                        text.Span("CODE");
                                    });


                                    #endregion

                                    int _count = 0;
                                    DirectoryInfo d = new DirectoryInfo(CodePath);

                                    FileInfo[] Files = d.GetFiles("*.PNG");
                                    string str = "";

                                    foreach (FileInfo file in Files)
                                    {
                                        if (file.Name.ToString().EndsWith("FinalQRCode.png"))
                                            str = file.Name + "," + str;
                                    }
                                    string[] filenames = str.Split(",");

                                    foreach (var item in filenames)
                                    {
                                        if (item != string.Empty)
                                        {
                                            i++;
                                            table.Cell().Row(i).Column(1).Border(0.0f).AlignCenter().Text(text =>
                                            {
                                                text.DefaultTextStyle(x => x.FontSize(10).FontColor(Colors.Black));
                                                text.Span((_count + 1).ToString());
                                            });

                                            table.Cell().Row(i).Column(2).Border(0.0f).AlignCenter().Text(text =>
                                            {
                                                text.DefaultTextStyle(x => x.FontSize(10).FontColor(Colors.Black));
                                                text.Span("ACCN");
                                            });

                                            table.Cell().Row(i).Column(3).Border(0).AlignCenter().PaddingLeft(5).PaddingTop(10).Height(110).Width(100)
                                            .Image(CodePath + item).FitHeight().FitWidth();

                                            _count++;
                                        }
                                    }
                                });


                        page.Footer().DefaultTextStyle(x => x.FontSize(9))
                           .AlignLeft()
                           .BorderTop(0.5f)
                           .ContentFromLeftToRight()
                           .PaddingTop(5)
                           .Table(table =>
                           {
                               table.ColumnsDefinition(columns =>
                               {
                                   columns.ConstantColumn(30, Unit.Millimetre);
                                   columns.ConstantColumn(40, Unit.Millimetre);
                                   columns.ConstantColumn(5, Unit.Millimetre);
                                   columns.ConstantColumn(5, Unit.Millimetre);
                                   columns.ConstantColumn(20, Unit.Millimetre);
                                   columns.ConstantColumn(15, Unit.Millimetre);
                                   columns.ConstantColumn(15, Unit.Millimetre);
                                   columns.ConstantColumn(20, Unit.Millimetre);
                                   columns.ConstantColumn(40, Unit.Millimetre);
                               });

                               table.Cell().Row(1).Column(1).PaddingLeft(20).AlignRight().Text("Email:");
                               table.Cell().Row(1).Column(2).ColumnSpan(2).AlignLeft().Text("support@deltacare.com");

                               table.Cell().Row(1).Column(5).AlignRight().Text("Pin Code:");
                               table.Cell().Row(1).Column(6).AlignLeft().Text("51265");

                               table.Cell().Row(1).Column(7).AlignRight().Text("Phone:");
                               table.Cell().Row(1).Column(8).ColumnSpan(2).AlignLeft().Text("+966 11 3005415");

                               table.Cell().Row(1).Column(9).AlignRight().Text(x =>
                               {
                                   x.Span("Page ");
                                   x.CurrentPageNumber();
                                   x.Span(" Of ");
                                   x.TotalPages();
                               });

                           });
                    });

                });

                return document.GeneratePdf();
            }

            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
