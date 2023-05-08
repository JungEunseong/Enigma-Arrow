using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;  

public class Bullet : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] float _speed = 70;      // �Ѿ� ���ǵ�
    [SerializeField] int _damage;

    [Header("Pool")]
    private IObjectPool<Bullet> _managedPool;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }
    private void OnEnable()
    {
    }

    public void InitBullet(Transform trans, Quaternion ro)
    {
        rigid.velocity = Vector3.zero;

        transform.position = trans.position;
        transform.rotation = ro;

        StartCoroutine(DestroyCoroutine());
        rigid.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2f);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //������ �ƴ� ��쿡�� player Hit �Լ� ȣ�� 
            Player player = other.gameObject.GetComponent<Player>();
            player.Hit(_damage);
        }
    }
}
