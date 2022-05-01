using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinegunType : CanonMoveBase, IShot,IShotStop
{
    private float _time=0;
    private float _rabdomValue = 0.7f;

    public void Shot(List<ShellBase> shell, CanonData canonData)
    {
        _time += Time.deltaTime;
        if (_animator.GetBool(_fireTrigger) == false)
        {
            _animator.SetBool(_fireTrigger, true);
        }
        if (_time > canonData.FireRate)
        {
            _time = 0;
            Fire(shell, canonData);
        }
    }

    private void Fire(List<ShellBase> shell,CanonData canonData)
    {
        for (int i = 0; i < shell.Count; i++)
        {
            shell[i].transform.parent = null;
            shell[i].transform.parent = _shotPos;
            shell[i].transform.localPosition = new Vector3(Random.Range(-_rabdomValue, _rabdomValue), 0, 0);
            shell[i].transform.parent = null;
            shell[i].Reset(canonData.Range);
            shell[i].transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0);
            Rigidbody rigid = shell[i].GetComponent<Rigidbody>();
            rigid.AddForce(shell[i].transform.up * canonData.BulletSpeed, ForceMode.Impulse);
        }
    }

    public void ShotStop()
    {
        _animator.SetBool(_fireTrigger, false);
    }
}
