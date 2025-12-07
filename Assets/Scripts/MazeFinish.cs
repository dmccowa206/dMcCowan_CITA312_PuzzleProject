using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MazeFinish : MonoBehaviour
{
    DoorControl doorCon;
    bool hasFinished = false;
    new Collider collider;
    GameManager gm;
    void Start()
    {
        doorCon = GetComponent<DoorControl>();
        collider = gameObject.GetComponent<SphereCollider>();
        gm = FindAnyObjectByType<GameManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MazeNormal") && !hasFinished)
        {
            OnFinish();
        }
        else if (other.gameObject.CompareTag("MazeSpecial"))
        {
            //do something for last puzzle
            OnSpecial();
        }
    }

    public void OnFinish()
    {
        doorCon.OpenDoor();
        hasFinished = true;
        gm.ActivateChkpt(2);
    }
    public void OnSpecial()
    {
        gm.ActivateChkpt(3);
    }

    void OnCollisionStay(Collision collision)
    {
        if (Physics.ComputePenetration(collider, collider.transform.position, collider.transform.rotation, collision.collider, collision.collider.transform.position, collision.collider.transform.rotation, out Vector3 direction, out float distance))
        {
            Debug.Log($"{direction} + {distance}");
            gameObject.transform.Translate(direction * distance);
        }
    }
}
