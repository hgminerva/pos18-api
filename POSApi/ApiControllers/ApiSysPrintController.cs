using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [RoutePrefix("api/print")]
    public class ApiSysPrintController : ApiController
    {
        // ============
        // Data Context
        // ============
        public Data.posDBDataContext db = new Data.posDBDataContext();

        // ================
        // Global Variables
        // ================
        private Int32 salesId = 0;

        // =============
        // Print Receipt
        // =============
        [HttpGet, Route("sales/{id}")]
        public void PrintSales(String id)
        {
            try
            {
                salesId = Convert.ToInt32(id);
                PrinterSettings ps = new PrinterSettings
                {
                    PrinterName = "EPSON TM-T81 Receipt"
                };

                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(PrintSalesReceipt);
                pd.PrinterSettings = ps;
                pd.Print();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        // ==========
        // Print Page
        // ==========
        public void PrintSalesReceipt(object sender, PrintPageEventArgs ev)
        {
            // =============
            // Font Settings
            // =============
            Font fontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
            Font fontArial12Regular = new Font("Arial", 12, FontStyle.Regular);
            Font fontArial11Bold = new Font("Arial", 11, FontStyle.Bold);
            Font fontArial11Regular = new Font("Arial", 11, FontStyle.Regular);
            Font fontArial10Regular = new Font("Arial", 10, FontStyle.Regular);

            // ==================
            // Alignment Settings
            // ==================
            StringFormat drawFormatCenter = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat drawFormatLeft = new StringFormat { Alignment = StringAlignment.Near };
            StringFormat drawFormatRight = new StringFormat { Alignment = StringAlignment.Far };

            float x = 10, y = 5;
            float width = 270.0F, height = 0F;

            // ==============
            // Tools Settings
            // ==============
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Pen blackPen = new Pen(Color.Black, 1);
            Graphics graphics = ev.Graphics;

            // =================================
            // Data / Receipt Content (Graphics)
            // =================================
            String companyNameText = "ACME Corporation";
            graphics.DrawString(companyNameText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(companyNameText, fontArial10Regular).Height;

            String companyCityText = "Cebu City";
            graphics.DrawString(companyCityText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(companyCityText, fontArial10Regular).Height;

            String operatedByText = "Operated by: " + companyNameText;
            graphics.DrawString(operatedByText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(operatedByText, fontArial10Regular).Height;

            String TINText = "TIN No.: 000-254-342-002V";
            graphics.DrawString(TINText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += graphics.MeasureString(TINText, fontArial10Regular).Height;

            String permitNoText = "Permit No.: 222-222-222";
            graphics.DrawString(permitNoText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += graphics.MeasureString(permitNoText, fontArial10Regular).Height;

            String accrdNoText = "Accrd. No.: 08226148138700037524583";
            graphics.DrawString(accrdNoText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += graphics.MeasureString(accrdNoText, fontArial10Regular).Height;

            String serialNoText = "Serial. No.: 111-111-111";
            graphics.DrawString(serialNoText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += graphics.MeasureString(serialNoText, fontArial10Regular).Height;

            String machineNoText = "Machine. No.: 15090116441020835";
            graphics.DrawString(machineNoText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            y += graphics.MeasureString(machineNoText, fontArial10Regular).Height;

            String officialReceiptText = "O F F I C I A L   R E C E I P T";
            graphics.DrawString(officialReceiptText, fontArial11Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(officialReceiptText, fontArial11Regular).Height;

            var sales = from d in db.TrnSales
                        where d.Id == Convert.ToInt32(salesId)
                        select d;

            if (sales.Any())
            {
                String salesNumberText = sales.FirstOrDefault().SalesNumber;
                graphics.DrawString(salesNumberText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(salesNumberText, fontArial10Regular).Height;

                String salesDateText = sales.FirstOrDefault().SalesDate.ToString("MM-dd-yyyy h:mm:ss tt", CultureInfo.InvariantCulture) + "\n\n";
                graphics.DrawString(salesDateText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(salesDateText, fontArial10Regular).Height;
            }

            // ====================
            // Line Points Settings
            // ====================
            Point firstPoint = new Point(0, 200);
            Point secondPoint = new Point(500, 200);

            graphics.DrawLine(blackPen, firstPoint, secondPoint);
        }
    }
}
