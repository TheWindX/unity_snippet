using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System;
using System.Linq;


[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
public class ImplEditorInterface : IEditorInterface
{

    public void SayHi()
    {
        Console.WriteLine("from unity request of SayHi!");
    }

    public int add(int a, int b)
    {
        Console.WriteLine("from unity request of add:" + a + b);
        return a + b;
    }

    public List<string> filter(List<string> src)
    {
        Console.WriteLine("from unity request of src!");
        return src.Where(item => !item.Contains("err")).ToList();
    }
}


public class test
{

	// Use this for initialization

	public static void test1 () {
        Console.WriteLine("start");
        try
        {
            //server
            string address = "http://localhost:8046/editor";
            ServiceHost serviceHost = new ServiceHost(typeof(ImplEditorInterface));
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                serviceHost.AddServiceEndpoint(typeof(IEditorInterface), binding, address);
                serviceHost.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("wait");
            var l = Console.ReadLine();
            Console.WriteLine("request");
            //client
            string address1 = "http://localhost:8045/unity";
            BasicHttpBinding binding1 = new BasicHttpBinding();
            EndpointAddress ep = new EndpointAddress(address1);
            IUnityInterface channel = ChannelFactory<IUnityInterface>.CreateChannel(binding1, ep);
            channel.SayHi();
            Console.WriteLine(string.Format(" 2 + 2 = {0}", channel.add(2, 2)));
            Console.WriteLine(string.Format("frome unity {0}", channel.filter(new List<string> { "ua", "ub", "uerr c" })[0]));
            Console.WriteLine("wait");
            Console.ReadLine();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
	}
}
