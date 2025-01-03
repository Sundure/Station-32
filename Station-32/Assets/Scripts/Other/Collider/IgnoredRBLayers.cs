using System.Collections;
using UnityEngine;

public class IgnoredRBLayers : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    public void AddIgnoredRBLayer(LayerMask layer)
    {
        _rb.excludeLayers |= layer;
    }
    public void AddIgnoredRBLayer(LayerMask layer, float time)
    {
        _rb.excludeLayers |= layer;

        StartCoroutine(AddIgnoredColliderLayerCoroutine(layer, time));
    }

    private IEnumerator AddIgnoredColliderLayerCoroutine(LayerMask layer, float time)
    {
        yield return new WaitForSeconds(time);

        _rb.excludeLayers &= ~layer;
    }
}
