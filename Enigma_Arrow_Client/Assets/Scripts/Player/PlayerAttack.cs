using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject bulletObj;
    [SerializeField] private float FireDelayTime;
    float fireTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackBtnClick()
    {
        if(fireTimer >= 0)      // 공격 쿨타임 중일때 
        {

        }
    }

    /// <summary>
    /// 총알 발사 
    /// </summary>
    private void Fire()
    {

    }
}
