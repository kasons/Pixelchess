using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject knightUnitPrefab;
    public GameObject archerUnitPrefab;
    public GameObject mageUnitPrefab;
    public GameObject tankUnitPrefab;
    public GameObject assassinUnitPrefab;

    private GameObject unitToBuild;

    public GameObject GetUnitToBuild()
    {
        return unitToBuild;
    }

    public void SetUnitToBuild(GameObject unit)
    {
        unitToBuild = unit;
    }
}
