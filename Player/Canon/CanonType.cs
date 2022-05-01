using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonType : CanonMoveBase, IShot,ITargetMarker
{
    Transform _targetMarker;
    const float _angle = 60;
    public void CreateTargetMarker(ref Transform targetMarker, Transform player)
    {
        targetMarker = Instantiate(targetMarker.gameObject).transform;
        targetMarker.transform.parent = null;
        targetMarker.localPosition = new Vector3(0, 0.3f, 0);
        targetMarker.eulerAngles = new Vector3(90, 0, 0);
        _targetMarker = targetMarker.transform;
    }

    public void MoveTargetMarker(Transform targetMarker,string controllerName,float range,Transform player)
    {
        //Debug.Log("TargetMarkerMove");
        float hori = UltimateJoystick.GetHorizontalAxis(controllerName);
        float vert = UltimateJoystick.GetVerticalAxis(controllerName);
       
        if (hori != 0 || vert != 0)
        {
            targetMarker.position = new Vector3(hori, 0, vert) * range + new Vector3(player.position.x, 0.3f, player.position.z);
        }

    }

    public void Shot(List<ShellBase> shell, CanonData canonData)
    {
        _animator.SetTrigger(_fireTrigger);
        shell[0].transform.parent = null;
        shell[0].transform.parent = _shotPos;
        shell[0].transform.localPosition = Vector3.zero;
        shell[0].transform.parent = null;
        shell[0].Reset(canonData.Range);
        Vector3 velocity = CalculateVelocity(shell[0].transform.position, _targetMarker.position, _angle);
        Rigidbody rigid = shell[0].GetComponent<Rigidbody>();
        //rigid.mass = canonData.BulletSpeed;
        rigid.useGravity = true;
        rigid.AddForce(velocity * rigid.mass, ForceMode.Impulse);
    }

    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        // 射出角をラジアンに変換
        float rad = angle * Mathf.PI / 180;

        // 水平方向の距離x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));

        // 垂直方向の距離y
        float y = pointA.y - pointB.y;

        // 斜方投射の公式を初速度について解く
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // 条件を満たす初速を算出できなければVector3.zeroを返す
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
        }
    }
}
