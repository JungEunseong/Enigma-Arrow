using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkingObject : MonoBehaviour
{
    public int Id { get; set; }
    public bool isMine;
    public Vector3 destPos;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SyncronizationSetting()
    {

    }

    public virtual void SyncMove(Vector3 pos)
    {

    }
}
