using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.MODELS.DTO
{
    public class PagedBaseRespondeDTO<T>
    {
        public int TotalRegisters { get; private set; }
        public List<T> Data { get; private set; }

        public PagedBaseRespondeDTO(int totalRegisters, List<T> data)
        {
            TotalRegisters = totalRegisters;
            Data = data;

        }

    }
}
