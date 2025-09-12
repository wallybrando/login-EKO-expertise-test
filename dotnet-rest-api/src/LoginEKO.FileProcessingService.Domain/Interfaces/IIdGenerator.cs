using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces
{
    public interface IIdGenerator
    {
        Guid GenerateId();
    }
}
