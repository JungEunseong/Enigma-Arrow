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
}
