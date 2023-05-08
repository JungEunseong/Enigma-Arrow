using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    [SerializeField] bool isDontDestroy;

    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if(_instance == null)
                {
                    GameObject go = new GameObject(nameof(T));
                    _instance = go.AddComponent<T>();

                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {

        if (_instance == null)
        {
            _instance = this as T;
            if (isDontDestroy)
            {
                DontDestroyOnLoad(this);
            }

        }
        else
            Destroy(gameObject);
    }
}
