using Google.Protobuf.Protocol;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Player : GameObject
{
    public ClientSession Session { get; set; }
    public Player()
    {
        _objectType = ObjectType.Player;
    }

    public Vec _moveDir = new Vec() { X = 0, Y = 0, Z = 0 };

    public override void Attack()
    {

    }

    long _nextMoveTick = 0;

    public override void Update()
    {
        MoveUpdate();
    }
    public override void MoveUpdate()
    {
        if (_nextMoveTick > Environment.TickCount64)
            return;

        int moveTick = (int)(1000 / Speed);
        _nextMoveTick = Environment.TickCount64 + moveTick;

        Move(_moveDir);

        S_MoveRes res = new S_MoveRes();
        res.Id = Id;
        res.Position = Pos;

        if(_moveDir.X != 0 || _moveDir.Y != 0 || _moveDir.Z != 0)
            Session.JoinedRoom.Broadcast(res);
    }
    public override void Move(Vec dir)
    {
        // Vector 크기 구하기
        float vectorSize = MathF.Sqrt(MathF.Pow(dir.X, 2) + MathF.Pow(dir.Y, 2) + MathF.Pow(dir.Z, 2));

        if (vectorSize == 0) return;
        // Vector 정규화
        dir.X /= vectorSize;
        dir.Y /= vectorSize;
        dir.Z /= vectorSize;

        // 이동
        Pos.X += dir.X;
        Pos.Y += dir.Y;
        Pos.Z += dir.Z;
    }   
}