using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCamera : MonoBehaviour
{
    private const string _mainCameraTag = "MainCamera";
    private GameObject _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag(_mainCameraTag);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = _mainCamera.transform.position;
        this.transform.rotation = _mainCamera.transform.rotation;
    }
}
