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
    private float _startVecY;       // ���� ���� ��ġ

    [Header("Fire")]
    [SerializeField] GameObject _bulletObj;
    [SerializeField] private float _FireDelayTime = 0.2f;       // ���� ������
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
    /// ���� ���� ������ 
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
        
    #region ����


    /// <summary>
    /// ���� �Է�
    /// </summary>
    public void AttackBtnClick()
    {
        if (_fireTimer > 0)      // ���� ��Ÿ�� ���϶� 
        {
            Debug.Log("���� ��Ÿ�� ���Դϴ�");
            return;
        }

        StartCoroutine(AttckCoroutine());
    }

    /// <summary>
    /// ���� üũ �ڷ�ƾ
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
    /// �Ѿ� �߻� (���� ����)
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
    /// pool���� ������Ʈ�� �־��� �� ���
    /// </summary>
    /// <param name="bullet"></param>
    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    /// <summary>
    /// ������Ʈ�� pool�� ������ �� ���
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
