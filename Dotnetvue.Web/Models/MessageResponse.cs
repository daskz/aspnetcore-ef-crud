using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dotnetvue.Web.Models
{
    public class MessageResponse 
    {
        public string Message { get; }

        public MessageResponse(string message)
        {
            Message = message;
        }
    }
}
