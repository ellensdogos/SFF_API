using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFF_API.Context;
using SFF_API.Models;

namespace SFF_API.Models
{
    public class Ticket
    {
        public string MovieTitle { get; set; }

        public string Location { get; set; }

        public DateTime Date { get; set; }
    }
}
