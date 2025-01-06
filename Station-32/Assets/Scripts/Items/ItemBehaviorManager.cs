using UnityEngine;

public class ItemBehaviorManager : MonoBehaviour
{
    private IgnoredRBLayers _ignoredRBLayers;
    public IgnoredRBLayers IgnoredRBLayers { get { return _ignoredRBLayers; } }

    private Outline _outline;
    public Outline Outline { get { return _outline; } }

    private bool _outlined;

    private void Awake()
    {
        _ignoredRBLayers = gameObject.AddComponent<IgnoredRBLayers>();
        _outline = gameObject.AddComponent<Outline>();

        _outline.enabled = false;
    }

    public void EnableOutline()
    {
        if (_outlined == true)
            return;

        RaycastInteractedTips.OnRaycastUnhit += DisableOutline;

        _outlined = true;
        _outline.enabled = true;
    }

    public void DisableOutline()
    {
        RaycastInteractedTips.OnRaycastUnhit -= DisableOutline;

        _outlined = false;
        _outline.enabled = false;
    }

    private void OnDestroy()
    {
        RaycastInteractedTips.OnRaycastUnhit -= DisableOutline;
    }
}
