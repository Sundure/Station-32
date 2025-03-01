using UnityEngine;

public class PlayerSpherePointManager : MonoBehaviour
{
    [SerializeField] private CrouchSystem _crouchSystem;

    private const float _defaultYPosition = -0.585f;
    private const float _onCrouchYPosition = -0.18f;

    private void Awake()
    {
        _crouchSystem.PlayerCrouch += SetPosition;
    }

    private void SetPosition(bool crouch)
    {
        if (crouch)
            transform.localPosition = new(transform.localPosition.x, _onCrouchYPosition, transform.localPosition.z);
        else
            transform.localPosition = new(transform.localPosition.x, _defaultYPosition, transform.localPosition.z);
    }

    private void OnDestroy()
    {
        _crouchSystem.PlayerCrouch -= SetPosition;
    }
}
