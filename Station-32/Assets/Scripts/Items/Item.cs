using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour, IInteracted
{
    [SerializeField] private RawImage _itemIcon;
    public RawImage ItemIcon { get { return _itemIcon; } }

    public Rigidbody RB { get; private set; }

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
