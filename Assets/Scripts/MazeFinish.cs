using UnityEngine;

public class MazeFinish : MonoBehaviour
{
    DoorControl doorCon;
    bool hasFinished = false;
    float specialTimer = -1;
    new Collider collider;
    GameManager gm;
    void Start()
    {
        doorCon = GetComponent<DoorControl>();
        collider = gameObject.GetComponent<SphereCollider>();
        gm = FindAnyObjectByType<GameManager>();
    }
    void Update()
    {
        if (specialTimer >= 0)
        {
            specialTimer -= Time.deltaTime;
            if (specialTimer <= 0)
            {
                OnSpecial();
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MazeNormal") && !hasFinished)
        {
            OnFinish();
        }
        else if (other.gameObject.CompareTag("MazeSpecial") && hasFinished)
        {
            //do something for last puzzle
            specialTimer = 5f;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MazeSpecial"))
        {
            specialTimer = -1;
        }
    }

    public void OnFinish()
    {
        doorCon.OpenDoor();
        hasFinished = true;
        gm.ActivateChkpt(2);
        gm.audioPlayer.PlayPuzzleClip();
    }
    public void OnSpecial()
    {
        gm.ActivateChkpt(3);
    }

    void OnCollisionStay(Collision collision)
    {
        if (Physics.ComputePenetration(collider, collider.transform.position, collider.transform.rotation, collision.collider, collision.collider.transform.position, collision.collider.transform.rotation, out Vector3 direction, out float distance))
        {
            // Debug.Log($"{direction} + {distance}");
            gameObject.transform.Translate(direction * distance);
        }
    }
}
