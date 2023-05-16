using Google.Protobuf.Protocol;
using Server;
using Server.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameRoom : JobSerializer
{
    public int RoomId { get; set; }
    public ObjectManager objectManager { get; set; } = new ObjectManager();

    Dictionary<int,ClientSession> _sessions= new Dictionary<int,ClientSession>();
    Dictionary<int,Player> _players = new Dictionary<int,Player>();


    public int PlayerCount { get { return _players.Count; } }
    /// <summary>
    /// 매칭 후 게임 방에 들어 올 때 실행하는 함수
    /// </summary>
    /// <param name="session">들어오는 세션</param>
    public void EnterGame(ClientSession session)
    {
        _sessions.Add(session.SessionId, session);
        session.JoinedRoom = this;
    }

    /// <summary>
    /// 게임 입장 후 게임 시작 시 플레이어 스폰할 때 사용하는 함수
    /// </summary>
    public void SpawnPlayer(ObjectInfo info)
    {
        //TODO: 플레이어 소환, 원래 있던 플레이어 소환, 원래 있던 플레이어에게 플레이어 소환

    }

    public void ExitGame(ClientSession session)
    {
        _sessions.Remove(session.SessionId);
    }
}
