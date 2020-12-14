using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_autoquery.ServiceModel.Entity;

namespace test_autoquery.ServiceModel.DTO
{
    [Route("/OperationRecords")]
    public class OperationRecords : QueryDb<OperationRecord>
    {
        public Guid? Id { get; set; }
        public Guid? BusinessId { get; set; }
        public string BusinessOrder { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
        public string Remark { get; set; }
    }
}
