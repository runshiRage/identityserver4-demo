using Amazon.DynamoDBv2;
using Funq;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Api.Swagger;
using ServiceStack.Aws.DynamoDb;
using ServiceStack.Data;
using ServiceStack.Extensions;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using test_autoquery.plugins;
using test_autoquery.ServiceInterface;
using test_autoquery.ServiceInterface.Services;
using test_autoquery.ServiceModel.Entity;

namespace test_autoquery
{
    //VS.NET Template Info: https://servicestack.net/vs-templates/EmptyAspNet
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("test_autoquery", typeof(MyServices).Assembly) { }


        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());

            //数据库连接
            var dbFactory = new OrmLiteConnectionFactory("Data Source = 192.168.0.188;Initial Catalog =QRPay_ALL_DEV;User Id = sa;Password = Atbms1q2w3e4r;", SqlServerDialect.Provider);
            container.Register<IDbConnectionFactory>(dbFactory);
            
            //全局json响应  根据类型匹配
            JsConfig.Init(new Config
            {
                TextCase = TextCase.SnakeCase,
                ExcludeDefaultValues = true,
            });
            JsConfig<Guid>.SerializeFn = guid => guid.ToString("D");
            JsConfig<TimeSpan>.SerializeFn = time =>
                (time.Ticks < 0 ? "-" : "") + time.ToString("hh':'mm':'ss'.'fffffff");


            //可以将请求和响应做一个缓存
            this.PreRequestFilters.Add((httpReq, httpRes) => {
                httpReq.UseBufferedStream = true;  // Buffer Request Input
                httpRes.UseBufferedStream = true;  // Buffer Response Output
            });

            Plugins.Add(new AutoQueryFeature { MaxLimit = 100 });

            //开启Swgger
            Plugins.Add(new SwaggerFeature());

        }

    }
}