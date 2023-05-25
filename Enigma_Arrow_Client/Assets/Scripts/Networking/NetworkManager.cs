using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Google.Protobuf;
using Google.Protobuf.Protocol;

public class NetworkManager : Singleton<NetworkManager>
{

    ServerSession _session = new ServerSession();

    public bool isTestWithoutServer;

    public bool isConnecting { get; set; }

    public UserInfo userInfo = new UserInfo();

    public UserInfo enemyInfo = new UserInfo(); // 적팀 유저 인포
    private void Start()
    {
        if(isConnecting == false)
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
    public void Update()
    {
        List<PacketMessage> list = PacketQueue.Instance.PopAll();
        foreach (PacketMessage packet in list)
        {
           Action<PacketSession, IMessage> handler = PacketManager.Instance.GetPacketHandler(packet.Id);
            if (handler != null)
                handler.Invoke(_session, packet.Message);
        }
    }


}
