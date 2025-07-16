using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image obj1;
    public Image obj2;
    public Image obj3;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(obj3, 1.0f);
        Destroy(obj2, 2.0f);
        Destroy(obj1, 3.0f);
        Time.timeScale = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (obj1 == null)
        {
            Time.timeScale = 1;
        }
    }
}
