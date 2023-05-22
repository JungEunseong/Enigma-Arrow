using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PacketHandler
{
    public static void C_MatchingReqHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ClientSession CSession = session as ClientSession;
        C_MatchingReq req = packet as C_MatchingReq;

        MasterRoom.Instance.HandleMatching(CSession, req.IsCancel);
    }
    public static void C_SpawnplayerReqHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ClientSession CSession = session as ClientSession;
        C_SpawnplayerReq req = packet as C_SpawnplayerReq;

        GameRoom joinedRoom = CSession.JoinedRoom;
        Player player = joinedRoom._objectManager.Add<Player>();
        player.Session = CSession;
        player.Info.Type = ObjectType.Player;
        
        joinedRoom.Push(joinedRoom.EnterGame,player);


    }
    public static void C_MoveReqHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ClientSession CSession = session as ClientSession;
        C_MoveReq req = packet as C_MoveReq;


        CSession.JoinedRoom.Push(CSession.JoinedRoom.HandleMove,CSession, req);
    }
    
    public static void C_AttackReqHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ClientSession CSession = session as ClientSession;
        C_AttackReq req = packet as C_AttackReq;
        GameRoom room = CSession.JoinedRoom;

        room.Push(room.HandleAttack,CSession, req);
    }

    /*
    public static void C_MoveReqHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ClientSession CSession = session as ClientSession;
    }*/
}
