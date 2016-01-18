using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace HelloService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHelloService" in both code and config file together.
    [ServiceContract(Namespace = "http://PragimTech.com/ServiceVersion1")]

    public interface IHelloService
    {
        [OperationContract]
        string GetMessage(string firstname, string lastname, string Mobile);

        [OperationContract]
        string ProcessData(string firstname, string lastname, string Mobile, string monthlypayment);

        [OperationContract]
        string ProcessDataAdd(string firstname, string lastname, string email, string Mobile, string debtlevel, string monthlypayment, string website);

        [OperationContract]
        bool UrlIsValid(string url);


    }
}
