using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacketHandler
{
    public static void S_MatchingResHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ServerSession Ssession = session as ServerSession;
        S_MatchingRes res = packet as S_MatchingRes;

        LobbyManager.Instance.OnMatching(res);
    }
    public static void S_SpawnResHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ServerSession Ssession = session as ServerSession;
        S_SpawnRes res = packet as S_SpawnRes;


        foreach(ObjectInfo info in res.Objects)
        {
            ObjectManager.Instance.Add(info, info.IsMine);
        }
    }
    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ServerSession Ssession = session as ServerSession;
        S_Despawn despawn = packet as S_Despawn;
        foreach (int id in despawn.ObjectId)
        {
            NetworkingObject obj = ObjectManager.Instance.FindById(id);
            UnityEngine.Object.Destroy(obj.gameObject);
        }

    }
    public static void S_MoveResHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ServerSession Ssession = session as ServerSession;
        S_MoveRes res = packet as S_MoveRes;
        NetworkingObject obj = ObjectManager.Instance.FindById(res.Id);
        
        if (obj == null) return;

        obj.SyncMove(new Vector3(res.Position.X,res.Position.Y,res.Position.Z));
    }
    public static void S_SetHpHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ServerSession Ssession = session as ServerSession;
        S_SetHp setHp = packet as S_SetHp;

        Player obj = ObjectManager.Instance.FindById(setHp.Id).GetComponent<Player>();
        obj.HP = setHp.Hp;
        
    }
    public static void S_SetOutcomeHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ServerSession Ssession = session as ServerSession;
        S_SetOutcome setOutcome = packet as S_SetOutcome;

        OutComeUI.Instance.ShowOutcomePanel(setOutcome.IsWin);
    }

}
