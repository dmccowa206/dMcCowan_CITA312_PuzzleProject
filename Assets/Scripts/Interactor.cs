using UnityEngine;
using StarterAssets;

public class Interactor : MonoBehaviour
{
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponent<FirstPersonController>();
    }
    void Update()
    {
        HandleInteract();
    }
    void HandleInteract()
    {
        if (!starterAssetsInputs.interact) return;

        starterAssetsInputs.InteractInput(false);

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, firstPersonController.interactRange))
        {
            Debug.Log($"Touched {hit.collider}/n run hit.collider.ObjectInteraction");
            ObjectInteraction obInteract = hit.collider.GetComponent<ObjectInteraction>();
            obInteract?.Interaction();
        }
    }
}
