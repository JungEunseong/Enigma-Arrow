using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float _speed = 30;   
    private float _startVecY;       // 공격 시작 위치

    [Header("Fire")]
    [SerializeField] GameObject _bulletObj;
    [SerializeField] private float _FireDelayTime = 0.5f;
    float _fireTimer = 0;

    void Start()
    {
        _startVecY = transform.eulerAngles.y;
    }

    void Update()
    {
        AttackMove();

        if(Input.GetMouseButtonDown(0))     // test
        {
            AttackBtnClick();
        }
    }

    /// <summary>
    /// 공격 방향 움직임 
    /// </summary>
    private void AttackMove()
    {
        Vector3 rotVec =
        new Vector3(transform.rotation.x,
        Mathf.PingPong(Time.time * _speed, 180) + _startVecY,
        transform.rotation.z);
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
            if(_fireTimer >= _FireDelayTime)
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
        Debug.Log("ㄹㄴㅇㄹ");
    }

    #endregion
}
