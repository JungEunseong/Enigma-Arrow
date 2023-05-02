using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] float _speed = 70;      // ÃÑ¾Ë ½ºÇÇµå

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
       rigid.AddForce(transform.forward * _speed,ForceMode.Impulse);
        
    }
}
