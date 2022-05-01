using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEffect : MonoBehaviour
{
    private Health _healthSc;   

    private void Awake()
    {
        _healthSc = GetComponentInParent<Health>();
        _healthSc._burnEffectSc = this;
    }

    private void OnEnable()
    {
        _healthSc._isBurning = true;
    }

    private void OnParticleSystemStopped()
    {
        _healthSc._isBurning = false;
        this.gameObject.SetActive(false);
    }
}
