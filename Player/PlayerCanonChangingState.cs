using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = StateMachine<PlayerMove>.State;
public partial class PlayerMove
{
    public class PlayerCanonChangingState : State
    {
        protected override void OnEnter(State prevState)
        {
            DestroyImmediate(Owner._currentCanonObj);
            Owner._currentCanonObj = Instantiate(Owner._currentCanon.CanonObj, Owner.transform);
            Owner._currentCanonObj.transform.localPosition = Owner._userData._baseData.CanonPos;
            Owner.DecideCanonType(Owner._currentCanon, Owner._currentCanonObj);
        }

        protected override void OnExit(State nextState)
        {
           
        }
    }
}
