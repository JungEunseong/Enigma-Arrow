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
    public ObjectManager _objectManager { get; set; } = new ObjectManager();

    Dictionary<int,ClientSession> _sessions= new Dictionary<int,ClientSession>();
    Dictionary<int,Player> _players = new Dictionary<int,Player>();


    public int PlayerCount { get { return _players.Count; } }
    /// <summary>
    /// 매칭 후 게임 방에 들어 올 때 실행하는 함수
    /// </summary>
    /// <param name="session">들어오는 세션</param>
    public void EnterRoom(ClientSession session)
    {
        _sessions.Add(session.SessionId, session);
        session.JoinedRoom = this;
    }

    /// <summary>
    /// 게임 입장 후 게임 시작 시 플레이어 스폰할 때 사용하는 함수
    /// </summary>
    public void EnterGame(GameObject gameObject)
    {

        //TODO: 플레이어 소환
        if (gameObject.Info.Type == ObjectType.Player) {
            Player spawnPlayer = gameObject as Player;
            {
                _players.Add(spawnPlayer.Id, spawnPlayer);

                S_SpawnRes res = new S_SpawnRes();
                res.Objects.Add(spawnPlayer.Info);

                foreach (ClientSession session in _sessions.Values)
                    session.Send(res);
            }
            //TODO: 원래 있던 플레이어 소환
            {
                S_SpawnRes res = new S_SpawnRes();

                foreach (Player player in _players.Values)
                    if(player != spawnPlayer) res.Objects.Add(player.Info);
                
                spawnPlayer.Session.Send(res);
            }
        }

        //TODO: 원래 있던 플레이어에게 플레이어 소환
        {
            S_SpawnRes res = new S_SpawnRes();
            res.Objects.Add(gameObject.Info);

            foreach (Player player in _players.Values)
                if (player.Id != gameObject.Id) player.Session.Send(res);
        }

    }

    public void ExitGame(ClientSession session)
    {
        _sessions.Remove(session.SessionId);
    }
}
