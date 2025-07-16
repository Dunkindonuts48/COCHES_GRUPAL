using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public int audioValue;

    public AudioSource audioSource;

    public AudioClip audClip1;
    public AudioClip audClip2;
    public AudioClip audClip3;
    public AudioClip audClip4;
    public AudioClip audClip5;
    public AudioClip audClip6;
    public AudioClip audClip7;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Audio");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
        switch (audioValue)
        {
            case 0:
                audioSource.Pause();
                break;
            case 1:
                audioSource.clip = audClip1;
                break;
            case 2:
                audioSource.clip = audClip2;
                break;
            case 3:
                audioSource.clip = audClip3;
                break;
            case 4:
                audioSource.clip = audClip4;
                break;
            case 5:
                audioSource.clip = audClip5;
                break;
            case 6:
                audioSource.clip = audClip6;
                break;
            case 7:
                audioSource.clip = audClip7;
                break;
            default:
                audioSource.Pause();
                break;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }  
    }
}
