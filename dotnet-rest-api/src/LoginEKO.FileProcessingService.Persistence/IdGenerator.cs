using LoginEKO.FileProcessingService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Persistence
{
    public class IdGenerator : IIdGenerator
    {
        public Guid GenerateId()
        {
            return Guid.NewGuid();
        }
    }
}
