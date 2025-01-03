using System;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteracted
{
    [SerializeField] private Texture _itemIcon;
    public Texture ItemIcon { get { return _itemIcon; } } 

    [SerializeField] private Vector3 _standartRotation;
    public Vector3 StandartRotation { get { return _standartRotation; } }

    public Rigidbody RB { get; private set; }

    [SerializeField] private IgnoredRBLayers _ignoredRBLayers;
    public IgnoredRBLayers IgnoredRBLayers { get { return _ignoredRBLayers; } }

    public static event Action<GameObject> AddItem;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();

        RB.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    public void Interact()
    {
        AddItem?.Invoke(gameObject);

        RB.useGravity = false;
        RB.isKinematic = true;
    }

    public abstract void Use();
    protected abstract void DisableItem();
}
