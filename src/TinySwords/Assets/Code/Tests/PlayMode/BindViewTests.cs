using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Destruct;
using Code.Gameplay.Features.Units.Animators;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Features.Units.Factory;
using Code.Gameplay.Level;
using Code.Gameplay.Level.Data;
using Code.Gameplay.Level.Factory;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Views;
using Code.Tests.Tools;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;
using static Code.Tests.Tools.Constants;

namespace Code.Tests.PlayMode
{
  public class BindViewTests : ZenjectUnitTestFixture
  {
    [SetUp]
    public void InstallBindings()
    {
      Bind.GameContext(Container);
      Bind.SystemFactory(Container);
      Bind.TimeService(Container);
      Bind.StaticDataService(Container);
      Bind.IdentifierService(Container);
      Bind.EntityViewFactory(Container);

      Bind.CollisionRegistryStub(Container);
      
      Container.Resolve<IStaticDataService>().LoadAll();
    }

    [TearDown]
    public void TearDown()
    {
      Destruct.AllEntities(
        Container.Resolve<ISystemFactory>().Create<ProcessDestructedFeature>(),
        Container.Resolve<GameContext>());
    }

    [UnityTest]
    public IEnumerator WhenCreateLevel_ThenLevelShouldBeSpawnOnScene()
    {
      // Arrange
      Bind.UnitFactoryStub(Container);
      Bind.LevelFactory(Container);

      EditorSceneManager.LoadSceneInPlayMode(EmptyTestScenePath, new(LoadSceneMode.Single));
      yield return null;

      BindViewFeature bindViewFeature = Container.Resolve<ISystemFactory>().Create<BindViewFeature>();
      bindViewFeature.Initialize();

      ILevelFactory levelFactory = Container.Resolve<ILevelFactory>();

      // Act
      levelFactory.CreateLevel(LevelId.Empty);
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
      Bind.UnitAI(Container);
      Bind.AttackAnimationService(Container);
      Bind.UnitFactory(Container);
      
      EditorSceneManager.LoadSceneInPlayMode(EmptyTestScenePath, new(LoadSceneMode.Single));
      yield return null;

      BindViewFeature bindViewFeature = Container.Resolve<ISystemFactory>().Create<BindViewFeature>();
      bindViewFeature.Initialize();

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
