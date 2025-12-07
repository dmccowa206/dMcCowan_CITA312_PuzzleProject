using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class DoorControl : MonoBehaviour
{
    [SerializeField] GameObject door;
    const string DOOR_ANIM_CLIP = "Open";
    public void OpenDoor()
    {
        door.GetComponent<Animator>().Play(DOOR_ANIM_CLIP, 0, 0f);
        door.GetComponent<AudioSource>().Play();
    }
}
