using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] TMP_Text _tempText;        // 움직임 임시 확인 txt

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
        #endif

        _dir *= Time.deltaTime;

        //temp Check Text
        _tempText.text = _dir.x.ToString();


        rigid.velocity = new Vector2(_dir.x * _speed , 0);


    }
}
