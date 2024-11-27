using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : Interacted
{
    [UnityEngine.SerializeField] private RawImage _itemIcon;
    public RawImage ItemIcon {  get { return _itemIcon; } }

    public Rigidbody RB { get; private set; }

    public static event Action<GameObject> AddItem;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }


    public override sealed void Interact()
    {
        AddItem?.Invoke(gameObject);
    }

    public abstract void Use();
    protected abstract void DisableItem();
}
