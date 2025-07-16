using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RacerCollision : MonoBehaviour
{
    public int nextCheckpoint { get; private set; }
    public int currentCheckpoint { get; private set; }
    public int currentLap { get; private set; }
    public int currentPos;
    public float positionPoints { get; private set; }


    // Start is called before the first frame update
    void Start()
    {

        nextCheckpoint = 1;//1
        currentCheckpoint = 0; //always one less than the next checkpoint
        currentLap = 1;//0
        currentPos = 0;
        positionPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePositionPoints();
        if (currentLap == 4 && gameObject.CompareTag("Player"))
        {   
            PlayerPrefs.SetInt("finalPos", currentPos);
            SceneManager.LoadScene("Finish");
        }
    }

    private void OnTriggerEnter(Collider other) //Doesnt check if collision is with checkpoint since it the only object with isTrigger
    {
        if (other.GetComponent<CheckpointCollisionData>().ID == nextCheckpoint)
        {
            if (other.GetComponent<CheckpointCollisionData>().type == 1)
            {
                currentLap++;
            }
            nextCheckpoint++;
            currentCheckpoint++;
            nextCheckpoint %= other.GetComponent<CheckpointCollisionData>().numberOfCheckpoints;
            currentCheckpoint %= other.GetComponent<CheckpointCollisionData>().numberOfCheckpoints;
            activateCheckpoint(other);
        }
    }

    private void activateCheckpoint(Collider other)
    {
        foreach (GameObject cp in CheckpointCollisionData.checkpointArray)
        {
            cp.GetComponent<CheckpointCollisionData>().setLastCrossedAsActive(false);
        }
        other.GetComponent<CheckpointCollisionData>().setLastCrossedAsActive(true);
    }

    private void CalculatePositionPoints() //Lap = 10000 pts  * lap || chkp = 100 pts  * number of chkp || 1 pts * distance to current chkp magnitude
    {
        positionPoints = 10000 * currentLap;
        positionPoints += 100 * currentCheckpoint;
        Vector3 distanceFromCheckpoint = transform.position - CheckpointCollisionData.checkpointArray[currentCheckpoint].transform.position;
        distanceFromCheckpoint.y = 0;
        positionPoints += distanceFromCheckpoint.magnitude;
        Debug.Log("dist magnitude" + distanceFromCheckpoint.magnitude);
        Debug.Log("posPoints " + positionPoints);
    }
}

