using ServiceStack;
using ServiceStack.Web;
using System;
using System.IO;
using System.Web;
using System.Xml.Serialization;

namespace test_autoquery.plugins
{
    public class XmlSerializerFormat : IPlugin
    {
        public static void Serialize(IRequest req, object response, Stream stream)
        {
            var serializer = new XmlSerializer(response.GetType());
            serializer.Serialize(stream, response);
        }

        public static object Deserialize(Type type, Stream stream)
        {
            var serializer = new XmlSerializer(type.GetType());
            var obj = (Type)serializer.Deserialize(stream);
            return obj;
        }

        public void Register(IAppHost appHost)
        {
            appHost.ContentTypes.Register(MimeTypes.Xml, Serialize, Deserialize);
        }
    }
}
