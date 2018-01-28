using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInput : MonoBehaviour
{
    private AudioSource source;

    public AudioClip[] keySounds;
    public AudioClip[] positiveSounds;
    public AudioClip[] negativeSounds;
    public AudioClip[] feedbackSounds;

    public static AudioInput instance = null;

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
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.anyKeyDown)
        {
            source.PlayOneShot(keySounds[Random.Range(0, keySounds.Length - 1)]);
        }
	}

    public void PlaySoundPositive()
    {
        source.PlayOneShot(negativeSounds[Random.Range(0, negativeSounds.Length - 1)]);
    }

    public void PlaySoundNegative()
    {
        source.PlayOneShot(feedbackSounds[Random.Range(0, feedbackSounds.Length - 1)]);
    }

    public void PlaySoundFeedback()
    {
        source.PlayOneShot(feedbackSounds[Random.Range(0, feedbackSounds.Length - 1)]);
    }

}
