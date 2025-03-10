using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private float _invokeTime;

    [SerializeField] private bool _waitTime;

    [Header("Events")]
    [SerializeField] private UnityEvent _unityEvent;

    public void InvokeTrigger() => _unityEvent.Invoke();

    public void InvokeTimeTrigger() => StartCoroutine(InvokeTrigger(_invokeTime));

    private IEnumerator InvokeTrigger(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        _unityEvent.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_waitTime)
            StartCoroutine(InvokeTrigger(_invokeTime));
    }
}
