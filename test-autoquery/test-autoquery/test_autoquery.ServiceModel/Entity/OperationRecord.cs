using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_autoquery.ServiceModel.Entity
{
    public class OperationRecord 
    {
        public Guid? Id { get; set; }
        public Guid? BusinessId { get; set; }
        public string BusinessOrder { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
        public string Remark { get; set; }
    }

    public class RockstarAlbum 
    {
        public int? Id { get; set; }         // Primary Key | Range Key
        public int? RockstarId { get; set; } // Foreign key | Hash Key
        public string Name { get; set; }
        public string Genre { get; set; }
        public int[] IdBetween { get; set; }
    }
}
