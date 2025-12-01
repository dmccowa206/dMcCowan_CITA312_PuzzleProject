using UnityEngine;

public class MazeTilt : MonoBehaviour
{
    [SerializeField] float tiltSpeed;
    [SerializeField] float tiltMax;
    void Tilt(float tSpeed, float tMax, Vector3 direction)
    {
        gameObject.transform.Rotate(tSpeed * direction);
        Mathf.Clamp(gameObject.transform.rotation.x, -tiltMax, tiltMax);
        Mathf.Clamp(gameObject.transform.rotation.z, -tiltMax, tiltMax);
    }
}
