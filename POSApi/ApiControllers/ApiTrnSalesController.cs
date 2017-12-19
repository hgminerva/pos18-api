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
        public HttpResponseMessage postSales(Entities.TrnSales objSales)
        {
            try
            {
                Data.TrnSale newSales = new Data.TrnSale();
                newSales.PeriodId = objSales.PeriodId;
                newSales.SalesDate = objSales.SalesDate;
                newSales.SalesNumber = objSales.SalesNumber;
                newSales.ManualInvoiceNumber = objSales.ManualInvoiceNumber;
                newSales.Amount = objSales.Amount;
                newSales.TableId = objSales.TableId;
                newSales.CustomerId = objSales.CustomerId;
                newSales.AccountId = objSales.AccountId;
                newSales.TermId = objSales.TermId;
                newSales.DiscountId = objSales.DiscountId;
                newSales.SeniorCitizenId = objSales.SeniorCitizenId;
                newSales.SeniorCitizenName = objSales.SeniorCitizenName;
                newSales.SeniorCitizenAge = objSales.SeniorCitizenAge;
                newSales.Remarks = objSales.Remarks;
                newSales.SalesAgent = objSales.SalesAgent;
                newSales.TerminalId = objSales.TerminalId;
                newSales.PreparedBy = objSales.PreparedBy;
                newSales.CheckedBy = objSales.CheckedBy;
                newSales.ApprovedBy = objSales.ApprovedBy;
                newSales.IsLocked = objSales.IsLocked;
                newSales.IsCancelled = objSales.IsCancelled;
                newSales.PaidAmount = objSales.PaidAmount;
                newSales.CreditAmount = objSales.CreditAmount;
                newSales.DebitAmount = objSales.DebitAmount;
                newSales.BalanceAmount = objSales.BalanceAmount;
                newSales.EntryUserId = UserId();
                newSales.EntryDateTime = DateTime.Today;
                newSales.UpdateUserId = UserId();
                newSales.UpdateDateTime = DateTime.Today;
                newSales.Pax = objSales.Pax;
                db.TrnSales.InsertOnSubmit(newSales);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK, newSales.Id);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
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
