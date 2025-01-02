using TMPro;
using UnityEngine;

public class InteractedUITips : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _interactedTips;

    private void Awake()
    {
       RaycastInteractedTips.OnRaycastHit += EnableInteractedTips;
       RaycastInteractedTips.OnRaycastUnhit += DisableInteractedTips;
    }

    private void EnableInteractedTips() => _interactedTips.enabled = true;

    private void DisableInteractedTips() => _interactedTips.enabled = false;


    private void OnDestroy()
    {
        RaycastInteractedTips.OnRaycastHit -= EnableInteractedTips;
        RaycastInteractedTips.OnRaycastUnhit -= DisableInteractedTips;
    }
}
