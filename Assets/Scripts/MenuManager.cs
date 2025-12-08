using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI continueText;
    [SerializeField] GameObject newGameBtn;
    [SerializeField] GameObject continueBtn;
    AudioSource audioSource;
    SceneHandler sh;
    void Awake()
    {
        sh = FindAnyObjectByType<SceneHandler>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        if (sh != null)
        {
            continueText.text = $"Continue\n{sh.GetChkPtIndex()} / 3 Rooms";
        }
        else
        {
            continueText.text = "";
            continueBtn.SetActive(false);
            Vector3 noContNewGameLoc = new Vector3(newGameBtn.transform.localPosition.x,
                newGameBtn.transform.localPosition.y - 150f, newGameBtn.transform.localPosition.z);
            newGameBtn.transform.SetLocalPositionAndRotation(noContNewGameLoc, newGameBtn.transform.rotation);
        }
    }
    public void OnNewGame()
    {
        audioSource.Play();
        if (sh != null)
        {
            sh?.SetChkPtIndex(0);
            sh?.HandleRestart();
        }
        else
        {
            SceneManager.LoadScene("Rooms");
        }
    }
    public void OnContinue()
    {
        audioSource.Play();
        sh?.HandleRestart();
    }
    public void OnQuit()
    {
        audioSource.Play();
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
