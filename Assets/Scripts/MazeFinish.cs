using System;
using UnityEngine;

public class MazeFinish : MonoBehaviour
{
    DoorControl doorCon;
    bool hasFinished = false;
    void Start()
    {
        doorCon = GetComponent<DoorControl>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MazeNormal") && !hasFinished)
        {
            doorCon.OpenDoor();
            hasFinished = true;
        }
        else if (other.gameObject.CompareTag("MazeSpecial"))
        {
            //do something for last puzzle
        }
    }
}
