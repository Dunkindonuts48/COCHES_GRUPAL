using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishCode : MonoBehaviour
{
    private GameObject audioSource;

    public AudioMixer audioMixer;

    public TextMeshProUGUI PosText;

    private bool isMuted;

    public void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Audio");

    }
    public void Update()
    {
        Cursor.visible = true;
        PosText.text = PlayerPrefs.GetInt("finalPos") + " Place!";
    }
    public void StartGame()
    {
        SceneManager.LoadScene("TrackSelector");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void SelectPista1()
    {
        SceneManager.LoadScene("Track");
        SceneManager.MoveGameObjectToScene(audioSource, SceneManager.GetSceneByName("Track"));
    }

    public void SelectPista2()
    {
        SceneManager.LoadScene("Track2");
        SceneManager.MoveGameObjectToScene(audioSource, SceneManager.GetSceneByName("Track2"));
    }

    public void SetMusic(int musicIndex)
    {
        audioSource.GetComponent<AudioManager>().audioValue = musicIndex;
    }

    public void SetVolume(float volumeValue)
    {
        audioMixer.SetFloat("volume", volumeValue);
    }
}
