using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [RoutePrefix("api/sales")]
    public class ApiTrnSalesController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST             SALES
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnSales> listSales()
        {
            var sales = from d in db.TrnSales
                              select new Entities.TrnSales
                              {
                                  Id = d.Id,
                                  PeriodId = d.PeriodId,
                                  SalesDate = d.SalesDate,
                                  SalesNumber = d.SalesNumber,
                                  ManualInvoiceNumber = d.ManualInvoiceNumber,
                                  Amount = d.Amount,
                                  TableId = d.TableId,
                                  CustomerId = d.CustomerId,
                                  AccountId = d.AccountId,
                                  TermId = d.TermId,
                                  DiscountId = d.DiscountId,
                                  SeniorCitizenId = d.SeniorCitizenId,
                                  SeniorCitizenName = d.SeniorCitizenName,
                                  SeniorCitizenAge = d.SeniorCitizenAge,
                                  Remarks = d.Remarks,
                                  SalesAgent = d.SalesAgent,
                                  TerminalId = d.TerminalId,
                                  PreparedBy = d.PreparedBy,
                                  CheckedBy = d.CheckedBy,
                                  ApprovedBy = d.ApprovedBy,
                                  IsLocked = d.IsLocked,
                                  IsCancelled = d.IsCancelled,
                                  PaidAmount = d.PaidAmount,
                                  CreditAmount = d.CreditAmount,
                                  DebitAmount = d.DebitAmount,
                                  BalanceAmount = d.BalanceAmount,
                                  EntryUserId = d.EntryUserId,
                                  EntryDateTime = d.EntryDateTime,
                                  UpdateUserId = d.UpdateUserId,
                                  UpdateDateTime = d.UpdateDateTime,
                                  Pax = d.Pax,
                              };
            return sales.ToList();
        }

        //**********************
        //ADD              SALES
        //**********************
        [HttpPost, Route("post")]
        public Int32 postSales()
        {
            try
            {

                Data.TrnSale newSales = new Data.TrnSale();
                newSales.PeriodId = PeriodId();
                newSales.SalesDate = DateTime.Today;
                newSales.SalesNumber = "n/a";
                newSales.ManualInvoiceNumber = "n/a";
                newSales.Amount = 0;
                newSales.TableId = TableId();
                newSales.CustomerId = CustomerId();
                newSales.AccountId = AccountId();
                newSales.TermId = TermId();
                newSales.DiscountId = StockInId();
                newSales.SeniorCitizenId = "n/a";
                newSales.SeniorCitizenName = "n/a";
                newSales.SeniorCitizenAge = 0;
                newSales.Remarks = "n/a";
                newSales.SalesAgent = UserId();
                newSales.TerminalId = TerminalId();
                newSales.PreparedBy = UserId();
                newSales.CheckedBy = UserId();
                newSales.ApprovedBy = UserId();
                newSales.IsLocked = false;
                newSales.IsCancelled = false;
                newSales.PaidAmount = 0;
                newSales.CreditAmount = 0;
                newSales.DebitAmount = 0;
                newSales.BalanceAmount = 0;
                newSales.EntryUserId = UserId();
                newSales.EntryDateTime = DateTime.Today;
                newSales.UpdateUserId = UserId();
                newSales.UpdateDateTime = DateTime.Today;
                newSales.Pax = 0;
                db.TrnSales.InsertOnSubmit(newSales);
                db.SubmitChanges();

                return newSales.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE           SALES 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putSales(String id, Entities.TrnSales sale)
        {
            try
            {
                var sales = from d in db.TrnSales where d.Id == Convert.ToInt32(id) select d;
                if (sales.Any())
                {
                    var updateSales = sales.FirstOrDefault();
                    updateSales.PeriodId = sale.PeriodId;
                    updateSales.SalesDate = sale.SalesDate;
                    updateSales.SalesNumber = sale.SalesNumber;
                    updateSales.ManualInvoiceNumber = sale.ManualInvoiceNumber;
                    updateSales.Amount = sale.Amount;
                    updateSales.TableId = sale.TableId;
                    updateSales.CustomerId = sale.CustomerId;
                    updateSales.AccountId = sale.AccountId;
                    updateSales.TermId = sale.TermId;
                    updateSales.DiscountId = sale.DiscountId;
                    updateSales.SeniorCitizenId = sale.SeniorCitizenId;
                    updateSales.SeniorCitizenName = sale.SeniorCitizenName;
                    updateSales.SeniorCitizenAge = sale.SeniorCitizenAge;
                    updateSales.Remarks = sale.Remarks;
                    updateSales.SalesAgent = sale.SalesAgent;
                    updateSales.TerminalId = sale.TerminalId;
                    updateSales.PreparedBy = sale.PreparedBy;
                    updateSales.CheckedBy = sale.CheckedBy;
                    updateSales.ApprovedBy = sale.ApprovedBy;
                    updateSales.IsLocked = true;
                    updateSales.IsCancelled = sale.IsCancelled;
                    updateSales.PaidAmount = sale.PaidAmount;
                    updateSales.CreditAmount = sale.CreditAmount;
                    updateSales.DebitAmount = sale.DebitAmount;
                    updateSales.BalanceAmount = sale.BalanceAmount;
                    updateSales.EntryUserId = UserId();
                    updateSales.EntryDateTime = DateTime.Today;
                    updateSales.UpdateUserId = UserId();
                    updateSales.UpdateDateTime = DateTime.Today;
                    updateSales.Pax = sale.Pax;
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                // Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        //**********************
        //DELETE           SALES
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteSales(String id)
        {
            try
            {
                var delete = from d in db.TrnSales where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnSales.DeleteOnSubmit(delete.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
