using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameObject
{
    public ObjectType _objectType { get; protected set; }
    public int Id
    {
        get { return Info.Id; }
        set { Info.Id = value; }
    }

    public GameRoom JoinedRoom { get; set; }

    public ObjectInfo Info { get; set; } = new ObjectInfo();
}
