using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoundManager : MonoBehaviour
{
    public static WeaponSoundManager instance;

    //public AudioClip clip; ȿ���� ���� �������� ��ũ��Ʈ�� Ŭ�� �Ҵ�
    //WeaponSoundManager.instance.SFXPlay("�̸�", clip); �ش� �ڵ带 ���ݰ� ���� ��ġ�� ����.

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
