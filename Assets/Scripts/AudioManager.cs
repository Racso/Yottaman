using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{

    public float MusicVolume = 1;
    public float SFXVolume = 1;

    public AudioClip BGMusic;

    public AudioClip Laser;
    public AudioClip Shoot;
    public AudioClip Die1;
    public AudioClip Die2;
    public AudioClip ExplosionSmall;
    public AudioClip ExplosionHard;
    public AudioClip Shield;
    public AudioClip ExplosionGlobal;
    
    void Start()
    {

    }

    void Update()
    {

    }

    public void PlayMusic()
    {
        var go = new GameObject("BGMUSIC");
        go.transform.parent = transform.parent;
        var source = go.AddComponent<AudioSource>();
        source.clip = BGMusic;
        source.loop = true;
        source.Play();
        source.volume = MusicVolume;
    }
    public void PlaySFX(AudioClip effect)
    {
        var go = new GameObject("SFX");
        go.transform.parent = transform.parent;
        var source = go.AddComponent<AudioSource>();
        source.clip = effect;
        source.Play();
        source.volume = SFXVolume;
        StartCoroutine(Routine_DestroySFX(source));
    }

    private IEnumerator Routine_DestroySFX(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length * 1.2f);
        Destroy(source.gameObject);
    }
}
