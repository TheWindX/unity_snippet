using UnityEngine;
using System.Collections;
using System.ServiceModel;
using System.ServiceModel.Description;
using System;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
public class ImplUnityInterface : IUnityInterface
{

    public void SayHi()
    {
        Debug.Log("from editor request of SayHi!");
    }

    public int add(int a, int b)
    {
        Debug.Log("from editor request of add:" + a + b);
        return a + b;
    }

    public List<string> filter(List<string> src)
    {
        Debug.Log("from editor request of src!");
        return src.Where(item => !item.Contains("err")).ToList();
    }
}


public class test : MonoBehaviour {

	// Use this for initialization
    void Awake()
    {   
    }
	void Start () {
        Debug.Log("start");
        try
        {
            //server
            string address = "http://localhost:8045/unity";
            ServiceHost serviceHost = new ServiceHost(typeof(ImplUnityInterface));
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                serviceHost.AddServiceEndpoint(typeof(IUnityInterface), binding, address);
                serviceHost.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
	}


    void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Space(256);
        //GUILayout.Label("from editor:" + mTextShow);
        if (GUILayout.Button("Say to editor"))
        {
            //client
            string address1 = "http://localhost:8046/editor";
            BasicHttpBinding binding1 = new BasicHttpBinding();
            EndpointAddress ep = new EndpointAddress(address1);
            IEditorInterface channel = ChannelFactory<IEditorInterface>.CreateChannel(binding1, ep);
            channel.SayHi();
            Debug.Log(string.Format(" 2 + 2 = {0}", channel.add(2, 2)));
            Debug.Log(string.Format("{0}", channel.filter(new List<string> { "a", "b", "err c" })[0]));
        }
        GUILayout.EndVertical();
    }
}
