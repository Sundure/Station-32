using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public float Health;

    public float Heat;

    public bool Grounded;

    public bool Jumped;

    public bool CanJump;

    public static readonly float InteractRange = 2f;

    public static readonly int MaxInventorySlot = 5;

    public readonly float PlayerFallSpeed = 8;

    public static LayerMask PlayerLayer { get; private set; }

    private void Awake()
    {
        PlayerLayer = 1 << gameObject.layer;
    }
}
