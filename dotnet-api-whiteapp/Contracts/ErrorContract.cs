﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api_swagger.Contracts
{
    public class ErrorContract
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
