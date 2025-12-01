using UnityEngine;

public class TiltButtonInteraction : MonoBehaviour
{
    [SerializeField] GameObject maze;
    [SerializeField] Vector3 direction;
    public void OnInteract()
    {
        maze.GetComponent<MazeTilt>().Tilt(direction);
    }
}
