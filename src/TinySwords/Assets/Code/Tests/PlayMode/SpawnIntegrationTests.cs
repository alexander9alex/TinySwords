using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Collisions;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Destruct;
using Code.Gameplay.Features.Units.Factory;
using Code.Gameplay.Level;
using Code.Gameplay.Level.Configs;
using Code.Gameplay.Level.Factory;
using Code.Gameplay.Services;
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
      Container.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();
      Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
      Container.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();
      Container.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle();
      Container.Bind<ITimeService>().To<TimeService>().AsSingle();
    }

    [UnityTest]
    public IEnumerator WhenCreateLevel_ThenLevelShouldBeSpawnOnScene()
    {
      // Arrange
      IUnitFactory unitFactoryStub = Substitute.For<IUnitFactory>();
      Container.Bind<IUnitFactory>().FromInstance(unitFactoryStub).AsSingle();

      IStaticDataService staticData = Substitute.For<IStaticDataService>();
      staticData.GetLevelConfig().Returns(Resources.Load<LevelConfig>("Editor/ForTests/LevelConfig"));
      Container.Bind<IStaticDataService>().FromInstance(staticData).AsSingle();
      
      EditorSceneManager.LoadSceneInPlayMode(EmptyTestScenePath, new(LoadSceneMode.Single));
      yield return null;

      BindViewFeature bindViewFeature = Container.Resolve<ISystemFactory>().Create<BindViewFeature>();
      bindViewFeature.Initialize();
      
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
