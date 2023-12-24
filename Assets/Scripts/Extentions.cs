using UnityEngine;

public static class Extentions
{
    static public void Ragdoll(this GameObject gameObject, bool isOn)
    {
        Rigidbody _rigidbody = gameObject.GetComponent<Rigidbody>();
        Animator _animator = gameObject.GetComponentInChildren<Animator>();

        foreach (Rigidbody rigidbody in _animator.GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.isKinematic = !isOn;
        }
        foreach (Collider collider in _animator.GetComponentsInChildren<Collider>())
        {
            //collider.enabled = isOn;
        }
        _animator.enabled = !isOn;
        _rigidbody.isKinematic = !isOn;
        //_collider.enabled = !isOn;
    }
}
