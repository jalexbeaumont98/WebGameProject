using UnityEngine;

public class AchievementSystem : MonoBehaviour
{

    [SerializeField] private VoidEventChannel voidChannel;



    private int achievementJumps = 10;
    private int currentJumps = 0;

    void OnEnable()
    {
        voidChannel.OnEventRaised += EventCalled;
    }

    private void OnDisable()
    {
        voidChannel.OnEventRaised -= EventCalled;
    }

    private void EventCalled()
    {
        Debug.Log("Event called by listening to the event channel of void type");
        currentJumps++;
        if (currentJumps > achievementJumps)
        {
            Debug.Log("Achievement earned! Too many jumps!");
        }
    }

}