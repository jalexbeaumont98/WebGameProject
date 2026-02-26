public interface IModeState
{
    void Enter();        // Called when the state becomes active
    void Exit();         // Called when leaving the state

    void Tick();         // Called every Update
    void FixedTick();    // Called every FixedUpdate
}
