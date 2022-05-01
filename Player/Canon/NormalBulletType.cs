using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBulletType : CanonMoveBase, IShot
{

    public void Shot(List<ShellBase> shell, CanonData canonData)
    {
        _animator.SetTrigger(_fireTrigger);
        shell[0].transform.parent = null;
        shell[0].transform.parent = _shotPos;
        shell[0].transform.localPosition = Vector3.zero;
        shell[0].transform.parent = null;
        shell[0].Reset(canonData.Range);
        shell[0].transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0);
        Rigidbody rigid = shell[0].GetComponent<Rigidbody>();
        rigid.AddForce(shell[0].transform.up * canonData.BulletSpeed, ForceMode.Impulse);
    }
}
