using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public float Health;
    public float Heat;

    public bool Grounded;

    public bool Crouch;

    public bool Jumped;
    public bool CanJump;

    [SerializeField] private Collider _playerCollider;
    public Collider PlayerCollider { get { return _playerCollider; } }

    public readonly float PlayerFallSpeed = 8;

    public const float INTERACT_RANGE = 2f;

    public const int MAX_INVENTORY_SLOT = 5;


    public static LayerMask PlayerLayer { get; private set; }

    private void Awake()
    {
        PlayerLayer = 1 << gameObject.layer;

    }
}
