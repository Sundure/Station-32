using System.Collections;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    [field : SerializeField] public Rigidbody RB { get; private set; }
    [field : SerializeField] public Collider Collider { get; private set; }

    public void SetRB(Rigidbody rb) => RB = rb;
    public void SetCollider(Collider collider) => Collider = collider;

    public void SetValues(Rigidbody rb, Collider collider)
    {
        RB = rb;
        Collider = collider;
    }

    public void EnableCollider()
    {
        Collider.enabled = true;
    }
    public void DisableCollider()
    {
        Collider.enabled = false;
    }

    public void AddIgnoredRBLayer(LayerMask layer)
    {
        RB.excludeLayers |= layer;
    }
    public void AddIgnoredRBLayer(LayerMask layer, float time)
    {
        RB.excludeLayers |= layer;

        StartCoroutine(AddIgnoredColliderLayerCoroutine(layer, time));
    }

    private IEnumerator AddIgnoredColliderLayerCoroutine(LayerMask layer, float time)
    {
        yield return new WaitForSeconds(time);

        RB.excludeLayers &= ~layer;
    }
}
