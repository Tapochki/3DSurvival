using Studio.Settings;
using UnityEngine;
using Zenject;

namespace Studio.ProjectSystems
{
    public class InputsSystem : MonoBehaviour
    {
        private GameStateSystem _gameStateSystem;

        [Inject]
        public void Construct(GameStateSystem gameStateSystem)
        {
            Utilities.Logger.Log("InputsSystem Construct", LogTypes.Info);

            _gameStateSystem = gameStateSystem;
        }

        public void Initialize()
        {
        }

        public void Update()
        {
            if (_gameStateSystem.GameStarted)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    EventBus.OnEscapeButtonDownEvent?.Invoke();
                }
            }
        }
    }
}