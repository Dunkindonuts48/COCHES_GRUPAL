using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCode : MonoBehaviour
{
    public Transform player;

    // lateUpdate because its called after update and fixed update so it will be called after the player has moved
    private void LateUpdate()
    {
        // folllows the player 

        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        // If the player rotate de map rotate in the same direction 
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f); 
    }
}
