using UnityEngine;

public abstract class Structure : MonoBehaviour, IInteracted
{
    public InteractedType InteractedTypes { get; private set; } = InteractedType.Structure;

    public void Interact()
    {
        Use();
    }

    protected abstract void Use();
}
