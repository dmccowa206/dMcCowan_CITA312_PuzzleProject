using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Click")]
    [SerializeField] AudioClip clickSfx;
    [SerializeField] [Range(0f, 1f)] float clickVol = 1f;
    [Header("Puzzle Solved")]
    [SerializeField] AudioClip puzzleSolvedSfx;
    [SerializeField] [Range(0f, 1f)] float puzzVol = 1f;
    [Header("Secret Solved")]
    [SerializeField] AudioClip secretSolvedSfx;
    [SerializeField] [Range(0f, 1f)] float secretVol = 1f;
    [Header("Victory")]
    [SerializeField] AudioClip victorySfx;
    [SerializeField] [Range(0f, 1f)] float victoryVol = 1f;


    public void PlayClickClip()
    {
        PlayClip(clickSfx,clickVol);
    }
    public void PlayPuzzleClip()
    {
        PlayClip(puzzleSolvedSfx, puzzVol);
    }
    public void PlaySecretClip()
    {
        PlayClip(secretSolvedSfx, secretVol);
    }
    public void PlayVictoryClip()
    {
        PlayClip(victorySfx, victoryVol);
    }
    public void PlayClip(AudioClip soundClip, float vol)
    {
        if(soundClip != null)
        {
            AudioSource.PlayClipAtPoint(soundClip, Camera.main.transform.position, vol);
        }
    }
}
