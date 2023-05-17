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
        
        if (joinedRoom.PlayerCount == 0)
        {
            player.Info.Position = new Vec() { X = 0, Y = 1 ,Z = -3};
        }
        else
        {
            player.Info.Position = new Vec() { X = 0, Y = 1, Z = 3 };
        }

        joinedRoom.EnterGame(player);


    }
}
