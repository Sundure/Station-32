using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public float Health;
    public float Heat;

    public bool Grounded;

    public bool Crouch;

    public bool Jumped;
    public bool CanJump;

    public Collider PlayerCollider {  get; private set; }

    public readonly float PlayerFallSpeed = 8;

    public const float InteractRange = 2f;

    public const int MaxInventorySlot = 5;


    public static LayerMask PlayerLayer { get; private set; }

    private void Awake()
    {
        PlayerLayer = 1 << gameObject.layer;

        PlayerCollider = transform.GetComponent<Collider>();
    }
}
