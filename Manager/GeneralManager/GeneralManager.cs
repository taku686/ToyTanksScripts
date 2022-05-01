using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    [SerializeField]
    private int[] _defaultBaseNumber = new int[3];
    [SerializeField]
    private CanonData[] _defaultCanonData = new CanonData[3];
    [SerializeField]
    private List<BaseData> _baseDataList;
    [SerializeField]
    private List<CanonData> _canonDataList;
    [SerializeField]
    private PlayerManager _playerManager;
    [SerializeField]
    private CameraManager _cameraManager;
    [SerializeField]
    private ShellManager _shellManager;
    [SerializeField]
    private CanonSwitchManager _canonSwitchManager;
    private CanonData _currentCanonData;
    private void Awake()
    {
        UserData userData = SaveSystem.Instance.UserData;
        if (userData._baseData == null  || userData._currentEqipedCanonArray.Length == 0)
        {
            userData._baseData = _baseDataList[_defaultBaseNumber[0]];
            userData._currentEqipedCanonArray = _defaultCanonData;
        }
        Application.targetFrameRate = 60;
      
        _canonSwitchManager.Initialize(userData._currentEqipedCanonArray,_playerManager);
        _playerManager.Initialize(userData);
        _cameraManager.Initialize();
        _shellManager.Initialize(userData);
    }
}
