using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    private void Awake() => PlayerInputSystem.OnInputInteract += Interact;

    private void Interact()
    {
        IInteracted interacted = PlayerCamera.GetComponentRaycast<IInteracted>();

        interacted?.Interact();
    }

    private void OnDestroy() => PlayerInputSystem.OnInputInteract -= Interact;
}
