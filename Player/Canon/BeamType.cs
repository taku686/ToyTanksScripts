using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamType : CanonMoveBase, IShot,IShotStop
{
    private GameObject _beamObj;
    private BeamEffect _beamEffect;

    public void Shot(List<ShellBase> shell, CanonData canonData)
    {
        GenerateBeamObj(canonData);
        if (_animator.GetBool(_fireTrigger) == false)
        {
            _animator.SetBool(_fireTrigger, true);
        }  
        //後で見直しが必要 positionの初期設定がいるのか検証
        _beamObj.SetActive(true);
        _beamObj.transform.position = transform.TransformPoint(canonData.ShotPos);
        
        _beamEffect.OnSpawned(canonData.ShotPos, this.transform.parent);
    }

    private void GenerateBeamObj(CanonData canonData)
    {
        if (_beamObj != null)
        {
            return;
        }
        _beamObj = Instantiate(canonData.ShellObj,Vector3.zero,Quaternion.Euler(Vector3.zero),null);
        _beamEffect = _beamObj.GetComponent<BeamEffect>();
        _beamEffect.Initialize(true, canonData.Range);
    }

    public void ShotStop()
    {
        _beamObj.SetActive(false);
        _animator.SetBool(_fireTrigger, false);
    }

}
