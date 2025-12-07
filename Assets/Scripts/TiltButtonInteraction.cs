using UnityEngine;

public class TiltButtonInteraction : MonoBehaviour
{
    [SerializeField] GameObject maze;
    [SerializeField] Vector3 direction;
    [SerializeField] Animator animator;
    public void OnInteract()
    {
        maze.GetComponent<MazeTilt>().SetTilt(direction);
        animator.Play("Press", 0 , 0f);
        GetComponent<AudioSource>().Play();
    }
}
