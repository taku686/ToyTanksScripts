using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShell : ShellBase,IInitialize
{
    public void Initialize(string poolTag)
    {
        if (_isInit)
        {
            return;
        }
        _isInit = true;
        _rb = this.gameObject.AddComponent<Rigidbody>();
        _col = this.gameObject.AddComponent<CapsuleCollider>();
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _rb.Sleep();
        _col.isTrigger = true;
        _pool = GameObject.FindGameObjectWithTag(poolTag).transform;
    }
}
