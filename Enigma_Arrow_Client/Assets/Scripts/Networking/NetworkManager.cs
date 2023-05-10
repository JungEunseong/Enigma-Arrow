using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Google.Protobuf;

public class NetworkManager : MonoBehaviour
{

    static NetworkManager _instance;
    public static NetworkManager Instance { get { return _instance; } }
    ServerSession _session = new ServerSession();

    public bool isHost;
    public string hostIp;

    public bool isConnecting { get; set; }
    private void Start()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Init();
    }

    public void Send(IMessage packet)
    {
        _session.Send(packet);
    }

    public void Init()
    {
        // DNS (Domain Name System)
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        //IPAddress ipAddr = IPAddress.Parse(remoteIp);
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

        Connector connector = new Connector();

        connector.Connect(endPoint,
            () => { return _session; },
            1);
    }

    public void Matching()
    {
        _session.Disconnect();
        _session = null;
        if (isHost)
        {
            IPAddress ipAddr = IPAddress.Parse(hostIp);
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 5555);

            Listener listener = new Listener();
            listener.Init(endPoint, () => { return _session = new ServerSession(); });
            Debug.Log("Listening...");
        }
        else
        {
            IPAddress ipAddr = IPAddress.Parse(hostIp);
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 5555);

            Connector connector = new Connector();

            connector.Connect(endPoint,
                () => { return _session = new ServerSession(); },
                1);
        } 
    }

    public void Update()
    {
        List<PacketMessage> list = PacketQueue.Instance.PopAll();
        foreach (PacketMessage packet in list)
        {
            /*Action<PacketSession, IMessage> handler = PacketManager.Instance.GetPacketHandler(packet.Id);
            if (handler != null)
                handler.Invoke(_session, packet.Message);*/
        }
    }


}
