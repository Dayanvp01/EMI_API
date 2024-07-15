using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMI_API.Commons.DTOs
{
    public class CreateEmployeeDTO
    {
        public string Name { get; set; } = null!;
        public decimal Salary { get; set; }
        /// <summary>
        /// 1 For Regular, 2 For Manager
        /// </summary>
        public int CurrentPosition { get; set; }
    }
}
