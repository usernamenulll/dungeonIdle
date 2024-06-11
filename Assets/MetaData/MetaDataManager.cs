using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class MetaDataManager : MonoBehaviour
{
    string path = "Assets/MetaData/Meta.json";
    [SerializeField] private MetaData metaData;
    void Start()
    {
        string jsonString = File.ReadAllText(path);

        metaData = JsonUtility.FromJson<MetaData>(jsonString);
        GameManager.Instance.metaData = metaData;
    }
}

[System.Serializable]
public class MetaData
{
    public int[] soldier;
    public int[] rune;

}
