using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Item/Canon")]
public class CanonData : ScriptableObject
{
    [SerializeField,Tooltip("���O")]
    private string _name;
    [SerializeField, Tooltip("�I�u�W�F�N�g")]
    private GameObject _canonObj;
    [SerializeField, Tooltip("�˒��͈�")]
    private float _range;
    [SerializeField, Tooltip("1��Œe�����Ă鐔")]
    private int _clipSize;
    [SerializeField, Tooltip("���˃_���[�W")]
    private float _damage;
    [SerializeField, Tooltip("�P��Œe�𔭎˂��鐔")]
    private int _fireCountLimit;
    [SerializeField, Tooltip("���˃X�s�[�h")]
    private float _bulletSpeed;
    [SerializeField, Tooltip("�����[�h����")]
    private float _reloadTime;
    [SerializeField, Tooltip("���C����")]
    private float _fireTime;
    [SerializeField, Tooltip("���ˊԊu")]
    private float _fireRate;
    [SerializeField, Tooltip("")]
    private float _chargeTime;
    [SerializeField, Tooltip("�o�E���X�����")]
    private int _bounceLimit;
    [SerializeField, Tooltip("��C�̎��")]
    private CanonType _canonKinds;
    [SerializeField, Tooltip("�e�̃I�u�W�F�N�g")]
    private GameObject _shellObj;
    [SerializeField, Tooltip("���ˈʒu")]
    private Vector3 _shotPos;
    [SerializeField, Tooltip("����")]
    private string _explanation;


    public enum CanonType
    {
        NormalBulletType,
        ShotGunBulletType,
        TrackingBulletType,
        ToxicBulletType,
        BounceBulletType,
        RailGunType,
        BeamType,
        MachinegunType,
        CanonType,
        FlameType,
        TwoCanonType
    }

    public string Name { get => _name; private set => _name = value; }
    public GameObject CanonObj { get => _canonObj; private set => _canonObj = value; }
    public float Range { get => _range; private set => _range = value; }
    public int ClipSize { get => _clipSize; private set => _clipSize = value; }
    public float Damage { get => _damage; private set => _damage = value; }
    public int FireCountLimit { get => _fireCountLimit; private set => _fireCountLimit = value; }
    public float BulletSpeed { get => _bulletSpeed; private set => _bulletSpeed = value; }
    public float ReloadTime { get => _reloadTime; private set => _reloadTime = value; }
    public float FireTime { get => _fireTime; private set => _fireTime = value; }
    public float FireRate { get => _fireRate; private set => _fireRate = value; }
    public float ChargeTime { get => _chargeTime; private set => _chargeTime = value; }
    public int BounceLimit { get => _bounceLimit; private set => _bounceLimit = value; }
    public string Explanation { get => _explanation; private set => _explanation = value; }
    public CanonType CanonKinds { get => _canonKinds; set => _canonKinds = value; }
    public GameObject ShellObj { get => _shellObj; set => _shellObj = value; }
    public Vector3 ShotPos { get => _shotPos; set => _shotPos = value; }
}
