using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;

public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager instance;
    public static GameSceneManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameSceneManager>();
            return instance;
        }
    }

    [SerializeField] ScreenTint screenTint;
    [SerializeField] CameraConfiner cameraConfiner;
    [SerializeField] Collider2D homeConfiner;
    [SerializeField] List<string> insideScene;
    [SerializeField] List<string> indarkScene;
    string currentScene;
    AsyncOperation unload;
    AsyncOperation load;

    bool respawnTransition;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    internal void ReSpawn(Vector3 respawnPointPosition, string respawnPointScene)
    {
        respawnTransition = true;
        if (currentScene != respawnPointScene)
        {
            InitSwitchScene(respawnPointScene, respawnPointPosition);
        }
        else
        {
            MovePlayerToHome(respawnPointPosition);
            GamesManager.Instance.dayTimeController.PlayerInside(true);
        }
    }

    internal string GetCurrentScene()
    {
        return currentScene;
    }

    public void InitSwitchScene(string toScene, Vector3 targetPosition)
    {
        StartCoroutine(Transition(toScene, targetPosition));
    }


    IEnumerator Transition(string toScene, Vector3 targetPosition)
    {
        screenTint.Tint();

        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f); // 0.1 iz time offset

        SwitchScene(toScene, targetPosition);

        while (load != null && unload != null)
        {
            if (load.isDone) { load = null; }
            if (unload.isDone) { unload = null; }
            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));

        cameraConfiner.UpdateBounds();
        screenTint.UnTint();

        if (insideScene.Contains(toScene))
        {
            GamesManager.Instance.dayTimeController.PlayerInside(true);
        }
        else
        {
            GamesManager.Instance.dayTimeController.PlayerInside(false);
            if (indarkScene.Contains(toScene))
            {
                GamesManager.Instance.dayTimeController.PlayerInDark(true);
            }
            else
            {
                GamesManager.Instance.dayTimeController.PlayerInDark(false);
            }
            
        }
        QuestManager.Instance.VisualizeQuest();
        PlayerPetManager.Instance.VisualizePet();
    }

    public void SwitchScene(string toScene, Vector3 targetPosition)
    {
        load = SceneManager.LoadSceneAsync(toScene, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);
        currentScene = toScene;
        MovePlayer(targetPosition);
    }

    private void MovePlayer(Vector3 targetPosition)
    {
        Transform playerTransform = GamesManager.Instance.player.transform;

        CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();

        //if (homeConfiner == null)
        //{
        //    homeConfiner = GameObject.Find("HomeConfiner").GetComponent<Collider2D>();
        //}
        //if (homeConfiner == null) { return; }
        //cameraConfiner.UpdateNewBounds(homeConfiner);

        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(
            playerTransform,
            targetPosition - playerTransform.position
        );

        playerTransform.position = new Vector3(
            targetPosition.x,
            targetPosition.y,
            playerTransform.position.z);

        if (respawnTransition)
        {
            PlayerManager.Instance.FullHeal();
            PlayerManager.Instance.FullRest();
            DisableControls.Instance.EnableControl();
            respawnTransition = false;
        }
    }

    private void MovePlayerToHome(Vector3 targetPosition)
    {
        Transform playerTransform = GamesManager.Instance.player.transform;

        CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();

        if (homeConfiner == null)
        {
            homeConfiner = GameObject.Find("HomeConfiner").GetComponent<Collider2D>();
        }
        if (homeConfiner == null) { return; }
        cameraConfiner.UpdateNewBounds(homeConfiner);

        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(
            playerTransform,
            targetPosition - playerTransform.position
        );

        playerTransform.position = new Vector3(
            targetPosition.x,
            targetPosition.y,
            playerTransform.position.z);

        if (respawnTransition)
        {
            PlayerManager.Instance.FullHeal();
            PlayerManager.Instance.FullRest();
            DisableControls.Instance.EnableControl();
            respawnTransition = false;
        }
    }
}
