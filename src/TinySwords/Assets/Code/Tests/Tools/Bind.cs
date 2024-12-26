using Code.Gameplay.Common.Collisions;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.AI.Services;
using Code.Gameplay.Features.Battle.Services;
using Code.Gameplay.Features.CollectEntities.Services;
using Code.Gameplay.Features.Command.Services;
using Code.Gameplay.Features.Death.Factory;
using Code.Gameplay.Features.Indicators.Factory;
using Code.Gameplay.Features.Sounds.Factory;
using Code.Gameplay.Features.Sounds.Services;
using Code.Gameplay.Features.Units.Factory;
using Code.Gameplay.Features.Units.Services;
using Code.Gameplay.Level.Factory;
using Code.Gameplay.Services;
using Code.Gameplay.UtilityAI;
using Code.Gameplay.UtilityAI.Brains;
using Code.Gameplay.UtilityAI.Components;
using Code.Infrastructure.Common.CoroutineRunner;
using Code.Infrastructure.Common.Services;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Views.Factory;
using NSubstitute;
using UnityEngine;
using Zenject;

namespace Code.Tests.Tools
{
  public static class Bind
  {
    public static void GameContext(DiContainer diContainer) =>
      diContainer.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();

    public static void UnitAI(DiContainer diContainer)
    {
      diContainer.Bind<When>().To<When>().AsSingle();
      diContainer.Bind<GetInput>().To<GetInput>().AsSingle();
      diContainer.Bind<Score>().To<Score>().AsSingle();
      diContainer.Bind<IBrainsComponents>().To<BrainsComponents>().AsSingle();

      diContainer.Bind<UnitBrains>().To<UnitBrains>().AsSingle();
      diContainer.Bind<IUnitAI>().To<UnitAI>().AsSingle();
    }

    public static void SystemFactory(DiContainer diContainer) =>
      diContainer.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();

    public static void TimeService(DiContainer diContainer) =>
      diContainer.Bind<ITimeService>().To<TimeService>().AsSingle();

    public static void IdentifierService(DiContainer diContainer) =>
      diContainer.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();

    public static void EntityViewFactory(DiContainer diContainer) =>
      diContainer.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();

    public static void UnitFactory(DiContainer diContainer) =>
      diContainer.Bind<IUnitFactory>().To<UnitFactory>().AsSingle();

    public static void UnitFactoryStub(DiContainer diContainer)
    {
      IUnitFactory unitFactoryStub = Substitute.For<IUnitFactory>();
      diContainer.Bind<IUnitFactory>().FromInstance(unitFactoryStub).AsSingle();
    }

    public static void StaticDataService(DiContainer diContainer) =>
      diContainer.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();

    public static IStaticDataService StaticDataServiceStub(DiContainer diContainer)
    {
      IStaticDataService staticDataStub = Substitute.For<IStaticDataService>();
      diContainer.Bind<IStaticDataService>().FromInstance(staticDataStub).AsSingle();
      return staticDataStub;
    }

    public static void AttackAnimationService(DiContainer diContainer) =>
      diContainer.Bind<IAttackAnimationService>().To<AttackAnimationService>().AsSingle();

    public static void CollisionRegistry(DiContainer diContainer) =>
      diContainer.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle();

    public static void CollisionRegistryStub(DiContainer diContainer)
    {
      ICollisionRegistry collisionRegistryStub = Substitute.For<ICollisionRegistry>();
      diContainer.Bind<ICollisionRegistry>().FromInstance(collisionRegistryStub).AsSingle();
    }

    public static void LevelFactory(DiContainer diContainer) =>
      diContainer.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();

    public static void PhysicsService(DiContainer diContainer) =>
      diContainer.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();

    public static void DecisionService(DiContainer diContainer) =>
      diContainer.Bind<IDecisionService>().To<DecisionService>().AsSingle();

    public static void CameraProvider(DiContainer diContainer) =>
      diContainer.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();

    public static void IndicatorFactory(DiContainer diContainer) =>
      diContainer.Bind<IIndicatorFactory>().To<IndicatorFactory>().AsSingle();

    public static void SoundService(DiContainer diContainer) =>
      diContainer.Bind<ISoundService>().To<SoundService>().AsSingle();

    public static void SoundFactory(DiContainer diContainer) =>
      diContainer.Bind<ISoundFactory>().To<SoundFactory>().AsSingle();

    public static void UnitDeathFactory(DiContainer diContainer) =>
      diContainer.Bind<IUnitDeathFactory>().To<UnitDeathFactory>().AsSingle();

    public static void SelectableCommandService(DiContainer diContainer) =>
      diContainer.Bind<ISelectableCommandService>().To<SelectableCommandService>().AsSingle();

    public static void RecruitUnitService(DiContainer diContainer) =>
      diContainer.Bind<IRecruitUnitService>().To<RecruitUnitService>().AsSingle();

    public static void CoroutineRunner(DiContainer diContainer)
    {
      CoroutineRunner coroutineRunner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
      Object.DontDestroyOnLoad(coroutineRunner);
      diContainer.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromInstance(coroutineRunner).AsSingle();
    }

    public static void DelayService(DiContainer diContainer) =>
      diContainer.Bind<IDelayService>().To<DelayService>().AsSingle();

    public static void CollectEntityService(DiContainer diContainer) =>
      diContainer.Bind<ICollectEntityService>().To<CollectEntityService>().AsSingle();
  }
}
