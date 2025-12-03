using System;
using UnityEngine;

public class MazeTilt : MonoBehaviour
{
    [SerializeField] float tiltSpeed = 1f;
    [SerializeField] float tiltRate = 5f;
    [SerializeField] float tiltMax = 10f;
    [SerializeField] GameObject specialZone;
    [SerializeField] GameObject slider;
    float deltaX, deltaZ;
    Quaternion targetRotation;
    void Update()
    {
        ProcessTilt();
        ProcessSpecialStatus();
    }
    void ProcessTilt()
    {
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
    }
    public void SetTilt(Vector3 direction)
    {
        Vector3 prevEuler = gameObject.transform.localEulerAngles;
        deltaX = prevEuler.x + (direction.x * tiltRate);
        if (deltaX < 180)
        {
            deltaX = Mathf.Clamp(deltaX, -tiltMax, tiltMax);
        }
        else
        {
            deltaX = Mathf.Clamp(deltaX, 360f-tiltMax, 360f+tiltMax);
        }
        deltaZ = prevEuler.z + (direction.z * tiltRate);
        if (deltaZ < 180)
        {
            deltaZ = Mathf.Clamp(deltaZ, -tiltMax, tiltMax);
        }
        else
        {
            deltaZ = Mathf.Clamp(deltaZ, 360f-tiltMax, 360f+tiltMax);
        }
        Debug.Log($"{deltaX} = {gameObject.transform.localEulerAngles.x} + {direction.x} * {tiltRate}");
        Debug.Log($"{deltaZ} = {gameObject.transform.localEulerAngles.z} + {direction.z} * {tiltRate}");
        // Debug.Log($"EulerAngle: {gameObject.transform.localEulerAngles.x} {gameObject.transform.localEulerAngles.z}");
        // Debug.Log($"Local rotation: {gameObject.transform.localRotation.x} {gameObject.transform.localRotation.z}");
        targetRotation = Quaternion.Euler(deltaX, prevEuler.y, deltaZ);
    }
    void ProcessSpecialStatus()
    {
        if (slider.GetComponent<SlidePuzzle>().CheckSpecial())
        {
            specialZone.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            specialZone.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
