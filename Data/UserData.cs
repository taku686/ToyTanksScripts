using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UserData 
{
  
    public BaseData _baseData;

    public int _currentCanonIndex;

    public List<BaseData> _availableBaseLists;

    public List<CanonData> _availableCanonList;

    public int _maxStage;

    public CanonData[] _currentEqipedCanonArray;


}
