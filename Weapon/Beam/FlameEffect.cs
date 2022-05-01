using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEffect : MonoBehaviour
{
    private const string _enemyTag = "Enemy";
    [SerializeField]
    private GameObject _burnEffect;
    public void Initialize()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag(_enemyTag))
        {
            return;
        }
        Health healthSc = other.GetComponent<Health>();
        if(healthSc == null) { return; }
        if(healthSc._burnEffectSc == null)
        {
            GameObject effect = Instantiate(_burnEffect, other.transform);
            effect.transform.localPosition = Vector3.zero;
            effect.transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            if (!healthSc._isBurning)
            {
                healthSc._burnEffectSc.gameObject.SetActive(true);
            }
        }
    }

}
