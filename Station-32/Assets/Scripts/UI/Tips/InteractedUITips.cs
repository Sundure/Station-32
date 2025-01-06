using TMPro;
using UnityEngine;

public class InteractedUITips : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _interactedTips;

    private void Awake()
    {
       RaycastInteractedTips.OnRaycastHit += ShowInteractedTips;
       RaycastInteractedTips.OnRaycastUnhit += DisableInteractedTips;
    }

    private void ShowInteractedTips(InteractedType type)
    {
        string action;

        switch (type)
        {
            case InteractedType.Structure:
                action = "Interact";
                break;
            case InteractedType.Item:
                action = "Pick Up Item";
                break;

            default:
                action = "Interact";
                break;

        }

        _interactedTips.text = $"Press E To {action}";

        _interactedTips.enabled = true;
    }

    private void DisableInteractedTips() => _interactedTips.enabled = false;


    private void OnDestroy()
    {
        RaycastInteractedTips.OnRaycastHit -= ShowInteractedTips;
        RaycastInteractedTips.OnRaycastUnhit -= DisableInteractedTips;
    }
}
