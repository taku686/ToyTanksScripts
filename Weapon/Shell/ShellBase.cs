using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBase : MonoBehaviour
{
    protected const string _groundTag = "Ground";
    protected const string _enemyTag = "Enemy";
    protected Rigidbody _rb;
    protected Collider _col;
    [SerializeField]
    protected Vector3 _initPos;
    protected float _limitRange;
    [SerializeField]
    protected Transform _pool;
    protected bool _isInit;

    protected virtual void Update()
    {
        if (Vector3.Distance(_initPos, this.transform.position) > _limitRange)
        {
            Sleep();
        }
    }
  
    public virtual void Reset(float limitRange)
    {
        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
        } 
        _limitRange = limitRange;
        _initPos = this.transform.position;
    }

    protected virtual void Sleep()
    {
        this.gameObject.SetActive(false);
        this.transform.SetParent(_pool);
        this.transform.localPosition = Vector3.zero;
        _rb.Sleep();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_groundTag) || other.CompareTag(_enemyTag))
        {
            Sleep();
        }
    }
}

public interface IInitialize
{
    void Initialize(string poolTag);
}


