using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;  

public class Bullet : NetworkingObject
{
    public int OwnerId;
    Rigidbody rigid;
    [SerializeField] float _speed = 70;      // 총알 스피드
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

    private void Update()
    {
        SyncMove(destPos);
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
        _managedPool.Release(this);     //인스턴스를 다시 pool로 반환
    }

    private void OnTriggerEnter(Collider other)
    {
        if (NetworkManager.Instance.isTestWithoutServer)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //본인이 아닌 경우에만 player
                //함수 호출 
                Player player = other.gameObject.GetComponent<Player>();

                if (player.Id != OwnerId)
                    player.Hit(_damage);
            }
        }
    }

    

    public override void SyncMove(Vector3 pos)
    {
        destPos = pos;
        transform.position = Vector3.Lerp(transform.position, destPos, (1000 / 60 / _speed) * Time.deltaTime);
    }
}
