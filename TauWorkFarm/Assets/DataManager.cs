using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<DataManager>();
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [SerializeField] ItemContainer inventoryContainer;
    [SerializeField] RecipeList knownRecipeList;
    [SerializeField] MissionContainer missionContainer;
    [SerializeField] PlayerData playerData;
    [SerializeField] List<CropsContainer> cropsContainers;
    [SerializeField] List<PlaceableObjectsContainer> placeableObjectsContainers;
    [SerializeField] List<DirtsContainer> dirtsContainers;
    [SerializeField] List<FencesContainer> fencesContainers;
    [SerializeField] List<JSONStringList> jSONStringLists;

    public void NewData()
    {
        inventoryContainer.Clear();
        knownRecipeList.Clear();
        missionContainer.Clear();
        playerData.ReSet();
        for (int i = 0; i < cropsContainers.Count; i++)
        {
            cropsContainers[i].Clear();
        }
        for (int i = 0; i < placeableObjectsContainers.Count; i++)
        {
            placeableObjectsContainers[i].Clear();
        }
        for (int i = 0; i < dirtsContainers.Count; i++)
        {
            dirtsContainers[i].Clear();
        }
        for (int i = 0; i < fencesContainers.Count; i++)
        {
            fencesContainers[i].Clear();
        }
        for (int i = 0; i < jSONStringLists.Count; i++)
        {
            jSONStringLists[i].Clear();
        }
    }
}
