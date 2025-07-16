using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollisionData : MonoBehaviour
{
    public int type = 2; //Type 1 will be the flag checkpoint. Type 2 will be for the normal checkpoints
    public int ID;
    private bool active = false;
    private GameObject nextCheckpoint;
    public static GameObject[] checkpointArray;
    public int numberOfCheckpoints = 0;

    void Start()
    {

        checkpointArray = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject cp in checkpointArray)
        {
            numberOfCheckpoints++;

        }
        Debug.Log("number of checkpoints " + numberOfCheckpoints);

        for (int i = 0; i < numberOfCheckpoints - 1; i++)
        {
            checkpointArray[i].GetComponent<CheckpointCollisionData>().ID = i;
            checkpointArray[i].GetComponent<CheckpointCollisionData>().SetNextCheckpoint(checkpointArray[i + 1].gameObject);
        }

        checkpointArray[numberOfCheckpoints - 1].GetComponent<CheckpointCollisionData>().ID = numberOfCheckpoints - 1;
        checkpointArray[numberOfCheckpoints - 1].GetComponent<CheckpointCollisionData>().SetNextCheckpoint(checkpointArray[0].gameObject);
    }

    public void SetNextCheckpoint(GameObject nextCheck)
    {
        nextCheckpoint = nextCheck;
    }

    public void setLastCrossedAsActive(bool crossed)
    {
        active = crossed;
    }
}
