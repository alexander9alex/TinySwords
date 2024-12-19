using System.Collections;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Destruct;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.Features.Units.Factory;
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
      ITimeService timeService = Container.Resolve<ITimeService>();

      Container.Resolve<ILevelFactory>().CreateLevel();
      
      Vector2 destinationPosition = new(2, 1);

      GameEntity knight = Container.Resolve<IUnitFactory>().CreateUnit(UnitTypeId.Knight, TeamColor.Blue, Vector3.zero);

      EditorSceneManager.LoadSceneInPlayMode(EmptyTestScenePath, new(LoadSceneMode.Single));
      yield return null;

      UnitBehaviourFeature unitBehaviourFeature = Container.Resolve<ISystemFactory>().Create<UnitBehaviourFeature>();
      unitBehaviourFeature.Initialize();
      unitBehaviourFeature.Execute();
      unitBehaviourFeature.Cleanup();
      yield return null;
      
      knight.ReplaceUserCommand(new() { CommandTypeId = CommandTypeId.Move, WorldPosition = destinationPosition });

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
      knight.WorldPosition.Should().Be(destinationPosition);
    }
  }
}
