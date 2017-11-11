using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.Entities
{
    public class MstCustomer
    {
        public Int32 Id { get; set; }
        public String Customer { get; set; }
        public String Address { get; set; }
        public String ContactPerson { get; set; }
        public String ContactNumber { get; set; }
        public Decimal CreditLimit { get; set; }
        public Int32 TermId { get; set; }
        public String TIN { get; set; }
        public Boolean WithReward { get; set; }
    }
}