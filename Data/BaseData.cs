using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Item",menuName ="Item/Base")]
public class BaseData : ScriptableObject
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private GameObject _baseObj;
    [SerializeField]
    private BaseType _baseType;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private int _energyCapacity;
    [SerializeField]
    private float _energyChargeSpeed;
    [SerializeField]
    private int _moveSpeed;
    [SerializeField]
    private int _itemCapacity;
    [SerializeField]
    Vector3 _canonPos;

    public int Hp { get => _hp;private set => _hp = value; } 
    public int EnergyCapacity { get => _energyCapacity;private set => _energyCapacity = value; }
    public float EnergyChargeSpeed { get => _energyChargeSpeed;private set => _energyChargeSpeed = value; }
    public int MoveSpeed { get => _moveSpeed;private set => _moveSpeed = value; }
    public int ItemCapacity { get => _itemCapacity;private set => _itemCapacity = value; }
    public string Name { get => _name;private set => _name = value; }
    public GameObject BaseObj { get => _baseObj;private set => _baseObj = value; }
    public BaseType BaseType1 { get => _baseType;private set => _baseType = value; }
    public Vector3 CanonPos { get => _canonPos;private set => _canonPos = value; }

    public enum BaseType
    {
        Ground,
        Amphibious
    }
}
