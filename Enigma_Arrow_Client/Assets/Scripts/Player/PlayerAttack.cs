using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float _speed = 30;   
    private float _startVecY;       // ���� ���� ��ġ

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
    /// ���� ���� ������ 
    /// </summary>
    private void AttackMove()
    {
        Vector3 rotVec =
        new Vector3(transform.rotation.x,
        Mathf.PingPong(Time.time * _speed, 180) + _startVecY,
        transform.rotation.z);
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
            if(_fireTimer >= _FireDelayTime)
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
        Debug.Log("��������");
    }

    #endregion
}
