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
            Font fontArial10Bold = new Font("Arial", 10, FontStyle.Bold);
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

            // ===============
            // System Settings
            // ===============
            String companyNameText = "ACME Corporation";
            graphics.DrawString(companyNameText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(companyNameText, fontArial10Regular).Height;

            String companyCityText = "Cebu City";
            graphics.DrawString(companyCityText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(companyCityText, fontArial10Regular).Height;

            String operatedByText = "Operated by: " + companyNameText;
            graphics.DrawString(operatedByText, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(operatedByText, fontArial10Regular).Height;

            String TINTextLabel = "TIN No.:";
            graphics.DrawString(TINTextLabel, fontArial10Regular, drawBrush, new RectangleF(x, y, 120.0F, height), drawFormatLeft);

            String TINTextData = "000-254-342-002V";
            graphics.DrawString(TINTextData, fontArial10Regular, drawBrush, new RectangleF(95, y, width, height), drawFormatLeft);
            y += graphics.MeasureString(TINTextLabel, fontArial10Regular).Height;

            String permitNoTextLabel = "Permit No.:";
            graphics.DrawString(permitNoTextLabel, fontArial10Regular, drawBrush, new RectangleF(x, y, 120.0F, height), drawFormatLeft);

            String permitNoTextData = "222-222-222";
            graphics.DrawString(permitNoTextData, fontArial10Regular, drawBrush, new RectangleF(95, y, width, height), drawFormatLeft);
            y += graphics.MeasureString(permitNoTextLabel, fontArial10Regular).Height;

            String accrdNoTextLabel = "Accrd. No.:";
            graphics.DrawString(accrdNoTextLabel, fontArial10Regular, drawBrush, new RectangleF(x, y, 120.0F, height), drawFormatLeft);

            String accrdNoTextData = "08226148138700037524583";
            graphics.DrawString(accrdNoTextData, fontArial10Regular, drawBrush, new RectangleF(95, y, width, height), drawFormatLeft);
            y += graphics.MeasureString(accrdNoTextLabel, fontArial10Regular).Height;

            String serialNoTextLabel = "Serial. No.:";
            graphics.DrawString(serialNoTextLabel, fontArial10Regular, drawBrush, new RectangleF(x, y, 120.0F, height), drawFormatLeft);

            String serialNoTextData = "111-111-111";
            graphics.DrawString(serialNoTextData, fontArial10Regular, drawBrush, new RectangleF(95, y, width, height), drawFormatLeft);
            y += graphics.MeasureString(serialNoTextLabel, fontArial10Regular).Height;

            String machineNoTextLabel = "Machine No.:";
            graphics.DrawString(machineNoTextLabel, fontArial10Regular, drawBrush, new RectangleF(x, y, 120.0F, height), drawFormatLeft);

            String machineNoTextData = "15090116441020835";
            graphics.DrawString(machineNoTextData, fontArial10Regular, drawBrush, new RectangleF(95, y, width, height), drawFormatLeft);
            y += graphics.MeasureString(machineNoTextLabel, fontArial10Regular).Height;

            String officialReceiptText = "O F F I C I A L   R E C E I P T";
            graphics.DrawString(officialReceiptText, fontArial11Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(officialReceiptText, fontArial11Regular).Height;

            // ============
            // Sales Header
            // ============
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

                // ====================
                // Line Points Settings
                // ====================
                Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) - 9);
                Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) - 9);

                graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                String itemLabel = "ITEM";
                graphics.DrawString(itemLabel, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                String amountLabel = "AMOUNT";
                graphics.DrawString(amountLabel, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(itemLabel, fontArial10Regular).Height + 5.0F;

                // ==========
                // Sales Item
                // ==========
                var salesLines = from d in db.TrnSalesLines
                                 where d.SalesId == sales.FirstOrDefault().Id
                                 select d;

                Decimal totalSales = 0, totalDiscount = 0, cash = 0, change = 0;
                if (salesLines.Any())
                {
                    foreach (var salesLine in salesLines)
                    {
                        String itemData = salesLine.MstItem.ItemDescription + "\n" + "**" + salesLine.Quantity.ToString("#,##0.00") + " @ " + salesLine.Price.ToString("#,##0.00") + " - " + salesLine.MstTax.Tax;
                        RectangleF itemDataRectangle = new RectangleF
                        {
                            X = x,
                            Y = y,
                            Size = new Size(150, ((int)graphics.MeasureString(itemData, fontArial10Regular, 150, StringFormat.GenericTypographic).Height))
                        };
                        graphics.DrawString(itemData, fontArial10Regular, Brushes.Black, itemDataRectangle, drawFormatLeft);

                        String itemAmountData = salesLine.Amount.ToString("#,##0.00");
                        graphics.DrawString(itemAmountData, fontArial10Regular, drawBrush, new RectangleF(x, y, 270.0F, height), drawFormatRight);
                        y += itemDataRectangle.Size.Height + 5.0F;

                        totalSales += salesLine.Amount;
                        totalDiscount += salesLine.DiscountAmount;
                    }
                }

                Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);

                graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                String totalSalesLabel = "\nTOTAL SALES";
                graphics.DrawString(totalSalesLabel, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                String totalSalesAmount = "\n" + totalSales.ToString("#,##0.00");
                graphics.DrawString(totalSalesAmount, fontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalSalesAmount, fontArial10Bold).Height;

                String totalDiscountLabel = "TOTAL DISCOUNT";
                graphics.DrawString(totalDiscountLabel, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                String totalDiscountAmount = totalDiscount.ToString("#,##0.00");
                graphics.DrawString(totalDiscountAmount, fontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalDiscountAmount, fontArial10Bold).Height;

                String cashLabel = "CASH";
                graphics.DrawString(cashLabel, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                String cashAmount = cash.ToString("#,##0.00");
                graphics.DrawString(cashAmount, fontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(cashAmount, fontArial10Bold).Height;

                String changelabel = "CHANGE";
                graphics.DrawString(changelabel, fontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);

                String changeAmount = change.ToString("#,##0.00");
                graphics.DrawString(changeAmount, fontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(changeAmount, fontArial10Bold).Height;
            }
        }
    }
}
