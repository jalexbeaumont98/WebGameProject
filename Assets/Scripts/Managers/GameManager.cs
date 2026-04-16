using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    public float MouseSensitivity = 120f;
    // Reset settings at the start of each game? 

    [Header("Mobile Controls")]
    [SerializeField] GameObject mobileControlsParent;

    public bool LoadGameFlag = false;

    void Start()
    {
#if UNITY_ANDROID || UNITY_EDITOR
    mobileControlsParent.SetActive(true);
#endif
    }


    public void LoadDeathSequence()
    {
        // Will move this later
        SceneManager.LoadScene("GameOver");
    }

    public void SetLoadFlag()
    {
        LoadGameFlag = true;
    }
}
