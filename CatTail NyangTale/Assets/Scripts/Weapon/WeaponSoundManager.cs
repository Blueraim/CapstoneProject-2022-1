using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoundManager : MonoBehaviour
{
    public static WeaponSoundManager instance;

    //public AudioClip clip; 효과음 넣을 프리팹의 스크립트에 클립 할당
    //WeaponSoundManager.instance.SFXPlay("이름", clip); 해당 코드를 공격과 같은 위치에 삽입.

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(go, clip.length);
    }
}
