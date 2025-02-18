using System;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteracted
{
    public InteractedType InteractedTypes { get; private set; } = InteractedType.Item;

    [SerializeField] private Texture _itemIcon;
    public Texture ItemIcon { get { return _itemIcon; } }

    [SerializeField] private Vector3 _standartRotation;
    public Vector3 StandartRotation { get { return _standartRotation; } }

    public Rigidbody RB { get; private set; }
    public Collider Collider { get; private set; }

    public ItemBehaviorManager ItemBehaviorManager { get; private set; }

    [SerializeField] private bool _canHoldToUse;
    public bool CanHoldToUse { get { return _canHoldToUse; } }

    public bool OnInventory { get; private set; }

    public static event Action<GameObject> AddItem;

    private void Awake()
    {
        Collider = GetComponent<Collider>();

        RB = GetComponent<Rigidbody>();

        RB.collisionDetectionMode = CollisionDetectionMode.Continuous;

        ItemBehaviorManager = gameObject.AddComponent<ItemBehaviorManager>();

        ItemBehaviorManager.PhysicsManager.SetValues(RB,Collider);
    }

    public void Interact()
    {
        AddItem?.Invoke(gameObject);

        RB.useGravity = false;
        RB.isKinematic = true;
    }

    public void OnItemDrop()
    {
        ItemBehaviorManager.PhysicsManager.AddIgnoredRBLayer(PlayerProperties.PlayerLayer, 0.5f);
        ItemBehaviorManager.PhysicsManager.EnableCollider();

        OverrideOnItemDrop();
        OnInventory = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void OnItemPickUp()
    {
        ItemBehaviorManager.PhysicsManager.AddIgnoredRBLayer(PlayerProperties.PlayerLayer);
        ItemBehaviorManager.PhysicsManager.DisableCollider();

        OnInventory = true;
        gameObject.layer = LayerMask.NameToLayer("Item");
    }

    public abstract void Use();
    protected abstract void OverrideOnItemDrop();
}
