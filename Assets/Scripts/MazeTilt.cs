using UnityEngine;

public class MazeTilt : MonoBehaviour
{
    [SerializeField] float tiltSpeed;
    [SerializeField] float tiltMax;
    public void Tilt(Vector3 direction)
    {
        gameObject.transform.Rotate(tiltSpeed * direction);
        Mathf.Clamp(gameObject.transform.rotation.x, -tiltMax, tiltMax);
        Mathf.Clamp(gameObject.transform.rotation.z, -tiltMax, tiltMax);
    }
}
