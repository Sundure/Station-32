using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            IInteracted interacted = PlayerCamera.GetComponentRaycast<IInteracted>();

            interacted?.Interact();
        }
    }
}
