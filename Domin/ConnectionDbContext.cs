using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin
{
    public static class ConnectionDbContext
    {
        public static string ConnectionString { get; } =
          "Data Source=BIBOSAMER12\\MSSQLSERVER02;Initial Catalog=EF_Test;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";
    }
}
