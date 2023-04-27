using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// ������ �Լ� 
    /// </summary>
    private void Move()
    {
        Vector3 _dir = Vector3.zero;        //�̵� ���� �ʱ�ȭ

        _dir.x = Input.acceleration.x;      
        _dir.y = Input.acceleration.y;

        if(_dir.sqrMagnitude > 1)
            _dir.Normalize();

        _dir *= Time.deltaTime;

        rigid.velocity = new Vector2(_dir.x * _speed , 0);


    }
}
