using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonMoveBase : MonoBehaviour
{
    private const string _joystickName = "CanonMove";
    protected const string _fireTrigger = "Fire";
    protected string _poolTag;
    protected Transform _shotPos;
    protected Animator _animator;



    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void CreateShotPos(Vector3 shotPos)
    {
        _shotPos = new GameObject().transform;
        _shotPos.name = "ShotPos";
        _shotPos.transform.parent = this.transform;
        _shotPos.localPosition = shotPos;
    }

    public void Rotate()
    {
        float hori = UltimateJoystick.GetHorizontalAxis(_joystickName);
        float vert = UltimateJoystick.GetVerticalAxis(_joystickName);
        if (hori != 0 || vert != 0)
        {
            var direction = new Vector3(hori, 0, vert);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
public interface ISetLayerMask
{
    void SetLayerMask(LayerMask layerMask);
}

public interface IShot
{
    void Shot(List<ShellBase> shell, CanonData canonData);
}

public interface IShotStop
{
    void ShotStop();
}

public interface ITargetMarker
{
    void CreateTargetMarker(ref Transform targetMarker, Transform player);

    void MoveTargetMarker(Transform targetMarker, string controllerName, float range,Transform player);
}