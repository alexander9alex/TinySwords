using Code.Gameplay.Common.Collisions;
using Code.Gameplay.Common.Curtain;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.CutScene.Factory;
using Code.Gameplay.CutScene.Services;
using Code.Gameplay.Features.AI.Services;
using Code.Gameplay.Features.Battle.Services;
using Code.Gameplay.Features.Cameras.Factory;
using Code.Gameplay.Features.Cameras.Services;
using Code.Gameplay.Features.CollectEntities.Services;
using Code.Gameplay.Features.Command.Services;
using Code.Gameplay.Features.Death.Factory;
using Code.Gameplay.Features.FogOfWar.Factory;
using Code.Gameplay.Features.FogOfWar.Services;
using Code.Gameplay.Features.Indicators.Factory;
using Code.Gameplay.Features.Input.Factory;
using Code.Gameplay.Features.Input.Services;
using Code.Gameplay.Features.Lose.Services;
using Code.Gameplay.Features.Move.Services;
using Code.Gameplay.Features.ProcessCommand.Services;
using Code.Gameplay.Features.Sounds.Factory;
using Code.Gameplay.Features.Sounds.Services;
using Code.Gameplay.Features.Units.Factory;
using Code.Gameplay.Features.Units.Services;
using Code.Gameplay.Features.Win.Services;
using Code.Gameplay.Level.Factory;
using Code.Gameplay.Services;
using Code.Gameplay.Tutorials.Services;
using Code.Gameplay.UtilityAI;
using Code.Gameplay.UtilityAI.Brains;
using Code.Gameplay.UtilityAI.Components;
using Code.Infrastructure.Common.CoroutineRunner;
using Code.Infrastructure.Common.Services;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.Views.Factory;
using Code.UI.Hud.Factory;
using Code.UI.Hud.Service;
using Code.UI.Windows.Factory;
using Code.UI.Windows.Services;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    public Curtain Curtain;

    public override void InstallBindings()
    {
      BindInfrastructureServices();
      BindInfrastructureFactories();
      BindCoroutineRunner();
      BindCommonServices();
      BindContexts();
      BindUIServices();
      BindUIFactories();
      BindGameplayServices();
      BindGameplayFactories();
      BindAI();
      BindSystemFactory();
      BindStateFactory();
      BindGameStates();
      BindStateMachine();
    }

    private void BindInfrastructureServices()
    {
      Container.BindInterfacesAndSelfTo<BootstrapInstaller>().FromInstance(this).AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
      Container.Bind<IDelayService>().To<DelayService>().AsSingle();
    }

    private void BindInfrastructureFactories()
    {
      Container.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();
    }

    private void BindCoroutineRunner()
    {
      CoroutineRunner coroutineRunner = new GameObject(nameof(CoroutineRunner)).AddComponent<CoroutineRunner>();
      DontDestroyOnLoad(coroutineRunner);
      Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromInstance(coroutineRunner).AsSingle();
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
      Container.Bind<IAttackAnimationService>().To<AttackAnimationService>().AsSingle();
      Container.Bind<ISoundService>().To<SoundService>().AsSingle();
      Container.Bind<IBattleFormationService>().To<BattleFormationService>().AsSingle();
      Container.Bind<IDecisionService>().To<DecisionService>().AsSingle();
      Container.Bind<IRecruitUnitService>().To<RecruitUnitService>().AsSingle();
      Container.Bind<ICollectEntityService>().To<CollectEntityService>().AsSingle();
      Container.Bind<ICutSceneService>().To<CutSceneService>().AsSingle();
      Container.Bind<IFogOfWarService>().To<FogOfWarService>().AsSingle();
      Container.Bind<IWindowService>().To<WindowService>().AsSingle();
      Container.Bind<ITutorialService>().To<TutorialService>().AsSingle();
      Container.Bind<IWinService>().To<WinService>().AsSingle();
      Container.Bind<ILoseService>().To<LoseService>().AsSingle();

      Container.Bind<ICommandService>().To<CommandService>().AsSingle();
      Container.Bind<IProcessCommandService>().To<ProcessCommandService>().AsSingle();
      Container.Bind<ISelectableCommandService>().To<SelectableCommandService>().AsSingle();

      Container.Bind<ICameraScalingService>().To<CameraScalingService>().AsSingle();
      Container.Bind<ICameraMovementService>().To<CameraMovementService>().AsSingle();
    }

    private void BindGameplayFactories()
    {
      Container.Bind<IUnitFactory>().To<UnitFactory>().AsSingle();
      Container.Bind<IIndicatorFactory>().To<IndicatorFactory>().AsSingle();
      Container.Bind<IUnitDeathFactory>().To<UnitDeathFactory>().AsSingle();
      Container.Bind<ISoundFactory>().To<SoundFactory>().AsSingle();
      Container.Bind<ICutSceneFactory>().To<CutSceneFactory>().AsSingle();
      Container.Bind<IFogOfWarFactory>().To<FogOfWarFactory>().AsSingle();
      Container.Bind<ICameraFactory>().To<CameraFactory>().AsSingle();
      Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
    }

    private void BindAI()
    {
      Container.Bind<When>().To<When>().AsSingle();
      Container.Bind<GetInput>().To<GetInput>().AsSingle();
      Container.Bind<Score>().To<Score>().AsSingle();
      Container.Bind<IBrainsComponents>().To<BrainsComponents>().AsSingle();

      Container.Bind<UnitBrains>().To<UnitBrains>().AsSingle();
      Container.Bind<IUnitAI>().To<UnitAI>().AsSingle();
    }

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadProgressState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadingMainMenuState>().AsSingle();
      Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadingCutSceneState>().AsSingle();
      Container.BindInterfacesAndSelfTo<CutSceneState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadingLevelState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LevelState>().AsSingle();
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
