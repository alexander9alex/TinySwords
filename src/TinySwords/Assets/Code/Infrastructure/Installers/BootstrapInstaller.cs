using Code.Gameplay.Common.Collisions;
using Code.Gameplay.Common.Curtain;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Build.Factory;
using Code.Gameplay.Features.Input.Factory;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Features.Move.Factory;
using Code.Gameplay.Features.Units.Factory;
using Code.Gameplay.Level.Factory;
using Code.Gameplay.Services;
using Code.Infrastructure.Common.CoroutineRunner;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.Views.Factory;
using Code.UI.Hud.Factory;
using Code.UI.Hud.Service;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable, ICoroutineRunner
  {
    public Curtain Curtain;

    public override void InstallBindings()
    {
      BindInfrastructureServices();
      BindInfrastructureFactories();
      BindCommonServices();
      BindContexts();
      BindUIServices();
      BindUIFactories();
      BindGameplayServices();
      BindGameplayFactories();
      BindSystemFactory();
      BindStateFactory();
      BindGameStates();
      BindStateMachine();
    }

    private void BindInfrastructureServices()
    {
      Container.BindInterfacesAndSelfTo<BootstrapInstaller>().FromInstance(this).AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
    }

    private void BindInfrastructureFactories()
    {
      Container.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();
    }

    private void BindCommonServices()
    {
      Container.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();
      Container.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle();
      Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
      Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
    }

    private void BindContexts()
    {
      Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();
      
      Container.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
    }

    private void BindUIServices()
    {
      Container.Bind<IHudService>().To<HudService>().AsSingle();
    }

    private void BindUIFactories()
    {
      Container.Bind<IHighlightFactory>().To<HighlightFactory>().AsSingle();
      Container.Bind<IHudFactory>().To<HudFactory>().AsSingle();
    }
    
    private void BindGameplayServices()
    {
      Container.Bind<ICurtain>().FromInstance(Curtain).AsSingle();
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
      Container.Bind<ITimeService>().To<TimeService>().AsSingle();
      Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
    }

    private void BindGameplayFactories()
    {
      Container.Bind<IUnitFactory>().To<UnitFactory>().AsSingle();
      Container.Bind<IMoveClickIndicatorFactory>().To<MoveClickIndicatorFactory>().AsSingle();
      Container.Bind<IBuildingFactory>().To<BuildingFactory>().AsSingle();
    }

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadProgressState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadingMainMenuState>().AsSingle();
      Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadingGameState>().AsSingle();
      Container.BindInterfacesAndSelfTo<GameLoopState>().AsSingle();
    }

    private void BindSystemFactory() =>
      Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();

    private void BindStateFactory() =>
      Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();

    private void BindStateMachine() =>
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

    public void Initialize() =>
      Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
  }
}
