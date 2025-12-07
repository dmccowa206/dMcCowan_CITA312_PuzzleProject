using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler instance;
    static bool isRestarting = false;
    static int chkptIndex = 0;
    void Awake()
    {
        if (instance != null && instance != this)
             Destroy(this.gameObject);
         else
         {
             instance = this;
             DontDestroyOnLoad(this.gameObject);
         }
    }

    public void HandleRestart()
    {
        isRestarting = true;
        SceneManager.LoadScene("Rooms");
    }
    public int GetChkPtIndex()
    {
        return chkptIndex;
    }
    public void SetChkPtIndex(int index)
    {
        chkptIndex = index;
    }
    public bool GetRestarting()
    {
        return isRestarting;
    }
    public void SetRestarting(bool restart)
    {
        isRestarting = restart;
    }
}
