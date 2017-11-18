using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiMethod
{
    public class ApiMstMethodController : ApiController
    {
        //*************
        // DATA CONTEXT
        //*************
        public Data.posDBDataContext db = new Data.posDBDataContext();

        //*************
        //LIST   METHOD
        //*************

        //USER
        public Int32 UserId()
        {
            var userId = from d in db.MstUsers select d.Id;
            return userId.FirstOrDefault();
        }

        //ACCOUNT
        public Int32 AccountId()
        {
            var accountId = from d in db.MstAccounts select d.Id;
            return accountId.FirstOrDefault();
        }

        //CUSTOMER
        public Int32 CustomerId()
        {
            var customerId = from d in db.MstCustomers select d.Id;
            return customerId.FirstOrDefault();
        }

        //DISCOUNT
        public Int32 DiscountId()
        {
            var discountId = from d in db.MstDiscounts select d.Id;
            return discountId.FirstOrDefault();
        }

        //ITEM
        public Int32 ItemId()
        {
            var itemId = from d in db.MstItems select d.Id;
            return itemId.FirstOrDefault();
        }

        //ITEM GROUP
        public Int32 ItemGroupId()
        {
            var itemGroupId = from d in db.MstItemGroups select d.Id;
            return itemGroupId.FirstOrDefault();
        }

        //COMPONENT ITEM
        public Int32 ComponentItemId()
        {
            var componentId = from d in db.MstItemComponents select d.Id;
            return componentId.FirstOrDefault();
        }


        //PAYTYPE
        public Int32 PayTypeId()
        {
            var payTypeId = from d in db.MstPayTypes select d.Id;
            return payTypeId.FirstOrDefault();
        }

        //PERIOD
        public Int32 PeriodId()
        {
            var periodId = from d in db.MstPeriods select d.Id;
            return periodId.FirstOrDefault();
        }

        //SUPPLIER
        public Int32 SupplierId()
        {
            var supplierId = from d in db.MstSuppliers select d.Id;
            return supplierId.FirstOrDefault();
        }

        //TABLE
        public Int32 TableId()
        {
            var tableId = from d in db.MstTables select d.Id;
            return tableId.FirstOrDefault();
        }

        //TAX
        public Int32 TaxId()
        {
            var taxId = from d in db.MstTaxes select d.Id;
            return taxId.FirstOrDefault();
        }
        //TERM
        public Int32 TermId()
        {
            var termId = from d in db.MstTerms select d.Id;
            return termId.FirstOrDefault();
        }
        //TERMINAL
        public Int32 TerminalId()
        {
            var terminalId = from d in db.MstTerminals select d.Id;
            return terminalId.FirstOrDefault();
        }
        //UNIT
        public Int32 UnitId()
        {
            var unitId = from d in db.MstUnits select d.Id;
            return unitId.FirstOrDefault();
        }
    }
}
