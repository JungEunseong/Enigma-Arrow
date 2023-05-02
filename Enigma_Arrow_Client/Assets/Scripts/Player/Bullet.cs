using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;  

public class Bullet : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] float _speed = 70;      // �Ѿ� ���ǵ�

    [Header("Pool")]
    private IObjectPool<Bullet> _managedPool;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        StartCoroutine(DestroyCoroutine());
       rigid.AddForce(transform.forward * _speed,ForceMode.Impulse);
        
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3f);
        DestroyBullet();
    }

    /// <summary>
    /// Pool Set
    /// </summary>
    /// <param name="pool"></param>
    public void SetManagedPool(IObjectPool<Bullet> pool)
    {
        _managedPool= pool;
    }

    public void DestroyBullet()
    {
        _managedPool.Release(this);     //�ν��Ͻ��� �ٽ� pool�� ��ȯ
    }
}
