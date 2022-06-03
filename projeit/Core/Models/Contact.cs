using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Models
{
    public class Contact
    {

        public int ContactId { get; set; }

        public string? UserName { get; set; }

        public string? SendMail { get; set; }

        public string? Subject { get; set; }

        public string? Message { get; set; }

        public string? urgency { get; set; }

        public string? FinishingTime { get; set; }

        public string? priority { get; set; }

        public string? Statusu { get; set; }

        public string? data { get; set; }


    }
}
