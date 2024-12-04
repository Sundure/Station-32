using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (PlayerCamera.GetComponentRaycast() != null)
            {
                IInteracted interacted = PlayerCamera.GetComponentRaycast();

                interacted.Interact();
            }
        }
    }
}
