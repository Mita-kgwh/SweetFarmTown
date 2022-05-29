using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string nameEssentialScene;
    [SerializeField] string nameNewGameStartScene;

    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    } 

    public void StartNewGame()
    {
        SceneManager.LoadScene(nameNewGameStartScene, LoadSceneMode.Single);
        SceneManager.LoadScene(nameEssentialScene, LoadSceneMode.Additive);
        
    }

    public void LoadGame()
    {
        PlayerData data = DataManager.Instance.GetPlayerData();
        SceneManager.LoadScene(data.curSceneName, LoadSceneMode.Single);
        SceneManager.LoadScene(nameEssentialScene, LoadSceneMode.Additive);
    }
}
