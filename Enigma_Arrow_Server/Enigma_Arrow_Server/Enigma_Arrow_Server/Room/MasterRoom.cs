using Google.Protobuf.Protocol;
using Server;
using Server.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MasterRoom : JobSerializer
{
    public static MasterRoom Instance { get; set; } = new MasterRoom();
    
    public List<ClientSession> _sessions = new List<ClientSession>();
    public List<ClientSession> _matchingSessions= new List<ClientSession>();


    public void Update()
    {
        Push(Match);

        Flush();
    }


    public void Enter(ClientSession session)
    {
        if (session == null)
            return;

        _sessions.Add(session);
        Console.WriteLine("MasterRoom Enter Failed");

    }


    public void Leave(ClientSession session)
    {
        if(session == null) return;

        if (_sessions.Remove(session))
        {
            return;
        }
        Console.WriteLine("MasterRoom Leave Failed");
    }

    public void HandleMatching(ClientSession session)
    {
        if (session == null)  return;

        _matchingSessions.Add(session);
    }

    /*public void Match()
    {
        if (_matchingSessions.Count < 2) return;


        // TODO: 서로의 ip를 건내주고 연결끊기
        ClientSession firstSession = _matchingSessions[0];
        ClientSession secondSession = _matchingSessions[1];

        _matchingSessions.Remove(firstSession);
        _matchingSessions.Remove(secondSession);

        // 알리기
        S_MatchingRes matchingRes = new S_MatchingRes();
        matchingRes.HostIP = firstSession.strIP;
        matchingRes.IsHost = true;
        firstSession.Send(matchingRes);
        
        matchingRes.IsHost = false;
        secondSession.Send(matchingRes);

    }*/
}
