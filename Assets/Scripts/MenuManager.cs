using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI continueText;
    [SerializeField] GameObject newGameBtn;
    [SerializeField] GameObject continueBtn;
    SceneHandler sh;
    void Awake()
    {
        sh = FindAnyObjectByType<SceneHandler>();
        
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
        sh?.HandleRestart();
    }
    public void OnQuit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
