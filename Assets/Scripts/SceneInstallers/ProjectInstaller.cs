using Studio.ProjectSystems;
using Studio.Settings;
using Zenject;

namespace Studio
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LoadObjectsSystem>().FromComponentInHierarchy().AsCached();
            Container.Bind<InputsSystem>().FromComponentInHierarchy().AsCached();
            Container.Bind<DataSystem>().FromComponentInHierarchy().AsCached();
            Container.Bind<LocalisationSystem>().FromComponentInHierarchy().AsCached();
            Container.Bind<SoundSystem>().FromComponentInHierarchy().AsCached();
            Container.Bind<UISystem>().FromComponentInHierarchy().AsCached();
            Container.Bind<SceneSystem>().FromComponentInHierarchy().AsCached();

            Container.Bind<GameStateSystem>().FromComponentInHierarchy().AsCached();
        }

        public override void Start()
        {
            EventBus.OnSystemsBindedEvent?.Invoke();
        }
    }
}