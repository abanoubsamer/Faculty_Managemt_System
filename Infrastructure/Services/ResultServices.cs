using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ResultServices
    {
        public bool Succed { get; set; }
        public bool Error { get; set; } = false;
        public string? MsgError { get; set; }
    }
}
