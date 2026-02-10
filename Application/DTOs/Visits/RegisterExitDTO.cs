using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Visits
{
    public class RegisterExitDTO
    {
        public Guid VisitId { get; set; }
        public string? Code { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
