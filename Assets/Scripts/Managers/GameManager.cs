using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Options/Settings")] 
    public float MouseSensitivity = 120f;
    // Reset settings at the start of each game? 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public void LoadDeathSequence()
    {
        // Will move this later
        SceneManager.LoadScene("GameOver");
    }
}
