using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MyWcfService
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract(Name = "GetNumber")]
        [WebInvoke(Method = "GET",
            UriTemplate = "GetNumber",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        string GetNumber();

        [OperationContract(Name = "GetSampleMethod")]
        [WebInvoke(Method = "GET",
            UriTemplate = "GetSampleMethod/inputStr/{name}",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetSampleMethod(string name);

        [OperationContract(Name = "GetPersons")]
        [WebInvoke(Method = "GET",
            UriTemplate = "GetPersons",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        List<Person> GetPersons();

        // POST doesn't work with $.getJSON
        [OperationContract(Name = "PostSampleMethod")]
        [WebInvoke(Method = "POST",
         UriTemplate = "PostSampleMethod",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        string PostSampleMethod(string data);
    }
}