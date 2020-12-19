using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    public class ActionResponse
    {
        public string ErrorMsg { get; set; }

        public string SuccessMsg { get; set; }
        public bool IsSuccess { get; set; }
		public string test {get;set;}

        public dynamic obj { get; set; }
       
    }
}