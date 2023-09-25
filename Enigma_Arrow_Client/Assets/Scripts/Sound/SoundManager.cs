using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get { return _instance; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SFXPlay(AudioClip clip, string name)
    {
        GameObject go = new GameObject(name);
        AudioSource a =  go.AddComponent<AudioSource>(); 
        a.clip = clip;
        Destroy(a, clip.length);
    }
}
