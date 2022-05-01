using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const string _name = "Player";
    [SerializeField]
    private Transform _generatePos;
    [SerializeField]
    private float _playerSize = 1;
    [SerializeField]
    private GameObject _canonBar;
    [SerializeField]
    private GameObject _targetMarker;
    [SerializeField]
    private LayerMask _enemyLayerMask;

    private GameObject _playerObj;
    private PlayerMove _playerMoveSc;
    private UserData _userData;
    public void Initialize(UserData userData)
    {
        _userData = userData;
        _playerObj = new GameObject();
        _playerObj.name = _name;
        _playerObj.tag = _name;
        _playerObj.transform.position = _generatePos.position;
        _playerObj.transform.localScale = Vector3.one * _playerSize;
        _playerMoveSc = _playerObj.AddComponent<PlayerMove>();
        _playerMoveSc.Initialize(_userData, _targetMarker.transform, _canonBar, _enemyLayerMask);
    }

    public void ChangeCanon(CanonData canonData,int CanonIndex)
    {
        _playerMoveSc.ChangeCanon(_userData, canonData, CanonIndex);
    }
}
