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
                ////pd.PrinterSettings = ps;
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
            Font fontArial10Regular = new Font("Arial", 10, FontStyle.Regular);

            // =================
            // Margins and Sizes
            // =================
            float x = 10, y = 5;
            float width = 270.0F, height = 0F;

            // ==============
            // Brush Settings
            // ==============
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // ==================
            // Alignment Settings
            // ==================
            StringFormat drawFormatCenter = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat drawFormatLeft = new StringFormat { Alignment = StringAlignment.Near };
            StringFormat drawFormatRight = new StringFormat { Alignment = StringAlignment.Far };

            // ======================
            // Data / Receipt Content
            // ======================
            var sales = from d in db.TrnSales
                        where d.Id == Convert.ToInt32(salesId)
                        select d;

            if (sales.Any())
            {
                String salesNumberText = sales.FirstOrDefault().SalesNumber;
                ev.Graphics.DrawString(salesNumberText, fontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += ev.Graphics.MeasureString(salesNumberText, fontArial12Bold).Height;

                String salesDateText = sales.FirstOrDefault().SalesDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
                ev.Graphics.DrawString(salesDateText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += ev.Graphics.MeasureString(salesDateText, fontArial10Regular).Height;
            }
        }
    }
}
