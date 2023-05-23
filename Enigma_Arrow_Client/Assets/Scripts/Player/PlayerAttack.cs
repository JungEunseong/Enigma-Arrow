using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float _speed = 60;
    private float _startVecY;       // 공격 시작 위치

    [Header("Fire")]
    [SerializeField] GameObject _bulletObj;
    [SerializeField] private float _FireDelayTime = 0.2f;       // 공격 딜레이
    float _fireTimer = 0;

    [Header("Pool")]
    private IObjectPool<Bullet> _pool;

    public bool isTopPlayer;
    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnRelaseBullet, OnDestroyBullet, maxSize: 50);
    }

    void Start()
    {
        _startVecY = transform.eulerAngles.y;
    }

    void Update()
    {
        AttackMove();
    }

    /// <summary>
    /// 공격 방향 움직임 
    /// </summary>
    private void AttackMove()
    {
        Vector3 rotVec;
        if (isTopPlayer)
        {
             rotVec =
                        new Vector3(transform.rotation.x,
                        Mathf.PingPong(Time.time * _speed, 180) * -1 + _startVecY,
                        transform.rotation.z);
        }
        else
        {
            rotVec =
            new Vector3(transform.rotation.x,
            Mathf.PingPong(Time.time * _speed, 180) + _startVecY,   
            transform.rotation.z);
        }
        transform.rotation = Quaternion.Euler(rotVec);
    }
        
    #region 공격


    /// <summary>
    /// 공격 입력
    /// </summary>
    public void AttackBtnClick()
    {
        if (_fireTimer > 0)      // 공격 쿨타임 중일때 
        {
            Debug.Log("아직 쿨타임 중입니다");
            return;
        }

        StartCoroutine(AttckCoroutine());
    }

    /// <summary>
    /// 공격 체크 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator AttckCoroutine()
    {
        Fire();
        while (true)
        {
            _fireTimer += Time.deltaTime;
            if (_fireTimer > _FireDelayTime)
            {
                _fireTimer = 0;
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// 총알 발사 (실제 공격)
    /// </summary>
    private void Fire()
    {
        _fireTimer = 0;

        //Instantiate(_bulletObj,transform.position , transform.rotation);

        if (NetworkManager.Instance.isTestWithoutServer)
        {
            Bullet bullet = _pool.Get().GetComponent<Bullet>();
            bullet.InitBullet(transform, transform.rotation);
            bullet.OwnerId = ObjectManager.Instance.MyPlayer.Id;
        }
        else
        {
            C_AttackReq req = new C_AttackReq();
            req.Position = new Vec() { X = transform.position.x, Y = transform.position.y, Z = transform.position.z };
            req.Rotation = new Vec() { X = transform.rotation.x, Y = transform.rotation.y, Z = transform.rotation.z };
            Vector3 dir = transform.forward.normalized;
            
            req.Dir = new Vec() { X = dir.x, Y = dir.y, Z = dir.z };

            NetworkManager.Instance.Send(req);
        }
    }

    #endregion

    #region Pool

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletObj).GetComponent<Bullet>();
        bullet.SetManagedPool(_pool);
        return bullet;
    }

    /// <summary>
    /// pool에서 오브젝트를 넣어줄 때 사용
    /// </summary>
    /// <param name="bullet"></param>
    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    /// <summary>
    /// 오브젝트를 pool에 돌려줄 때 사용
    /// </summary>
    /// <param name="bullet"></param>
    private void OnRelaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    #endregion
}
