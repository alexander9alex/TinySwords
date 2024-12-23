using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Destruct;
using Code.Gameplay.Features.NavMesh.Registrars;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Features.Units.Factory;
using Code.Gameplay.Level.Data;
using Code.Gameplay.Level.Factory;
using Code.Gameplay.Services;
using Code.Infrastructure.Factory;
using Code.Tests.Tools;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;
using static Code.Tests.Tools.Constants;

namespace Code.Tests.PlayMode.UnitsBehaviour
{
  public class KnightBehaviourTests : ZenjectUnitTestFixture
  {
    [SetUp]
    public void InstallBindings()
    {
      Bind.GameContext(Container);
      Bind.UnitFactory(Container);
      Bind.StaticDataService(Container);
      Bind.TimeService(Container);
      Bind.IdentifierService(Container);
      Bind.SystemFactory(Container);
      Bind.UnitAI(Container);
      Bind.EntityViewFactory(Container);
      Bind.PhysicsService(Container);
      Bind.CollisionRegistry(Container);
      Bind.DecisionService(Container);
      Bind.CameraProvider(Container);
      Bind.IndicatorFactory(Container);
      Bind.SoundService(Container);
      Bind.SoundFactory(Container);
      Bind.UnitDeathFactory(Container);
      Bind.AttackAnimationService(Container);
      Bind.LevelFactory(Container);

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
    public IEnumerator WhenUnitHasMoveCommand_ThenUnitShouldMoveToEndDestination()
    {
      // Arrange
      EditorSceneManager.LoadSceneInPlayMode(EmptyTestScenePath, new(LoadSceneMode.Single));
      yield return null;

      BindNavMesh();
      
      Container.Resolve<ILevelFactory>().CreateLevel(LevelId.Empty);

      Vector2 destinationPosition = new(2, 1);
      GameEntity knight = Container.Resolve<IUnitFactory>().CreateUnit(UnitTypeId.Knight, TeamColor.Blue, Vector3.zero);
      
      ITimeService timeService = Container.Resolve<ITimeService>();
      timeService.TimeScale = 10;

      UnitBehaviourFeature unitBehaviourFeature = Container.Resolve<ISystemFactory>().Create<UnitBehaviourFeature>();
      unitBehaviourFeature.Initialize();
      unitBehaviourFeature.Execute();
      unitBehaviourFeature.Cleanup();

      knight.ReplaceUserCommand(new() { CommandTypeId = CommandTypeId.Move, WorldPosition = destinationPosition });
      unitBehaviourFeature.Execute();
      unitBehaviourFeature.Cleanup();

      // Act
      float timer = 0;

      while (knight.hasDestination && timer <= 3)
      {
        unitBehaviourFeature.Execute();
        unitBehaviourFeature.Cleanup();
        yield return null;
        timer += timeService.DeltaTime;
      }

      // Assert
      Vector2.Distance(knight.WorldPosition, destinationPosition).Should().BeLessThanOrEqualTo(MaxPositionDelta);
    }

    private void BindNavMesh()
    {
      SelfInitializedEntityView navMeshInitializer = AllGameObjects(SceneManager.GetActiveScene())
        .Single(x => x.GetComponent<NavMeshRegistrar>())
        .GetComponent<SelfInitializedEntityView>();

      Container.Inject(navMeshInitializer);
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
