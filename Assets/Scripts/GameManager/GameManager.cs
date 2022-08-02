using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] string SpawnPointMapTag = "SpawnPointMap";
    public MapData[] mapDatas;
    public int level=0;
    GameObject map;
    public Text NumburTextLevel;
    private void Awake()
    {
        CreateMapFirst();
        instance = this;
    }

    private void Start()
    {
       
    }

    public void CreateMapFirst()
    {
        level = Prefs.LEVEL;
        Debug.LogWarning(level);

        int indexLevel = level + 1;
        NumburTextLevel.text = indexLevel.ToString();// hiển thị text xem là level mấy !!!

        GameObject MapPoint = GameObject.FindGameObjectWithTag(SpawnPointMapTag);
        map = Instantiate(mapDatas[Prefs.LEVEL].mapPrefab);
        map.transform.position = MapPoint.transform.position;
        map.transform.rotation = MapPoint.transform.rotation;
        PlayerPrefs.Save();
    }

    public void LevelInCrease()
    {
        Destroy(map);
        SceneManager.LoadScene(0);
        level++;
        Prefs.LEVEL = level;
        Debug.LogWarning(level);
        Debug.LogWarning(Prefs.LEVEL + "!!!!");

        int indexLevel = level + 1;
        NumburTextLevel.text = indexLevel.ToString();// hiển thị text xem là level mấy !!!

        GameObject MapPoint = GameObject.FindGameObjectWithTag(SpawnPointMapTag);
        map = Instantiate(mapDatas[Prefs.LEVEL].mapPrefab);
        map.transform.position = MapPoint.transform.position;
        map.transform.rotation = MapPoint.transform.rotation;
        PlayerPrefs.Save();
    }
}
