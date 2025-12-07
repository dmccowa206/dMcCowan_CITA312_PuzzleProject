using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string clipName;
    [SerializeField] int roomIndex;
    GameManager gm;
    void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
    }
    public void Interaction()
    {
        animator.Play(clipName, 0 , 0f);
        DoorControl doorCon = GetComponent<DoorControl>();
        doorCon?.OpenDoor();
        gm.ActivateChkpt(roomIndex);
    }
}
