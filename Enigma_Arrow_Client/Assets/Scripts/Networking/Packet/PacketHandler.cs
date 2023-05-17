using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class PacketHandler
{
    public static void S_MatchingResHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ServerSession Ssession = session as ServerSession;
        S_MatchingRes res = packet as S_MatchingRes;

        SceneManager.LoadScene("MultiTestScene");
    }
    public static void S_SpawnResHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ServerSession Ssession = session as ServerSession;
        S_SpawnRes res = packet as S_SpawnRes;


        foreach(ObjectInfo info in res.Objects)
        {
            ObjectManager.Instance.Add(info);
        }
    }
    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ServerSession Ssession = session as ServerSession;
    }
}
