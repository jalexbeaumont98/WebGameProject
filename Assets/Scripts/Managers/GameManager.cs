using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Options/Settings")] 
    public float MouseSensitivity = 120f;
    // Reset settings at the start of each game.

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
            Instance = this;
    }

}
