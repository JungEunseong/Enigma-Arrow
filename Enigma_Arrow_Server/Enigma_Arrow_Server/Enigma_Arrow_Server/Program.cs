using System;
using System.Net;
using Server;
using ServerCore;
class Program
{
    static Listener _listener = new Listener();
    public static void Main(string[] args)
    {

        // DNS (Domain Name System)
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

        Console.WriteLine(endPoint.Address.ToString());
        Console.WriteLine(endPoint.Port.ToString());
        // DNS (Domain Name System)
        /*string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = IPAddress.Parse("10.82.17.113");
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);*/

        _listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
        Console.WriteLine("Listening...");

        //FlushRoom();
        //JobTimer.Instance.Push(FlushRoom);

        // TODO
        while (true)
        {
            MasterRoom.Instance.Update();
            List<GameRoom> rooms = RoomManager.Instance.Rooms;
            
            foreach(GameRoom room in rooms)
                room.Update();
            
        }
    }
}