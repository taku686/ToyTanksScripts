using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Item/Canon")]
public class CanonData : ScriptableObject
{
    [SerializeField,Tooltip("名前")]
    private string _name;
    [SerializeField, Tooltip("オブジェクト")]
    private GameObject _canonObj;
    [SerializeField, Tooltip("射程範囲")]
    private float _range;
    [SerializeField, Tooltip("1回で弾を撃てる数")]
    private int _clipSize;
    [SerializeField, Tooltip("発射ダメージ")]
    private float _damage;
    [SerializeField, Tooltip("１回で弾を発射する数")]
    private int _fireCountLimit;
    [SerializeField, Tooltip("発射スピード")]
    private float _bulletSpeed;
    [SerializeField, Tooltip("リロード時間")]
    private float _reloadTime;
    [SerializeField, Tooltip("発砲時間")]
    private float _fireTime;
    [SerializeField, Tooltip("発射間隔")]
    private float _fireRate;
    [SerializeField, Tooltip("")]
    private float _chargeTime;
    [SerializeField, Tooltip("バウンスする回数")]
    private int _bounceLimit;
    [SerializeField, Tooltip("大砲の種類")]
    private CanonType _canonKinds;
    [SerializeField, Tooltip("弾のオブジェクト")]
    private GameObject _shellObj;
    [SerializeField, Tooltip("発射位置")]
    private Vector3 _shotPos;
    [SerializeField, Tooltip("説明")]
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
