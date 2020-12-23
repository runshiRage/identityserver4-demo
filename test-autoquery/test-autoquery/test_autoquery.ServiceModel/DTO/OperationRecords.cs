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
    [Route("/OperationRecords2")]
    public class OperationRecords2 : QueryData<OperationRecord>
    {
        public Guid? Id { get; set; }
        public Guid? BusinessId { get; set; }
        public string BusinessOrder { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
        public string Remark { get; set; }
    }

    [Route("/OperationRecords1")]
    public class OperationRecords1 : QueryDb<OperationRecord>
    {
        public Guid? Id { get; set; }
        public Guid? BusinessId { get; set; }
        public string BusinessOrder { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
        public string Remark { get; set; }
    }


    [Route("/rockstar-albums")]
    public class QueryRockstarAlbums : QueryData<RockstarAlbum>
    {
        public int? Id { get; set; }         // Primary Key | Range Key
        public int? RockstarId { get; set; } // Foreign key | Hash Key
        public string Name { get; set; }
        public string Genre { get; set; }
        public int[] IdBetween { get; set; }
    }
}
