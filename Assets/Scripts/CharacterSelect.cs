using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    BuildManager buildManager;

    void Start ()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectKnightUnit ()
    {
        Debug.Log("Knight Unit Selected");
        buildManager.SetUnitToBuild(buildManager.knightUnitPrefab);
    }

    public void SelectArcherUnit()
    {
        Debug.Log("Archer Unit Selected");
        buildManager.SetUnitToBuild(buildManager.archerUnitPrefab);
    }

    public void SelectMageUnit()
    {
        Debug.Log("Mage Unit Selected");
        buildManager.SetUnitToBuild(buildManager.mageUnitPrefab);
    }

    public void SelectTankUnit()
    {
        Debug.Log("Tank Unit Selected");
        buildManager.SetUnitToBuild(buildManager.tankUnitPrefab);
    }

    public void SelectAssassinUnit()
    {
        Debug.Log("Assassin Unit Selected");
        buildManager.SetUnitToBuild(buildManager.assassinUnitPrefab);
    }
}
