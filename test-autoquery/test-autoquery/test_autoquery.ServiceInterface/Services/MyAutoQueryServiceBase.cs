using NSubstitute.Core;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_autoquery.ServiceInterface.Services
{
    public abstract class MyAutoQueryServiceBase : AutoQueryServiceBase
    {
        public override object Exec<From>(IQueryDb<From> dto)
        {
            var q = AutoQuery.CreateQuery(dto, Request.GetRequestParams(), base.Request);
            return AutoQuery.Execute(dto, q, base.Request);
        }

        public override object Exec<From, Into>(IQueryDb<From, Into> dto)
        {
            var q = AutoQuery.CreateQuery(dto, Request.GetRequestParams(), base.Request);
            return AutoQuery.Execute(dto, q, base.Request);
        }
    }
}
