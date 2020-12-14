using NLog.Fluent;
using ServiceStack;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.IO;
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

        public object Post(OperationRecords request)
        {
            if (this.Request.Files.Length > 0)
            {
                var uploadedFile = base.Request.Files[0];
                uploadedFile.SaveTo("");
            }
            return HttpResult.Redirect("/");
        }

        // Log all Request DTOs that implement IHasSessionId
        public override void OnBeforeExecute(object requestDto)
        {
            if (requestDto is IHasSessionId dtoSession)
            {
                Log.Debug($"{nameof(OnBeforeExecute)}: {dtoSession.SessionId}");
            }
        }

        //Return Response DTO Name in HTTP Header with Response
        public override object OnAfterExecute(object response)
        {
            return new HttpResult(response)
            {
                Headers = {
                ["X-Response"] = response.GetType().Name
            }
            };
        }

        //Return custom error with additional metadata
        public override Task<object> OnExceptionAsync(object requestDto, Exception ex)
        {
            var error = DtoUtils.CreateErrorResponse(requestDto, ex);
            if (error is IHttpError httpError)
            {
                var errorStatus = httpError.Response.GetResponseStatus();
                errorStatus.Meta = new Dictionary<string, string>
                {
                    ["InnerType"] = ex.InnerException?.GetType().Name
                };
            }
            return Task.FromResult(error);
        }


        //跳过反序列化或者是实体类绑定步骤，拿到请求原始数据
        public object Post(RawBytes request)
        {
            byte[] bytes = request.RequestStream.ReadFully();
            string text = bytes.FromUtf8Bytes(); //if text was sent
            return null;
        }

    }


    //跳过反序列化或者是实体类绑定步骤，拿到请求原始数据
    public class RawBytes : IRequiresRequestStream
    {
        /// <summary>
        /// The raw Http Request Input Stream
        /// </summary>
        public Stream RequestStream { get; set; }
        Stream IRequiresRequestStream.RequestStream { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
