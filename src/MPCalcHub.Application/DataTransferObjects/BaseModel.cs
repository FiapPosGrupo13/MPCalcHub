using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPCalcHub.Application.DataTransferObjects
{
    public class BaseModel : Identifier
    {
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public bool Removed { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? RemovedAt { get; set; }
        public Guid? RemovedBy { get; set; }

        public BaseModel() : base() { }
    }
}