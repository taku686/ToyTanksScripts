using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanonSwitchManager : MonoBehaviour
{
    private const float _canonRotationY = 198.76f;
    public CanonData[] _canonDataArray = new CanonData[3];
    [SerializeField]
    private Button _button;
    [SerializeField]
    private List<GameObject> _canonList = new List<GameObject>();
    private int count=0;
    public CanonData _currentCanon;
    private PlayerManager _playerManager;
  

    public void Initialize(CanonData[] canonDataArray,PlayerManager playerManager)
    {
        _canonDataArray = canonDataArray;
        _playerManager = playerManager;
        for (int i = 0; i < 3; i++)
        {
            GameObject canon = Instantiate(_canonDataArray[i].CanonObj);
            canon.transform.position = new Vector3(-100 + 100 * i, 1000, 0);
            canon.transform.localEulerAngles = new Vector3(0, _canonRotationY, 0);
            canon.GetComponent<Animator>().enabled = false;
            _canonList.Add(canon);
        }
        _currentCanon = _canonDataArray[0];
    }
    
    public void ChangeCanon()
    {
        count++;
        _canonList[0].transform.position = new Vector3(-100 + 100 * (count % 3), 1000, 0);
        _canonList[2].transform.position = new Vector3(-100 + 100 * ((count +1) % 3), 1000, 0);
        _canonList[1].transform.position = new Vector3(-100 + 100 * ((count +2) % 3), 1000, 0);
        _currentCanon = _canonDataArray[count%3];
        _playerManager.ChangeCanon(_currentCanon, count % 3);
    }



}
