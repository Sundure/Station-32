using UnityEngine;

public class Door : MonoBehaviour, IInteracted
{
    [SerializeField] private bool _opened;

    [SerializeField] private Animator _animator;

    public void Interact()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            _opened = !_opened;

            _animator.SetBool("Opened", _opened);
            _animator.SetTrigger("Animate");
        }
    }
}
