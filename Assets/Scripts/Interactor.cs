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
            
                    // ObjectInteraction obInteract = hit.collider.GetComponentInParent<ObjectInteraction>();
                    // obInteract?.Interaction();
            Debug.Log($"Touched {hit.collider.gameObject.tag} run hit.collider.ObjectInteraction");

            switch(hit.collider.gameObject.tag)
            {
                case "Button":
                    ObjectInteraction obInteract = hit.collider.GetComponentInParent<ObjectInteraction>();
                    obInteract?.Interaction();
                    break;
                case "SlidePiece":
                    SlidePuzzle slidepuzzle = hit.collider.GetComponentInParent<SlidePuzzle>();
                    slidepuzzle?.MoveEmpty(hit.collider.transform);
                    break;
            }
        }
    }
}
