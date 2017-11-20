using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    [RoutePrefix("api/salesline")]
    public class ApiTrnSalesLineController : ApiMethod.ApiMethodController
    {
        //**********************
        //LIST         SALESLINE
        //**********************
        [HttpGet, Route("list")]
        public List<Entities.TrnSalesLine> listSalesLine()
        {
            var salesLine = from d in db.TrnSalesLines
                        select new Entities.TrnSalesLine
                        {
                            Id = d.Id,
                            SalesId = d.SalesId,
                            ItemId = d.ItemId,
                            UnitId = d.UnitId,
                            Price = d.Price,
                            DiscountId = d.DiscountId,
                            DiscountRate = d.DiscountRate,
                            DiscountAmount = d.DiscountAmount,
                            NetPrice = d.NetPrice,
                            Quantity = d.Quantity,
                            Amount = d.Amount,
                            TaxId = d.TaxId,
                            TaxRate = d.TaxRate,
                            TaxAmount = d.TaxAmount,
                            SalesAccountId = d.SalesAccountId,
                            AssetAccountId = d.AssetAccountId,
                            CostAccountId = d.CostAccountId,
                            TaxAccountId = d.TaxAccountId,
                            SalesLineTimeStamp = d.SalesLineTimeStamp,
                            UserId = d.UserId,
                            Preparation = d.Preparation,
                        };
            return salesLine.ToList();
        }

        //**********************
        //ADD          SALESLINE
        //**********************
        [HttpPost, Route("post")]
        public Int32 postSalesLine()
        {
            try
            {

                Data.TrnSalesLine newSalesLine = new Data.TrnSalesLine();
                newSalesLine.SalesId = SalesId();
                newSalesLine.ItemId = ItemId();
                newSalesLine.UnitId = UnitId();
                newSalesLine.Price = 0;
                newSalesLine.DiscountId = DiscountId();
                newSalesLine.DiscountRate = 0;
                newSalesLine.DiscountAmount = 0;
                newSalesLine.NetPrice = 0;
                newSalesLine.Quantity = 0;
                newSalesLine.Amount = 0;
                newSalesLine.TaxId = TaxId();
                newSalesLine.TaxRate = 0;
                newSalesLine.TaxAmount = 0;
                newSalesLine.SalesAccountId = AccountId();
                newSalesLine.AssetAccountId = AccountId();
                newSalesLine.CostAccountId = AccountId();
                newSalesLine.TaxAccountId = AccountId();
                newSalesLine.SalesLineTimeStamp = DateTime.Today;
                newSalesLine.UserId = UserId();
                newSalesLine.Preparation = "n/a";
                db.TrnSalesLines.InsertOnSubmit(newSalesLine);
                db.SubmitChanges();

                return newSalesLine.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        //**********************
        //UPDATE       SALESLINE 
        //**********************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putSalesLine(String id, Entities.TrnSalesLine salesLine)
        {
            try
            {
                var salesLines = from d in db.TrnSalesLines where d.Id == Convert.ToInt32(id) select d;
                if (salesLines.Any())
                {
                    var updateSalesLine = salesLines.FirstOrDefault();
                    updateSalesLine.SalesId = salesLine.SalesId;
                    updateSalesLine.ItemId = salesLine.ItemId;
                    updateSalesLine.UnitId = salesLine.UnitId;
                    updateSalesLine.Price = salesLine.Price;
                    updateSalesLine.DiscountId = salesLine.DiscountId;
                    updateSalesLine.DiscountRate = salesLine.DiscountRate;
                    updateSalesLine.DiscountAmount = salesLine.DiscountAmount;
                    updateSalesLine.NetPrice = salesLine.NetPrice;
                    updateSalesLine.Quantity = salesLine.Quantity;
                    updateSalesLine.Amount = salesLine.Amount;
                    updateSalesLine.TaxId = salesLine.TaxId;
                    updateSalesLine.TaxRate = salesLine.TaxRate;
                    updateSalesLine.TaxAmount = salesLine.TaxAmount;
                    updateSalesLine.SalesAccountId = salesLine.SalesAccountId;
                    updateSalesLine.AssetAccountId = salesLine.AssetAccountId;
                    updateSalesLine.CostAccountId = salesLine.CostAccountId;
                    updateSalesLine.TaxAccountId = salesLine.TaxAccountId;
                    updateSalesLine.SalesLineTimeStamp = salesLine.SalesLineTimeStamp;
                    updateSalesLine.UserId = salesLine.UserId;
                    updateSalesLine.Preparation = salesLine.Preparation;
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
        //DELETE       SALESLINE
        //**********************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteSalesLine(String id)
        {
            try
            {
                var delete = from d in db.TrnSalesLines where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.TrnSalesLines.DeleteOnSubmit(delete.First());
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
