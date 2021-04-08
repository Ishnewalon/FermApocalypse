using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Events
    {
        [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState>
        {
        }
    }
}
