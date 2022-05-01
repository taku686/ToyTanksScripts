using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanonBar : MonoBehaviour
{
    Slider _slider;
    float _reloadTime;
    bool _isReload;
    float _timer=0;
   
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);   
    }

    public void Initialize(float maxValue,float reloadTime)
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = maxValue;
        _reloadTime = reloadTime;
        _slider.value = maxValue;
    }

    public bool isFire()
    {
        _slider.value -= Time.deltaTime;
        if (_slider.value > 0&&!_isReload)
        {
            return true;
        }
        else
        {
            _isReload = true;
            return false;
        }
       
    }

    public void Reload()
    {
        if (!_isReload)
        {
            return;
        }
        _timer += Time.deltaTime;
        _slider.value = _slider.maxValue * (_timer / _reloadTime);
        if(_timer >= _reloadTime)
        {
            _isReload = false;
            _timer = 0;
        }
    }
}
