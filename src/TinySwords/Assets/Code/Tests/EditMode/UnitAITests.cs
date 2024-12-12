using Code.Common.Extensions;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.UtilityAI;
using Code.Gameplay.UtilityAI.Brains;
using Code.Gameplay.UtilityAI.Components;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace Code.Tests.EditMode
{
  public class UnitAITests
  {
    [Test]
    public void WhenUnitHasNotUserCommandAndTargetsAndAllies_ThenUnitAIShouldMakeStayDecision()
    {
      // Arrange
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity unit = gameContext.CreateEntity()
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
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

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
    public void WhenUnitHasMoveWithAttackUserCommand_ThenUnitAIShouldMakeMoveDecision()
    {
      // Arrange
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

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
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity target = gameContext.CreateEntity()
        .AddId(0)
        .AddWorldPosition(Vector2.one)
        .With(x => x.isAlive = true);

      Vector2 endDestination = Vector2.one;
      GameEntity unit = gameContext.CreateEntity()
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
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity target = gameContext.CreateEntity()
        .AddId(0)
        .AddWorldPosition(Vector2.one)
        .With(x => x.isAlive = true);

      Vector2 endDestination = Vector2.one;
      GameEntity unit = gameContext.CreateEntity()
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
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity target = gameContext.CreateEntity()
        .AddId(0)
        .AddWorldPosition(Vector2.one)
        .With(x => x.isAlive = true);

      GameEntity unit = gameContext.CreateEntity()
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
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity target = gameContext.CreateEntity()
        .AddId(0)
        .AddWorldPosition(Vector2.one * 3)
        .With(x => x.isAlive = true);

      GameEntity unit = gameContext.CreateEntity()
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
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity aimedTarget = gameContext.CreateEntity()
        .AddId(0)
        .AddWorldPosition(Vector2.one * 3)
        .With(x => x.isAlive = true);

      GameEntity reachableTarget = gameContext.CreateEntity()
        .AddId(1)
        .AddWorldPosition(Vector2.left)
        .With(x => x.isAlive = true);

      GameEntity unit = gameContext.CreateEntity()
        .AddUserCommand(new() { CommandTypeId = CommandTypeId.AimedAttack, TargetId = aimedTarget.Id })
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { reachableTarget.Id, aimedTarget.Id })
        .AddCollectTargetsRadius(2f)
        .AddReachedTargetBuffer(new() { reachableTarget.Id });

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
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity target = gameContext.CreateEntity()
        .AddId(0)
        .AddWorldPosition(Vector2.one)
        .With(x => x.isAlive = true);

      GameEntity unit = gameContext.CreateEntity()
        .AddUserCommand(new() { CommandTypeId = CommandTypeId.AimedAttack, TargetId = target.Id })
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { target.Id })
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new() { target.Id });

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
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity aimedTarget = gameContext.CreateEntity()
        .AddId(0)
        .AddWorldPosition(Vector2.one)
        .With(x => x.isAlive = true);

      GameEntity reachableTarget = gameContext.CreateEntity()
        .AddId(1)
        .AddWorldPosition(Vector2.one * 0.5f)
        .With(x => x.isAlive = true);

      GameEntity unit = gameContext.CreateEntity()
        .AddUserCommand(new() { CommandTypeId = CommandTypeId.AimedAttack, TargetId = aimedTarget.Id })
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { aimedTarget.Id, reachableTarget.Id })
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new() { aimedTarget.Id, reachableTarget.Id });

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
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity nearestTarget = gameContext.CreateEntity()
        .AddId(0)
        .AddWorldPosition(Vector2.right)
        .With(x => x.isAlive = true);

      GameEntity furtherTarget = gameContext.CreateEntity()
        .AddId(1)
        .AddWorldPosition(Vector2.right * 2)
        .With(x => x.isAlive = true);

      GameEntity unit = gameContext.CreateEntity()
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

    private static IUnitAI UnitAI(GameContext gameContext)
    {
      When when = new();
      GetInput getInput = new(gameContext);
      Score score = new();

      BrainsComponents brainsComponents = new(when, getInput, score);

      UnitBrains unitBrains = new(brainsComponents);
      IUnitAI unitAI = new UnitAI(unitBrains, gameContext);
      return unitAI;
    }
  }
}
