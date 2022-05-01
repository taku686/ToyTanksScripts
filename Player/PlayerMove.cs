using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public partial class PlayerMove : MonoBehaviour
{
    private const string _joystickTag = "CanonJoystick";
    private const string _shellManagerTag = "ShellManager";
    private const string _shellPoolTag = "PlayerShellPool";
    private StateMachine<PlayerMove> _stateMachine;
    private BaseMove _baseMove;
    private UserData _userData;
    [SerializeField]
    private CanonMoveBase _canonMoveBase;
    private ShellManager _shellManager;
    private UltimateJoystick _ultimateJoystick;
    private CanonBar _canonBar;
    private IShot _Ishot;
    private IShotStop _IshotStop;
    private ITargetMarker _ItargetMarker;
    private ISetLayerMask _IinitializeCanon;
    private LayerMask _enemyLayerMask;
    public Transform _targetMarker;
    private GameObject[] _canonArray = new GameObject[3];
    private CanonData _currentCanon;
    private GameObject _currentCanonObj;

    private enum Event
    {
        Idle,
        Dead,
        CanonChanging
    }
   
    // Update is called once per frame
    void Update()
    {
        _stateMachine.Update();

        //test
        if (Input.GetKeyUp(KeyCode.Z)){
            _stateMachine.Dispatch((int)Event.Dead);
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            _stateMachine.Dispatch((int)Event.Idle);
        }
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void Initialize(UserData userData,Transform targetMarker,GameObject canonBar,LayerMask layerMask)
    {
        _userData = userData;
        _shellManager = GameObject.FindGameObjectWithTag(_shellManagerTag).GetComponent<ShellManager>();
        GameObject joystick = GameObject.FindGameObjectWithTag(_joystickTag);
        _ultimateJoystick = joystick.GetComponent<UltimateJoystick>();

        CreateCanon(userData._currentEqipedCanonArray, userData._baseData, canonBar,0);
        CreateBase(userData._baseData);

        _targetMarker = targetMarker;
        _enemyLayerMask = layerMask;

        _stateMachine = new StateMachine<PlayerMove>(this);
        _stateMachine.AddTransition<PlayerIdleState, PlayerDeadState>((int)Event.Dead);
        _stateMachine.AddTransition<PlayerIdleState, PlayerCanonChangingState>((int)Event.CanonChanging);
        _stateMachine.AddAnyTransition<PlayerIdleState>((int)Event.Idle);
        _stateMachine.Start<PlayerIdleState>();
    }

    public void CreateCanon(CanonData[] canonDataArray, BaseData baseData, GameObject canonBar, int CanonIndex)
    {
        _currentCanonObj = Instantiate(canonDataArray[CanonIndex].CanonObj, this.transform);
        _currentCanonObj.transform.localPosition = baseData.CanonPos;
        DecideCanonType(canonDataArray[CanonIndex], _currentCanonObj);
        _currentCanon = canonDataArray[0];
        _userData._currentCanonIndex = 0;
        _canonBar = Instantiate(canonBar, this.transform).GetComponentInChildren<CanonBar>();
        _canonBar.Initialize(canonDataArray[0].FireTime, canonDataArray[0].ReloadTime);
    }

    private void CreateBase(BaseData baseData)
    {
        GameObject baseObj = Instantiate(baseData.BaseObj, this.transform);
        baseObj.transform.localPosition = Vector3.zero;
        _baseMove = this.gameObject.AddComponent<BaseMove>();
    }

    public void ChangeCanon(UserData userData, CanonData canonData, int canonIndex)
    {
        _userData._currentCanonIndex = canonIndex;
        _canonBar.Initialize(canonData.FireTime, canonData.ReloadTime);
        _currentCanon = canonData;
        _stateMachine.Dispatch((int)Event.CanonChanging);
        _stateMachine.Dispatch((int)Event.Idle);
    }

    private void DecideCanonType(CanonData canonData, GameObject canonObj)
    {
        switch (canonData.CanonKinds)
        {
            case CanonData.CanonType.BounceBulletType:
                _canonMoveBase = canonObj.AddComponent<NormalBulletType>();
                break;
            case CanonData.CanonType.NormalBulletType:
                _canonMoveBase = canonObj.AddComponent<NormalBulletType>();
                break;
            case CanonData.CanonType.RailGunType:
                _canonMoveBase = canonObj.AddComponent<NormalBulletType>();
                break;
            case CanonData.CanonType.ShotGunBulletType:
                _canonMoveBase = canonObj.AddComponent<ShotGunBulletType>();
                break;
            case CanonData.CanonType.ToxicBulletType:
                _canonMoveBase = canonObj.AddComponent<NormalBulletType>();
                break;
            case CanonData.CanonType.TrackingBulletType:
                _canonMoveBase = canonObj.AddComponent<TrackingCanonType>();
                break;
            case CanonData.CanonType.BeamType:
                _canonMoveBase = canonObj.AddComponent<BeamType>();
                break;
            case CanonData.CanonType.MachinegunType:
                _canonMoveBase = canonObj.AddComponent<MachinegunType>();
                break;
            case CanonData.CanonType.CanonType:
                _canonMoveBase = canonObj.AddComponent<CanonType>();
                break;
            case CanonData.CanonType.FlameType:
                _canonMoveBase = canonObj.AddComponent<FlameType>();
                break;
            case CanonData.CanonType.TwoCanonType:
                _canonMoveBase = canonObj.AddComponent<TwoCanonType>();
                break;
            default:
                break;
        }
        _canonMoveBase.CreateShotPos(canonData.ShotPos);
        _Ishot=_canonMoveBase.GetComponent<IShot>();
        _IshotStop = _canonMoveBase.GetComponent<IShotStop>();
        _ItargetMarker = _canonMoveBase.GetComponent<ITargetMarker>();
        _IinitializeCanon = _canonMoveBase.GetComponent<ISetLayerMask>();
        if(_ItargetMarker != null)
        {
            _ItargetMarker.CreateTargetMarker(ref _targetMarker, this.transform);
        }
        if(_IinitializeCanon != null)
        {
            _IinitializeCanon.SetLayerMask(_enemyLayerMask);
        }
    }
}
