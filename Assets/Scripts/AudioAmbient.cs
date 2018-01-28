using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAmbient : MonoBehaviour
{
    private AudioSource source;

    public AudioClip startupSound;
    public AudioClip idleSound;
    public AudioClip shutdownSound;

    public static AudioAmbient instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
        StartCoroutine(playPCSound());
    }

    IEnumerator playPCSound()
    {
        source.clip = startupSound;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        source.clip = idleSound;
        source.Play();
    }

    public void PlayPCShutdown()
    {
        source.clip = shutdownSound;
        source.Play();
    }
}
