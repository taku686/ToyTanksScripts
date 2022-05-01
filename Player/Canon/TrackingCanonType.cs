using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrackingCanonType : CanonMoveBase,IShot,ISetLayerMask
{
    private  float _rigidSphereRadius = 10f;
    private const string _enemyTag = "Enemy";
    private LayerMask _enemyLayerMask;
    private Rigidbody _rigid;
    private SphereCollider _sphereCollider;
    public float m_fSightAngle = 60.0f;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private List<Transform> _targetsList = new List<Transform>();
    protected override void Start()
    {
        base.Start();
        _sphereCollider = this.gameObject.AddComponent<SphereCollider>();
        _sphereCollider.center = Vector3.zero;
        _sphereCollider.radius = 35f / 2f;
        _sphereCollider.isTrigger = true;
    }

    private void Update()
    {
        DetectTarget(DecideTarget());
    }

    public void Shot(List<ShellBase> shell, CanonData canonData)
    {
        _animator.SetTrigger(_fireTrigger);
        shell[0].transform.parent = null;
        shell[0].transform.parent = _shotPos;
        shell[0].transform.localPosition = Vector3.zero;
        shell[0].transform.parent = null;
        shell[0].Reset(canonData.Range);
        shell[0].transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        shell[0].GetComponent<TrackingShell>().SetProperty(canonData.Range, canonData.BulletSpeed,_target);

    }

    private void DetectTarget(Transform target)
    {
        if (target == null) return;
        Vector3 posDelta = target.transform.position - transform.position;
        float targetAngle = Vector3.Angle(transform.forward, posDelta);
        //Debug.Log(targetAngle);
        if (targetAngle < m_fSightAngle)
        {
            _dir = new Vector3(posDelta.x, 0f, posDelta.z);
            if (Physics.Raycast(transform.position, _dir, out RaycastHit hit))
            {
                if (hit.collider.gameObject == target.gameObject)
                {
                    _target = target.transform;
                    Debug.Log(_target.name);
                }
            }
        }
        else
        {
            _target = null;
        }
    }
    private Vector3 _dir;

    private Transform DecideTarget()
    {
        Transform target= null;
        if(_targetsList.Count < 1)
        {
            return target;
        }
        if(_targetsList.Count == 1)
        {
            return _targetsList[0];
        }
        foreach (var target_candidate in _targetsList)
        {
            if(target == null)
            {
                target = target_candidate;
            }
            if (Vector3.Distance(this.transform.position, target.position) > Vector3.Distance(this.transform.position, target_candidate.position))
            {
                target = target_candidate;
            }
        }
        return target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_enemyTag))
        {
            return;
        }
        foreach(var target in _targetsList)
        {
            if(target.gameObject == other.gameObject)
            {
                return;
            }
        }

        _targetsList.Add(other.transform);

    }

    private void OnDrawGizmos()
    {
        if (_target == null) return;
        Debug.DrawRay(transform.position, _target.transform.position-transform.position, Color.red, 0.01f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_enemyTag))
        {
            return;
        }
        _targetsList.Remove(other.transform);
    }



    public void SetLayerMask(LayerMask layerMask)
    {
        _enemyLayerMask = layerMask;
    }
}
