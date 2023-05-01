using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.ExceptionServices;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }


    /// <summary>
    /// 움직임 함수 
    /// </summary>
    private void Move()
    {
        Vector3 _dir = Vector3.zero;        //이동 방향 초기화

#if (Mobile)
        _dir.x = Input.acceleration.x;      
        _dir.y = Input.acceleration.y;

        if(_dir.sqrMagnitude > 1)
            _dir.Normalize();

#else
        float h = Input.GetAxis("Horizontal");
        _dir.x = h;
#endif

        rigid.velocity = new Vector2(_dir.x * _speed, 0);


    }
}
