using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [Header("Progress Bars")]
    [SerializeField] Slider progressBarHUD;
    [SerializeField] Slider progressBarMenu;
    [Header("Checkpoints")]
    [SerializeField] Transform currentChkpt;
    [SerializeField] List<Transform> Chkpts;
    [Header("Puzzles")]
    [SerializeField] GameObject slidePuzzle;
    [SerializeField] GameObject mazePuzzle;
    [SerializeField] GameObject riddlePuzzle;
    [Header("Overlays")]
    [SerializeField] Canvas uiOverlay;
    [SerializeField] Canvas pauseMenu;
    [SerializeField] Canvas victoryScreen;
    [Header("Audio")]
    [SerializeField] public AudioPlayer audioPlayer;
    StarterAssetsInputs starterAssetsInputs;
    SceneHandler sh;

    void Awake()
    {
        starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();
        sh = FindAnyObjectByType<SceneHandler>();
        // Debug.Log($"Scene Handler: {sh}");
    }
    void Start()
    {
        if (sh.GetRestarting())
        {
            starterAssetsInputs.EnableMouseLook(true);
            Continue();
        }
    }
    void Update()
    {
        HandleMenu();
    }
    void HandleMenu()
    {
        if (!starterAssetsInputs.pause) return;

        starterAssetsInputs.PauseInput(false);

        uiOverlay.enabled = false;
        pauseMenu.enabled = true;
        starterAssetsInputs.EnableMouseLook(false);
        player.GetComponent<PlayerInput>().enabled = false;
    }

    public void ActivateChkpt(int index)
    {
        
        Debug.Log($"Checkpoint Activated {index}");
        if (index > sh.GetChkPtIndex() || sh.GetRestarting())
        {
            sh.SetChkPtIndex(index);
            progressBarHUD.value = index;
            progressBarHUD.GetComponentInChildren<TextMeshProUGUI>().text = $"Progress {index/progressBarHUD.maxValue * 100 : 0}%";
            progressBarMenu.value = index;
            progressBarMenu.GetComponentInChildren<TextMeshProUGUI>().text = $"Progress {index/progressBarMenu.maxValue * 100 : 0}%";
        }
        if (index == progressBarMenu.maxValue)
        {
            OnVictory();
        }
    }
    void OnVictory()
    {
        audioPlayer.PlayVictoryClip();
        uiOverlay.enabled = false;
        victoryScreen.enabled = true;
        starterAssetsInputs.EnableMouseLook(false);
        player.GetComponent<PlayerInput>().enabled = false;
    }
    public void OnRestartClick()
    {
        audioPlayer.PlayClickClip();
        // Debug.Log("Restart");
        sh.HandleRestart();
        uiOverlay.enabled = true;
        pauseMenu.enabled = false;
        starterAssetsInputs.EnableMouseLook(true);
        player.GetComponent<PlayerInput>().enabled = true;
    }
    public void OnQuitClick()
    {
        audioPlayer.PlayClickClip();
        // Debug.Log("Quit");
        SceneManager.LoadScene("MainMenu");
    }
    public void OnCloseClick()
    {
        audioPlayer.PlayClickClip();
        // Debug.Log("Close");
        uiOverlay.enabled = true;
        pauseMenu.enabled = false;
        starterAssetsInputs.EnableMouseLook(true);
        player.GetComponent<PlayerInput>().enabled = true;
    }
    public void OnReturnClick()
    {
        audioPlayer.PlayClickClip();
        // Debug.Log("Return");
        uiOverlay.enabled = true;
        victoryScreen.enabled = false;
        starterAssetsInputs.EnableMouseLook(true);
        player.GetComponent<PlayerInput>().enabled = true;
    }
    // public void NewGame()
    // {
    //     sh.SetChkPtIndex(0);
    //     Continue();
    // }
    public void Continue()
    {   
        int index = sh.GetChkPtIndex();
        ActivateChkpt(index);
        currentChkpt = Chkpts[index].transform;
        player.transform.SetPositionAndRotation(currentChkpt.position, currentChkpt.rotation);
        if (index >= 1)
        {
            slidePuzzle.GetComponentInChildren<SlidePuzzle>().OnFinish();
            if (index >= 2)
            {
                mazePuzzle.GetComponentInChildren<MazeFinish>().OnFinish();
                if (index >= 3)
                {
                    mazePuzzle.GetComponentInChildren<MazeFinish>().OnSpecial();
                }
            }
        }
        sh.SetRestarting(false);
    }
}
