using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    public Dictionary<int, NetworkingObject> _objects = new Dictionary<int, NetworkingObject>();
    public Player MyPlayer { get; set; }
    public int _idCounter = 0;

    public GameObject Add(ObjectInfo info, bool isMine = false)
    {
        
        if (info.Type == ObjectType.Player)
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/Player");
            NetworkingObject NO = Instantiate(obj, new Vector3(info.Position.X, info.Position.Y, info.Position.Z), Quaternion.identity).GetComponent<NetworkingObject>();

            Player player = NO.GetComponent<Player>();
            player.destPos = new Vector3(info.Position.X, info.Position.Y, info.Position.Z);

            if (info.Position.Z == 0) player.IsTopPlayer = true;
            if (isMine)
            {
                NO.isMine = isMine;
                MyPlayer = player;
                Camera.main.transform.parent = MyPlayer.transform;
            }
            else
            {
                player.RemotePlayerInit();
            }

            NO.Id = info.Id;

            _objects.Add(NO.Id, NO);
            
            return NO.gameObject;
        }
        else if(info.Type == ObjectType.Bullet)
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/Bullet");
            NetworkingObject NO = Instantiate(obj, new Vector3(info.Position.X, info.Position.Y, info.Position.Z), Quaternion.identity).GetComponent<NetworkingObject>();
            NO.Id = info.Id;
            NO.destPos = new Vector3(info.Position.X, info.Position.Y, info.Position.Z);
            _objects.Add(NO.Id, NO);
        }


        return null;
    }


    public NetworkingObject FindById(int id)
    {
        NetworkingObject obj = null;
        if(_objects.TryGetValue(id,out obj))
        {
            return obj;
        }

        return null;
    }
}
