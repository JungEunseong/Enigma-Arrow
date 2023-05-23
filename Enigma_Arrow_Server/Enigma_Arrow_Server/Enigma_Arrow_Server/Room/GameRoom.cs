using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server;
using Server.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class GameRoom : JobSerializer
{
    public int RoomId { get; set; }
    public ObjectManager _objectManager { get; set; } = new ObjectManager();

    Dictionary<int, ClientSession> _sessions = new Dictionary<int, ClientSession>();
    Dictionary<int, Player> _players = new Dictionary<int, Player>();
    Dictionary<int, Bullet> _bullets = new Dictionary<int, Bullet>();

    public List<Player> Players{
        get
        {
            return _players.Values.ToList();
        }
    }

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
        gameObject.JoinedRoom = this;


        //TODO: 플레이어 소환
        if (gameObject.Info.Type == ObjectType.Player) {
            Player spawnPlayer = gameObject as Player;

            if (PlayerCount == 0)
                gameObject.Info.Position = new Vec() { X = 0, Y = 1, Z = 0 };
            else
                gameObject.Info.Position = new Vec() { X = 0, Y = 1, Z = -33 };

            {
                _players.Add(spawnPlayer.Id, spawnPlayer);
                spawnPlayer.Session.MyPlayer = spawnPlayer;
                S_SpawnRes res = new S_SpawnRes();

                ObjectInfo info = spawnPlayer.Info;
                info.IsMine = true;

                res.Objects.Add(info);
                
                spawnPlayer.Session.Send(res);

                info.IsMine = false;
            }

            //TODO: 원래 있던 플레이어 소환
            {
                S_SpawnRes res = new S_SpawnRes();
                
                foreach (Player player in _players.Values)
                    if(player != spawnPlayer) res.Objects.Add(player.Info);
                
                spawnPlayer.Session.Send(res);
            }
        }
        else if(gameObject.Info.Type == ObjectType.Bullet)
        {
            _bullets.Add(gameObject.Id, gameObject as Bullet);
            gameObject.JoinedRoom = this;
        }

        //TODO: 원래 있던 플레이어에게 플레이어 소환
        {
            S_SpawnRes res = new S_SpawnRes();
            res.Objects.Add(gameObject.Info);

            foreach (Player player in _players.Values)
                if (player.Id != gameObject.Id) player.Session.Send(res);
        }

    }

    public void LeaveGame(int gameObjectId)
    {
        GameObject obj = _objectManager.FindById(gameObjectId);

        if (obj == null) return;

        S_Despawn despawn = new S_Despawn();
        despawn.ObjectId.Add(obj.Id);

        Broadcast(despawn);

        if (obj._objectType == ObjectType.Player)
            _players.Remove(gameObjectId);
        else if(obj._objectType == ObjectType.Bullet)
            _bullets.Remove(gameObjectId);

        
    }

    public void HandleMove(ClientSession session,C_MoveReq req)
    {
        session.MyPlayer._moveDir = req.InputDir;
    }

    public void HandleAttack(ClientSession session,C_AttackReq req)
    {
        session.MyPlayer.Attack(req);

    }

    public void ExitRoom(ClientSession session)
    {
        _sessions.Remove(session.SessionId);
        MasterRoom.Instance.Enter(session);
        session.MyPlayer = null;
        session.JoinedRoom = null;
    }

    public void Broadcast(IMessage packet)
    {
        foreach (ClientSession session in _sessions.Values)
            session.Send(packet);
    }

    public void Update()
    {
        foreach (Player player in _players.Values)
            player.Update();
        foreach (Bullet bullet in _bullets.Values) 
            bullet.Update();

        Flush();
    }
}
