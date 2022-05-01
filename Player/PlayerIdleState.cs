using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using State = StateMachine<PlayerMove>.State;

public partial class PlayerMove
{
    public class PlayerIdleState : State
    {
        BaseMove _baseMove;
        CanonMoveBase _canonMoveBase;
        UserData _userData;
        private Action _pointerUpCallBack;
        private bool _hasShotStopMethod;
        private const string _canonJoystickName = "CanonMove";

        protected override void OnEnter(State prevState)
        {
            _baseMove = Owner._baseMove;
            _canonMoveBase = Owner._canonMoveBase;
            _userData = Owner._userData;
            DetectEventTrigger(_userData);
        }

        protected override void OnExit(State nextState)
        {
            if(Owner._IshotStop != null)
            {
                Owner._IshotStop.ShotStop();
            }
            Owner._ultimateJoystick.OnPointerUpCallback -= _pointerUpCallBack;
        }

        protected override void OnUpdate()
        {
            _baseMove.Move(_userData._baseData.MoveSpeed);
            _canonMoveBase.Rotate();
            Owner._canonBar.Reload();
            if (Owner._ultimateJoystick.GetJoystickState())
            {
                OnDragUltimatejoystick();
            }
        }      

        private void OnDragUltimatejoystick()
        {
            if (Owner._canonBar.isFire())
            {
                Owner._Ishot.Shot(Owner._shellManager.GetShell(_shellPoolTag,_userData._currentCanonIndex),Owner._currentCanon);
            }
            else if(!Owner._canonBar.isFire()&&Owner._IshotStop != null)
            {
                Owner._IshotStop.ShotStop();
            }
            if (Owner._ItargetMarker != null)
            {
                Owner._ItargetMarker.MoveTargetMarker(Owner._targetMarker,_canonJoystickName,Owner._currentCanon.Range,Owner.transform);
            }
        }
        
        public void OnPointerUp()
        {
            if (!_hasShotStopMethod)
            {
                Owner._Ishot.Shot(Owner._shellManager.GetShell(_shellPoolTag,_userData._currentCanonIndex), Owner._currentCanon);
            }
            else if(_hasShotStopMethod)
            {
                Owner._IshotStop.ShotStop();
            }
            
        }

        private void DetectEventTrigger(UserData userData)
        {
            if(Owner._currentCanon.CanonKinds == CanonData.CanonType.BeamType ||
               Owner._currentCanon.CanonKinds == CanonData.CanonType.MachinegunType||
               Owner._currentCanon.CanonKinds == CanonData.CanonType.FlameType)
            {
                _hasShotStopMethod = true;
                _pointerUpCallBack = OnPointerUp;
                Owner._ultimateJoystick.OnPointerUpCallback += _pointerUpCallBack;
            }
            else
            {
                _hasShotStopMethod = false;
                _pointerUpCallBack = OnPointerUp;
                Owner._ultimateJoystick.OnPointerUpCallback += _pointerUpCallBack;
            }
        }

    }
}
