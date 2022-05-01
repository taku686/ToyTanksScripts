using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GunCannon_TestMove : MonoBehaviour
{
    [SerializeField]
    float moveRange = 0.01f;

    Vector3 _startPosition;
    bool _isShake;
    private void Start()
    {
        _startPosition = transform.position;   
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)&&!_isShake)
        {
            _isShake = true;
            Shake();
        }
    }

    private void Shake()
    {
        Debug.Log("Shake");
        this.transform.DOShakePosition(2,0.05f,10,10,false,true).OnComplete(()=> { _isShake = false; });
    }
}
