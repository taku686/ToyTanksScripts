using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingShell : ShellBase,IInitialize
{
    [SerializeField]
    private Transform _target;
    private float _velocity;
    private float _maxDisatance;
    Vector3 _step;
    private CapsuleCollider _collider;
    private float _lifeTime = 3.5f;
    private float _timer = 0;
    private float _rotationSpeed = 1.5f;
    protected override void Update()
    {
        base.Update();
        Tracking();        
        _timer += Time.unscaledDeltaTime;
        if(_lifeTime < _timer)
        {
            Sleep();
        }
    }

    public void Initialize(string poolTag)
    {
        if (_isInit)
        {
            return;
        }
        _rb = this.gameObject.AddComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _rb.Sleep();
        _pool = GameObject.FindGameObjectWithTag(poolTag).transform;
        _collider = this.gameObject.AddComponent<CapsuleCollider>();
        _collider.isTrigger = true;
        _collider.direction = 2;
        _collider.center = new Vector3(0, -0.24f, 0.22f);
        _collider.radius = 0.32f;
        _collider.height = 2.8f;
        _isInit = true;
    }

    public void SetProperty(float range, float shellSpeed,Transform target)
    {
        _maxDisatance = range;
        _velocity = shellSpeed;
        _target = target;
    }

    protected override void Sleep()
    {
        base.Sleep();
        _timer = 0;
    }

    private void Tracking()
    {
        if (_target == null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + this.transform.forward * _maxDisatance, Time.deltaTime * _velocity);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(_target.position.x, this.transform.position.y, _target.position.z) - transform.position), Time.deltaTime * _rotationSpeed);
            _step = transform.forward * Time.deltaTime * _velocity;
            this.transform.position += _step;
        }
    }


   

  
}
