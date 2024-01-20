using Studio.Settings;
using UnityEngine;
using Zenject;

namespace Studio.ProjectSystems
{
    public class GameStateSystem : MonoBehaviour
    {
        public GameStates CurrentState { get; private set; } = GameStates.Unknown;

        public bool GameStarted { get; private set; } = false;

        [Inject]
        public void Construct()
        {
            Utilities.Logger.Log("GameStateSystem Construct", LogTypes.Info);
        }

        public void ChangeGameState(GameStates targetState)
        {
            if (CurrentState == targetState || targetState == GameStates.Unknown)
            {
                return;
            }

            CurrentState = targetState;
            EventBus.OnGameStateWasChangedEvent(CurrentState);
        }

        public void GameplayStarted()
        {
            GameStarted = true;

            EventBus.OnGameplayStartedEvent?.Invoke();
        }

        public void GameplayStoped()
        {
            GameStarted = false;

            EventBus.OnGameplayStopedEvent?.Invoke();
        }

        public void GameplayPaused(bool isOn)
        {
            EventBus.OnGameplayPausedEvent?.Invoke(isOn);
        }
    }
}