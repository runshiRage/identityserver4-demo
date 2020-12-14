using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_autoquery.ServiceModel.DTO;

namespace test_autoquery.ServiceInterface.Services
{
    public class OperationRecordService : AutoQueryServiceBase
    {
        public object Get(OperationRecords request)
        {
            var query = AutoQuery.CreateQuery(request, Request);
            var result = AutoQuery.Execute(request, query);
            return result;
        }
    }
}
