using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellManager : MonoBehaviour
{
    public List<ShellBase>[] _playerShellList = { new List<ShellBase>(), new List<ShellBase>(), new List<ShellBase>() };
    public List<ShellBase> _enemyShellList = new List<ShellBase>();
    private UserData _userData;
    private int _maxCount = 50;

    [SerializeField]
    private Transform[] _playerPools;
    [SerializeField]
    private Transform _enemyPool;
    private CanonData _currentCanon;
    public void Initialize(UserData userData)
    {
        _userData = userData;
        _currentCanon = _userData._currentEqipedCanonArray[_userData._currentCanonIndex];
        for(int i = 0; i < _userData._currentEqipedCanonArray.Length; i++)
        {
            if (_userData._currentEqipedCanonArray[i].CanonKinds == CanonData.CanonType.BeamType ||
          _userData._currentEqipedCanonArray[i].CanonKinds == CanonData.CanonType.FlameType) { return; }
            CreatePool(_userData._currentEqipedCanonArray[i].ShellObj, _playerPools[i],i);
        }
       
    }


    private void CreatePool(GameObject shellObj, Transform parent,int index)
    {
        for (int i = 0; i < _maxCount; i++)
        {
            ShellBase newShellBase = CreateShell(shellObj, parent);
            newShellBase.gameObject.SetActive(false);
            _playerShellList[index].Add(newShellBase);
        }
    }

    private ShellBase CreateShell(GameObject shellObj, Transform parent)
    {
        GameObject shell = Instantiate(shellObj, parent);
        DetectShellType(_userData, shell);
        return shell.GetComponent<ShellBase>();
    }

    public List<ShellBase> GetShell(string poolTag,int index)
    {
        List<ShellBase> objs = new List<ShellBase>();
        foreach (ShellBase obj in _playerShellList[index])
        {
            if (!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                obj.GetComponent<IInitialize>().Initialize(poolTag);
                objs.Add(obj);
                if (objs.Count == _userData._currentEqipedCanonArray[_userData._currentCanonIndex].FireCountLimit)
                {
                    return objs;
                }
            }
        }
        for (int i = 0; i < _userData._currentEqipedCanonArray[_userData._currentCanonIndex].FireCountLimit; i++)
        {
            ShellBase newobj = CreateShell(_userData._currentEqipedCanonArray[_userData._currentCanonIndex].ShellObj, _playerPools[_userData._currentCanonIndex]);
            _playerShellList[index].Add(newobj);
            newobj.GetComponent<IInitialize>().Initialize(poolTag);
            objs.Add(newobj);
        }
        return objs;
    }

    private GameObject DetectShellType(UserData userData, GameObject shell)
    {
        switch (userData._currentEqipedCanonArray[_userData._currentCanonIndex].CanonKinds)
        {
            case CanonData.CanonType.BeamType:
                break;
            case CanonData.CanonType.BounceBulletType:
                break;
            case CanonData.CanonType.CanonType:
                shell.AddComponent<CanonShell>();
                break;
            case CanonData.CanonType.MachinegunType:
                shell.AddComponent<NormalShell>();
                break;
            case CanonData.CanonType.NormalBulletType:
                shell.AddComponent<NormalShell>();
                break;
            case CanonData.CanonType.RailGunType:
                break;
            case CanonData.CanonType.ShotGunBulletType:
                shell.AddComponent<NormalShell>();
                break;
            case CanonData.CanonType.ToxicBulletType:
                break;
            case CanonData.CanonType.TrackingBulletType:
                shell.AddComponent<TrackingShell>();
                break;
            case CanonData.CanonType.TwoCanonType:
                shell.AddComponent<NormalShell>();
                break;
        }
        return shell;
    }
}
