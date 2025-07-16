

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameAndUIController : MonoBehaviour
{

    //Time related
    public TextMeshProUGUI timeText;
    private float seconds = 0;
    private float minutes = 0;

    //lap and position related
    public TextMeshProUGUI lapText;
    public TextMeshProUGUI posText;
    public RacerCollision playerInfo;
    public int laps = 0;
    public static GameObject[] racerArray;
    private int numberOfRacers;




    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "TIME: 0:00";
        lapText.text = "LAP: 0";
        posText.text = "POSITION: 0/7";
        //playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<RacerCollision>();
        GameObject[] auxArray = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] auxArray2 = GameObject.FindGameObjectsWithTag("Racer");

        racerArray = new GameObject[auxArray.Length + auxArray2.Length];

        for (int i = 0; i < racerArray.Length; i++)
        {
            if (i == 0)
                racerArray[i] = auxArray[i];
            else
                racerArray[i] = auxArray2[i-1];

            numberOfRacers++;
        }
    }

    void Update()
    {
        //Position related
        updateRacerPositions();

        //Time related
            seconds += Time.deltaTime;

            if (seconds >= 60) 
            {
                seconds = 0;
                minutes += 1;
            }

            else timeText.text = "Time: " + minutes + ":" + seconds.ToString("00");

        lapText.text = "LAP: " + playerInfo.currentLap.ToString();
        posText.text = "POSITION: " + playerInfo.currentPos.ToString() + "/7";
    }

    private void updateRacerPositions()
    {

        //put the racers in order of position
        GameObject auxObject;
        bool ordered = false;

        do
        {
            ordered = true;
            for (int j = 0; j < numberOfRacers - 1; j++)
            {
                if (racerArray[j].GetComponent<RacerCollision>().positionPoints < racerArray[j + 1].GetComponent<RacerCollision>().positionPoints)
                {
                    auxObject = racerArray[j + 1];
                    racerArray[j + 1] = racerArray[j];
                    racerArray[j] = auxObject;
                    ordered = false;
                }

            }
        } while (ordered == false);

        //change the position variable in each racer
        for (int i = 0; i < numberOfRacers; i++)
        {
            racerArray[i].GetComponent<RacerCollision>().currentPos = i + 1;
        }

    }
}
