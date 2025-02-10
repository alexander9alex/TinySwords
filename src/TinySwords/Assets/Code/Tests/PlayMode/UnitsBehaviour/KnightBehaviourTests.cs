using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Destruct;
using Code.Gameplay.Features.NavMesh.Registrars;
using Code.Gameplay.Features.Units.Configs;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Features.Units.Factory;
using Code.Gameplay.Level.Configs;
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
      Bind.SelectableCommandService(Container);
      Bind.RecruitUnitService(Container);
      Bind.CoroutineRunner(Container);
      Bind.DelayService(Container);
      Bind.CollectEntityService(Container);
      Bind.FogOfWarFactory(Container);

      Container.Resolve<IStaticDataService>().LoadAll();
      Container.Resolve<ITimeService>().TimeScale = 10;
    }

    [TearDown]
    public void TearDown()
    {
      Destruct.AllEntities(
        Container.Resolve<ISystemFactory>().Create<ProcessDestructedFeature>(),
        Container.Resolve<GameContext>());

      Destruct.CoroutineRunner();
    }

    [UnityTest]
    public IEnumerator WhenKnightHasMoveCommand_ThenKnightShouldMoveToEndDestination()
    {
      // Arrange
      EditorSceneManager.LoadSceneInPlayMode(EmptyTestScenePath, new(LoadSceneMode.Single));
      yield return null;

      BindNavMesh();
      
      LevelConfig config = Container.Resolve<IStaticDataService>().GetLevelConfig(LevelId.Empty);
      Container.Resolve<ILevelFactory>().CreateLevel(config);

      ITimeService timeService = Container.Resolve<ITimeService>();

      Vector2 destinationPosition = new(2, 1);

      GameEntity knight = Container.Resolve<IUnitFactory>().CreateUnit(UnitTypeId.Knight, TeamColor.Blue, Vector3.zero);
      knight.ReplaceUserCommand(new() { CommandTypeId = CommandTypeId.Move, WorldPosition = destinationPosition });

      UnitBehaviourFeature unitBehaviourFeature = Container.Resolve<ISystemFactory>().Create<UnitBehaviourFeature>();
      unitBehaviourFeature.Initialize();

      // Act
      float timer = 0;

      while (knight.hasUserCommand && timer <= 3)
      {
        unitBehaviourFeature.Execute();
        unitBehaviourFeature.Cleanup();
        yield return null;
        timer += timeService.DeltaTime;
      }

      // Assert
      Vector2.Distance(knight.WorldPosition, destinationPosition).Should().BeLessThanOrEqualTo(MaxPositionDelta);
    }

    [UnityTest]
    public IEnumerator WhenKnightHasTarget_ThenKnightShouldMoveToTargetAndHitHim()
    {
      // Arrange
      EditorSceneManager.LoadSceneInPlayMode(EmptyTestScenePath, new(LoadSceneMode.Single));
      yield return null;

      BindNavMesh();

      LevelConfig config = Container.Resolve<IStaticDataService>().GetLevelConfig(LevelId.Empty);
      Container.Resolve<ILevelFactory>().CreateLevel(config);

      GameEntity knight = Container.Resolve<IUnitFactory>().CreateUnit(UnitTypeId.Knight, TeamColor.Blue, Vector3.zero);
      knight.ReplaceWorldPosition(new Vector3(0, 0));

      GameEntity goblin = Container.Resolve<IUnitFactory>().CreateUnit(UnitTypeId.TorchGoblin, TeamColor.Red, Vector3.zero);
      goblin.ReplaceWorldPosition(new Vector3(1, 0));

      ITimeService timeService = Container.Resolve<ITimeService>();

      UnitBehaviourFeature unitBehaviourFeature = Container.Resolve<ISystemFactory>().Create<UnitBehaviourFeature>();
      unitBehaviourFeature.Initialize();

      // Act
      float timer = 0;

      while (timer <= 3)
      {
        unitBehaviourFeature.Execute();
        unitBehaviourFeature.Cleanup();
        yield return null;
        timer += timeService.DeltaTime;
      }

      // Assert
      goblin.CurrentHp.Should().BeLessThan(goblin.MaxHp);
    }

    [UnityTest]
    public IEnumerator WhenKnightHasMoveWithAttackCommandInDirectionNextToTarget_ThenKnightShouldMoveToTargetAndKillHimAndMoveToEndDestination()
    {
      // Arrange
      EditorSceneManager.LoadSceneInPlayMode(EmptyTestScenePath, new(LoadSceneMode.Single));
      yield return null;

      BindNavMesh();

      LevelConfig config = Container.Resolve<IStaticDataService>().GetLevelConfig(LevelId.Empty);
      Container.Resolve<ILevelFactory>().CreateLevel(config);

      ITimeService timeService = Container.Resolve<ITimeService>();

      Vector2 destinationPosition = new(4, 0);

      GameEntity knight = Container.Resolve<IUnitFactory>().CreateUnit(UnitTypeId.Knight, TeamColor.Blue, Vector3.zero);
      knight.ReplaceUserCommand(new() { CommandTypeId = CommandTypeId.MoveWithAttack, WorldPosition = destinationPosition });

      UnitConfig knightConfig = Container.Resolve<IStaticDataService>().GetUnitConfig(UnitTypeId.Knight, TeamColor.Blue);

      GameEntity goblin = Container.Resolve<IUnitFactory>().CreateUnit(UnitTypeId.TorchGoblin, TeamColor.Red, Vector3.zero);
      goblin.ReplaceWorldPosition(new Vector3(3, knightConfig.AttackReach / 2));

      UnitBehaviourFeature unitBehaviourFeature = Container.Resolve<ISystemFactory>().Create<UnitBehaviourFeature>();
      unitBehaviourFeature.Initialize();

      // Act
      float timer = 0;

      while (knight.hasUserCommand && timer <= 30)
      {
        unitBehaviourFeature.Execute();
        unitBehaviourFeature.Cleanup();
        yield return null;
        timer += timeService.DeltaTime;
      }

      // Assert
      goblin.isAlive.Should().Be(false);
      Vector2.Distance(knight.WorldPosition, destinationPosition).Should().BeLessOrEqualTo(MaxPositionDelta);
    }

    [UnityTest]
    public IEnumerator WhenKnightHasMoveCommandInDirectionNextToTarget_ThenKnightShouldMoveToEndDestination()
    {
      // Arrange
      EditorSceneManager.LoadSceneInPlayMode(EmptyTestScenePath, new(LoadSceneMode.Single));
      yield return null;

      BindNavMesh();

      LevelConfig config = Container.Resolve<IStaticDataService>().GetLevelConfig(LevelId.Empty);
      Container.Resolve<ILevelFactory>().CreateLevel(config);

      ITimeService timeService = Container.Resolve<ITimeService>();

      Vector2 destinationPosition = new(-3, 2);

      GameEntity knight = Container.Resolve<IUnitFactory>().CreateUnit(UnitTypeId.Knight, TeamColor.Blue, Vector3.zero);
      knight.ReplaceUserCommand(new() { CommandTypeId = CommandTypeId.Move, WorldPosition = destinationPosition });

      UnitConfig knightConfig = Container.Resolve<IStaticDataService>().GetUnitConfig(UnitTypeId.Knight, TeamColor.Blue);

      GameEntity goblin = Container.Resolve<IUnitFactory>().CreateUnit(UnitTypeId.TorchGoblin, TeamColor.Red, Vector3.zero);
      goblin.ReplaceWorldPosition(new Vector3(-3, 2 - knightConfig.AttackReach / 2));

      UnitBehaviourFeature unitBehaviourFeature = Container.Resolve<ISystemFactory>().Create<UnitBehaviourFeature>();
      unitBehaviourFeature.Initialize();
      unitBehaviourFeature.Execute();
      unitBehaviourFeature.Cleanup();

      // Act
      float timer = 0;

      while (knight.hasDestination && timer <= 10)
      {
        unitBehaviourFeature.Execute();
        unitBehaviourFeature.Cleanup();
        yield return null;
        timer += timeService.DeltaTime;
      }

      // Assert
      goblin.isAlive.Should().Be(true);
      Vector2.Distance(knight.WorldPosition, destinationPosition).Should().BeLessOrEqualTo(MaxPositionDelta);
    }

    [UnityTest]
    public IEnumerator WhenKnightHasAimedAttackCommand_ThenKnightShouldMoveToTargetAndKillHim()
    {
      // Arrange
      EditorSceneManager.LoadSceneInPlayMode(EmptyTestScenePath, new(LoadSceneMode.Single));
      yield return null;

      BindNavMesh();

      LevelConfig config = Container.Resolve<IStaticDataService>().GetLevelConfig(LevelId.Empty);
      Container.Resolve<ILevelFactory>().CreateLevel(config);

      ITimeService timeService = Container.Resolve<ITimeService>();

      GameEntity goblin = Container.Resolve<IUnitFactory>().CreateUnit(UnitTypeId.TorchGoblin, TeamColor.Red, Vector3.zero);
      goblin.ReplaceWorldPosition(new Vector3(-4, -2));

      GameEntity knight = Container.Resolve<IUnitFactory>().CreateUnit(UnitTypeId.Knight, TeamColor.Blue, Vector3.zero);
      knight.ReplaceUserCommand(new() { CommandTypeId = CommandTypeId.AimedAttack, TargetId = goblin.Id });

      UnitBehaviourFeature unitBehaviourFeature = Container.Resolve<ISystemFactory>().Create<UnitBehaviourFeature>();
      unitBehaviourFeature.Initialize();

      // Act
      float timer = 0;

      while (knight.hasUserCommand && timer <= 30)
      {
        unitBehaviourFeature.Execute();
        unitBehaviourFeature.Cleanup();
        yield return null;
        timer += timeService.DeltaTime;
      }

      // Assert
      goblin.isAlive.Should().Be(false);
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
