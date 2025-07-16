using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteController : MonoBehaviour
{
    private bool isMuted;

    // Start is called before the first frame update
    void Start()
    {
        isMuted = false;
    }

    // Update is called once per frame
    public void MutePressed()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
    }
}
