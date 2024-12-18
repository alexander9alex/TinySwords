using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Collisions;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Battle.Services;
using Code.Gameplay.Features.Destruct;
using Code.Gameplay.Features.Units.Animators;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Features.Units.Factory;
using Code.Gameplay.Level;
using Code.Gameplay.Level.Configs;
using Code.Gameplay.Level.Factory;
using Code.Gameplay.Services;
using Code.Gameplay.UtilityAI;
using Code.Gameplay.UtilityAI.Brains;
using Code.Gameplay.UtilityAI.Components;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Views;
using Code.Infrastructure.Views.Factory;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace Code.Tests.PlayMode
{
  public class SpawnIntegrationTests : ZenjectUnitTestFixture
  {
    private const string EmptyTestScenePath = "Assets/Scenes/ForTesting/EmptyScene.unity";

    [SetUp]
    public void InstallBindings()
    {
      Container.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
      Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
      Container.Bind<ITimeService>().To<TimeService>().AsSingle();

      Container.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();
      Container.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();
      Container.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle();
    }

    [UnityTest]
    public IEnumerator WhenCreateLevel_ThenLevelShouldBeSpawnOnScene()
    {
      // Arrange
      IUnitFactory unitFactoryStub = Substitute.For<IUnitFactory>();
      Container.Bind<IUnitFactory>().FromInstance(unitFactoryStub).AsSingle();

      IStaticDataService staticData = Substitute.For<IStaticDataService>();
      staticData.GetLevelConfig().Returns(Resources.Load<LevelConfig>("Editor/ForTests/EmptyLevelConfig"));
      Container.Bind<IStaticDataService>().FromInstance(staticData).AsSingle();

      EditorSceneManager.LoadSceneInPlayMode(EmptyTestScenePath, new(LoadSceneMode.Single));
      yield return null;

      BindViewFeature bindViewFeature = Container.Resolve<ISystemFactory>().Create<BindViewFeature>();
      bindViewFeature.Initialize();

      Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
      ILevelFactory levelFactory = Container.Resolve<ILevelFactory>();

      // Act
      levelFactory.CreateLevel();
      bindViewFeature.Execute();
      yield return null;

      // Assert
      AllGameObjects(SceneManager.GetActiveScene())
        .Where(x => x.GetComponent<LevelMap>())
        .Should().HaveCount(1);
    }

    [UnityTest]
    public IEnumerator WhenCreateBlueKnight_ThenUnitShouldBeSpawnOnScene()
    {
      // Arrange
      BindUnitAI();
      
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
      Container.Resolve<IStaticDataService>().LoadAll();
      
      Container.Bind<IAttackAnimationService>().To<AttackAnimationService>().AsSingle();

      EditorSceneManager.LoadSceneInPlayMode(EmptyTestScenePath, new(LoadSceneMode.Single));
      yield return null;

      BindViewFeature bindViewFeature = Container.Resolve<ISystemFactory>().Create<BindViewFeature>();
      bindViewFeature.Initialize();

      Container.Bind<IUnitFactory>().To<UnitFactory>().AsSingle();
      IUnitFactory unitFactory = Container.Resolve<IUnitFactory>();

      // Act
      unitFactory.CreateUnit(UnitTypeId.Knight, TeamColor.Blue, Vector3.zero);
      bindViewFeature.Execute();
      yield return null;

      // Assert
      AllGameObjects(SceneManager.GetActiveScene())
        .Where(x => x.GetComponent<KnightAnimator>())
        .Should().HaveCount(1);
    }

    [TearDown]
    public void TearDown()
    {
      GameContext gameContext = Container.Resolve<GameContext>();

      ProcessDestructedFeature processDestructedFeature = Container.Resolve<ISystemFactory>().Create<ProcessDestructedFeature>();

      foreach (GameEntity entity in gameContext.GetEntities())
        entity.isDestructed = true;

      processDestructedFeature.Execute();
      processDestructedFeature.Cleanup();
    }

    private void BindUnitAI()
    {
      Container.Bind<When>().To<When>().AsSingle();
      Container.Bind<GetInput>().To<GetInput>().AsSingle();
      Container.Bind<Score>().To<Score>().AsSingle();
      Container.Bind<IBrainsComponents>().To<BrainsComponents>().AsSingle();

      Container.Bind<UnitBrains>().To<UnitBrains>().AsSingle();
      Container.Bind<IUnitAI>().To<UnitAI>().AsSingle();
    }

    private static IEnumerable<GameObject> AllGameObjects(Scene scene)
    {
      Queue<GameObject> gameObjectQueue = new(scene.GetRootGameObjects());

      while (gameObjectQueue.Count > 0)
      {
        GameObject gameObject = gameObjectQueue.Dequeue();

        yield return gameObject;

        foreach (Transform child in gameObject.transform)
          gameObjectQueue.Enqueue(child.gameObject);
      }
    }
  }
}
