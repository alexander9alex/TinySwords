using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Destruct;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.UtilityAI;
using Code.Infrastructure.Factory;
using Code.Tests.TestTools;
using Code.Tests.Tools;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Code.Tests.EditMode
{
  public class UnitAITests : ZenjectUnitTestFixture
  {
    [SetUp]
    public void InstallBindings()
    {
      Bind.GameContext(Container);
      Bind.UnitAI(Container);
    }

    [TearDown]
    public void TearDown()
    {
      Bind.SystemFactory(Container);
      Bind.TimeService(Container);

      Destruct.AllEntities(Container.Resolve<ISystemFactory>().Create<ProcessDestructedFeature>(), Container.Resolve<GameContext>());
    }

    [Test]
    public void WhenUnitHasNotUserCommandAndTargetsAndAllies_ThenUnitAIShouldMakeStayDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      GameEntity unit = CreateEntity.Empty()
        .AddTargetBuffer(new())
        .AddReachedTargetBuffer(new())
        .AddAllyBuffer(new());

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.Stay);
    }

    [Test]
    public void WhenUnitHasMoveUserCommand_ThenUnitAIShouldMakeMoveDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      Vector2 endDestination = Vector2.one;
      GameEntity unit = CreateEntity.Empty()
        .AddUserCommand(new UserCommand { CommandTypeId = CommandTypeId.MoveWithAttack, WorldPosition = endDestination });

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.Move);
      decision.Destination.Should().Be(endDestination);
    }

    [Test]
    public void WhenUnitHasMoveWithAttackUserCommand_ThenUnitAIShouldMakeMoveDecision()
    {
      // Arrange
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      Vector2 endDestination = Vector2.one;
      GameEntity unit = gameContext.CreateEntity()
        .AddUserCommand(new UserCommand { CommandTypeId = CommandTypeId.MoveWithAttack, WorldPosition = endDestination });

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.Move);
      decision.Destination.Should().Be(endDestination);
    }

    [Test]
    public void WhenUnitHasMoveUserCommandAndTarget_ThenUnitAIShouldMakeMoveDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      GameEntity target = CreateEntity.Empty()
        .AddId(0)
        .AddWorldPosition(Vector2.one)
        .With(x => x.isAlive = true);

      Vector2 endDestination = Vector2.one;
      GameEntity unit = CreateEntity.Empty()
        .AddUserCommand(new UserCommand { CommandTypeId = CommandTypeId.Move, WorldPosition = endDestination })
        .AddWorldPosition(Vector3.zero)
        .AddTargetBuffer(new() { target.Id })
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new());

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.Move);
      decision.Destination.Should().Be(endDestination);
    }

    [Test]
    public void WhenUnitHasMoveWithAttackUserCommandAndTarget_ThenUnitAIShouldMakeMoveToTargetDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      GameEntity target = CreateEntity.Empty()
        .AddId(0)
        .AddWorldPosition(Vector2.one)
        .With(x => x.isAlive = true);

      Vector2 endDestination = Vector2.one;
      GameEntity unit = CreateEntity.Empty()
        .AddUserCommand(new UserCommand { CommandTypeId = CommandTypeId.MoveWithAttack, WorldPosition = endDestination })
        .AddWorldPosition(Vector3.zero)
        .AddTargetBuffer(new() { target.Id })
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new());

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.MoveToTarget);
      decision.TargetId.Should().Be(target.Id);
    }

    [Test]
    public void WhenUnitHasTarget_ThenUnitAIShouldMakeMoveToTargetDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      GameEntity target = CreateEntity.Empty()
        .AddId(0)
        .AddWorldPosition(Vector2.one)
        .With(x => x.isAlive = true);

      GameEntity unit = CreateEntity.Empty()
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { target.Id })
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new())
        .AddAllyBuffer(new());

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.MoveToTarget);
      decision.TargetId.Should().Be(target.Id);
    }

    [Test]
    public void WhenUnitHasAimedAttackUserCommandAndUnreachableTarget_ThenUnitAIShouldMakeMoveToAimedTargetDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      GameEntity target = CreateEntity.Empty()
        .AddId(0)
        .AddWorldPosition(Vector2.one * 3)
        .With(x => x.isAlive = true);

      GameEntity unit = CreateEntity.Empty()
        .AddUserCommand(new() { CommandTypeId = CommandTypeId.AimedAttack, TargetId = target.Id })
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { target.Id })
        .AddCollectTargetsRadius(1f)
        .AddReachedTargetBuffer(new());

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.MoveToAimedTarget);
      decision.TargetId.Should().Be(target.Id);
    }

    [Test]
    public void WhenUnitHasAimedAttackUserCommandAndUnreachableAimedTargetAndReachableTarget_ThenUnitAIShouldMakeMoveToAimedTargetDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      GameEntity aimedTarget = CreateEntity.Empty()
        .AddId(0)
        .AddWorldPosition(Vector2.one * 3)
        .With(x => x.isAlive = true);

      GameEntity reachableTarget = CreateEntity.Empty()
        .AddId(1)
        .AddWorldPosition(Vector2.left)
        .With(x => x.isAlive = true);

      GameEntity unit = CreateEntity.Empty()
        .AddUserCommand(new() { CommandTypeId = CommandTypeId.AimedAttack, TargetId = aimedTarget.Id })
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { reachableTarget.Id, aimedTarget.Id })
        .AddCollectTargetsRadius(4f)
        .AddReachedTargetBuffer(new() { reachableTarget.Id })
        .AddCollectReachedTargetsRadius(2f);

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.MoveToAimedTarget);
      decision.TargetId.Should().Be(aimedTarget.Id);
    }

    [Test]
    public void WhenUnitHasAimedAttackUserCommandAndReachableTarget_ThenUnitAIShouldMakeAttackAimedTargetDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      GameEntity target = CreateEntity.Empty()
        .AddId(0)
        .AddWorldPosition(Vector2.one)
        .With(x => x.isAlive = true);

      GameEntity unit = CreateEntity.Empty()
        .AddUserCommand(new() { CommandTypeId = CommandTypeId.AimedAttack, TargetId = target.Id })
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { target.Id })
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new() { target.Id })
        .AddCollectReachedTargetsRadius(2f);

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.AttackAimedTarget);
      decision.TargetId.Should().Be(target.Id);
    }

    [Test]
    public void WhenUnitHasAimedAttackUserCommandAndReachableAimedTargetAndReachableTarget_ThenUnitAIShouldMakeAttackAimedTargetDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      GameEntity aimedTarget = CreateEntity.Empty()
        .AddId(0)
        .AddWorldPosition(Vector2.one)
        .With(x => x.isAlive = true);

      GameEntity reachableTarget = CreateEntity.Empty()
        .AddId(1)
        .AddWorldPosition(Vector2.right * 0.5f)
        .With(x => x.isAlive = true);

      GameEntity unit = CreateEntity.Empty()
        .AddUserCommand(new() { CommandTypeId = CommandTypeId.AimedAttack, TargetId = aimedTarget.Id })
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { aimedTarget.Id, reachableTarget.Id })
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new() { aimedTarget.Id, reachableTarget.Id })
        .AddCollectReachedTargetsRadius(2f);

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.AttackAimedTarget);
      decision.TargetId.Should().Be(aimedTarget.Id);
    }

    [Test]
    public void WhenUnitHas2Targets_ThenUnitAIShouldMakeMoveToNearestTargetDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      GameEntity nearestTarget = CreateEntity.Empty()
        .AddId(0)
        .AddWorldPosition(Vector2.right)
        .With(x => x.isAlive = true);

      GameEntity furtherTarget = CreateEntity.Empty()
        .AddId(1)
        .AddWorldPosition(Vector2.right * 2)
        .With(x => x.isAlive = true);

      GameEntity unit = CreateEntity.Empty()
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { nearestTarget.Id, furtherTarget.Id })
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new())
        .AddAllyBuffer(new());

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.MoveToTarget);
      decision.TargetId.Value.Should().Be(nearestTarget.Id);
    }

    [Test]
    public void WhenUnitHasUnreachableTargetAndReachableTarget_ThenUnitAIShouldMakeAttackDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      GameEntity reachableTarget = CreateEntity.Empty()
        .AddId(0)
        .AddWorldPosition(Vector2.up * 2)
        .With(x => x.isAlive = true);

      GameEntity unreachableTarget = CreateEntity.Empty()
        .AddId(1)
        .AddWorldPosition(Vector2.down * 4)
        .With(x => x.isAlive = true);

      GameEntity unit = CreateEntity.Empty()
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { unreachableTarget.Id, reachableTarget.Id })
        .AddCollectTargetsRadius(5f)
        .AddReachedTargetBuffer(new() { reachableTarget.Id })
        .AddCollectReachedTargetsRadius(3f);

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.Attack);
      decision.TargetId.Value.Should().Be(reachableTarget.Id);
    }

    [Test]
    public void WhenUnitHasAllyWithTarget_ThenUnitAIShouldMakeMoveToAllyTargetDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      GameEntity target = CreateEntity.Empty()
        .AddId(0)
        .AddWorldPosition(Vector2.right * 4)
        .With(x => x.isAlive = true);

      GameEntity ally = CreateEntity.Empty()
        .AddId(1)
        .AddWorldPosition(Vector2.right * 2)
        .AddTargetBuffer(new() { target.Id })
        .With(x => x.isAlive = true);

      GameEntity unit = CreateEntity.Empty()
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new())
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new())
        .AddAllyBuffer(new() { ally.Id })
        .AddCollectAlliesRadius(3f);

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.MoveToAllyTarget);
      decision.TargetId.Value.Should().Be(target.Id);
    }

    [Test]
    public void WhenUnitHasAllyWithAllyTargetId_ThenUnitAIShouldMakeMoveToAllyTargetDecision()
    {
      // Arrange
      IUnitAI unitAI = Container.Resolve<IUnitAI>();

      GameEntity target = CreateEntity.Empty()
        .AddId(0)
        .AddWorldPosition(Vector2.right * 4)
        .With(x => x.isAlive = true);

      GameEntity ally = CreateEntity.Empty()
        .AddId(1)
        .AddWorldPosition(Vector2.right * 2)
        .AddTargetBuffer(new())
        .AddAllyTargetId(target.Id)
        .With(x => x.isAlive = true);

      GameEntity unit = CreateEntity.Empty()
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new())
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new())
        .AddAllyBuffer(new() { ally.Id })
        .AddCollectAlliesRadius(3f);

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.MoveToAllyTarget);
      decision.TargetId.Value.Should().Be(target.Id);
    }
  }
}
