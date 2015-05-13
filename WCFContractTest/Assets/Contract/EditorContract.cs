using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

[ServiceContract(Namespace = "http://www.kingsoft.com")]
interface IEditorInterface
{
    [OperationContract]
    void SayHi();
    [OperationContract]
    int add(int a, int b);
    [OperationContract]
    List<String> filter(List<String> src);
}