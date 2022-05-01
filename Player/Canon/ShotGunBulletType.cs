using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBulletType : CanonMoveBase,IShot
{
    private float _shotAngle = 40f;
    public void Shot(List<ShellBase> shell, CanonData canonData)
    {
        _animator.SetTrigger(_fireTrigger);
        for (int i = 0; i < shell.Count; i++)
        {
            shell[i].transform.parent = null;
            shell[i].transform.parent = _shotPos;
            shell[i].transform.localPosition = Vector3.zero;
            shell[i].transform.parent = null;
            shell[i].Reset(canonData.Range);
            shell[i].transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y-(_shotAngle/2f)+(_shotAngle/((float)shell.Count-1f))*i, 0);
            Rigidbody rigid = shell[i].GetComponent<Rigidbody>();
            rigid.AddForce(shell[i].transform.up * canonData.BulletSpeed,ForceMode.Impulse);
            
        }
    }
}
