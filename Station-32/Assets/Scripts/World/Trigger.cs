using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private float _invokeTime;

    [Header("Events")]
    [SerializeField] private UnityEvent UnityEvent;

    public void InvokeTrigger()
    {
        StartCoroutine(TiggerCoroutine());
    }

    private IEnumerator TiggerCoroutine()
    {
        yield return new WaitForSeconds(_invokeTime);

        UnityEvent.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TiggerCoroutine());
    }
}
