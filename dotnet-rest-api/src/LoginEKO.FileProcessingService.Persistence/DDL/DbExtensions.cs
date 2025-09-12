using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Persistence.DDL
{
    public static class DbExtensions
    {
        public const string CreateUUIDModule = "CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";";
    }
}
