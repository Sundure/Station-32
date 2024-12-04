using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public float Health;

    public float Heat;

    public bool Grounded;

    public bool Jumped;

    public bool CanJump;

    public static readonly float InteractRange = 4f;

    public static readonly int MaxInventorySlot = 5;

    public readonly float PlayerFallSpeed = 8;
}
