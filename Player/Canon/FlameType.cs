using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameType : CanonMoveBase, IShot, IShotStop
{
    private GameObject _flameObj;
    private FlameEffect _flameEffect;
    public void Shot(List<ShellBase> shell, CanonData canonData)
    {
        GenerateEffect(canonData);
        _flameObj.SetActive(true);
        _flameObj.transform.position = transform.TransformPoint(canonData.ShotPos);
    }

    private void GenerateEffect(CanonData canonData)
    {
        if(_flameObj != null)
        {
            return;
        }
        _flameObj = Instantiate(canonData.ShellObj, Vector3.zero, Quaternion.identity, _shotPos);
        _flameObj.transform.localEulerAngles = Vector3.zero;

    }

    public void ShotStop()
    {
        _flameObj.SetActive(false);
    }
}
