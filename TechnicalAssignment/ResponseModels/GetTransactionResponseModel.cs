﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAssignment.ResponseModels
{
    public class GetTransactionResponseModel
    {
        public string Id { get; set; }

        public string PaymentDetails { get; set; }

        public string Status { get; set; }
    }
}
