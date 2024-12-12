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

      GameEntity unit = gameContext.CreateEntity()
        .AddUserCommand(new UserCommand() { CommandTypeId = CommandTypeId.MoveWithAttack, WorldPosition = Vector2.one });

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.Move);
    }

    [Test]
    public void WhenUnitHasMoveWithAttackUserCommand_ThenUnitAIShouldMakeMoveDecision()
    {
      // Arrange
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity unit = gameContext.CreateEntity()
        .AddUserCommand(new UserCommand() { CommandTypeId = CommandTypeId.MoveWithAttack, WorldPosition = Vector2.one });

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.Move);
    }

    [Test]
    public void WhenUnitHasTarget_ThenUnitAIShouldMakeMoveToTargetDecision()
    {
      // Arrange
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity enemy = gameContext.CreateEntity()
        .AddId(0)
        .AddWorldPosition(Vector2.one)
        .With(x => x.isAlive = true);

      GameEntity unit = gameContext.CreateEntity()
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { enemy.Id })
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new())
        .AddAllyBuffer(new());

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.MoveToTarget);
    }

    [Test]
    public void WhenUnitHasAimedAttackUserCommandAndUnreachableTarget_ThenUnitAIShouldMakeMoveToAimedTargetDecision()
    {
      // Arrange
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity enemy = gameContext.CreateEntity()
        .AddId(0)
        .AddWorldPosition(Vector2.one * 3)
        .With(x => x.isAlive = true);

      GameEntity unit = gameContext.CreateEntity()
        .AddUserCommand(new() { CommandTypeId = CommandTypeId.AimedAttack, TargetId = enemy.Id })
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { enemy.Id })
        .AddCollectTargetsRadius(1f)
        .AddReachedTargetBuffer(new());

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.MoveToAimedTarget);
    }

    [Test]
    public void WhenUnitHasAimedAttackUserCommandAndReachableTarget_ThenUnitAIShouldMakeAttackAimedTargetDecision()
    {
      // Arrange
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity enemy = gameContext.CreateEntity()
        .AddId(0)
        .AddWorldPosition(Vector2.one)
        .With(x => x.isAlive = true);

      GameEntity unit = gameContext.CreateEntity()
        .AddUserCommand(new() { CommandTypeId = CommandTypeId.AimedAttack, TargetId = enemy.Id })
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { enemy.Id })
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new() { enemy.Id });

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.AttackAimedTarget);
    }

    [Test]
    public void WhenUnitHas2Targets_ThenUnitAIShouldMakeMoveToNearestTargetDecision()
    {
      // Arrange
      Contexts contexts = new();
      GameContext gameContext = contexts.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity nearestEnemy = gameContext.CreateEntity()
        .AddId(0)
        .AddWorldPosition(Vector2.right)
        .With(x => x.isAlive = true);

      GameEntity furtherEnemy = gameContext.CreateEntity()
        .AddId(1)
        .AddWorldPosition(Vector2.right * 2)
        .With(x => x.isAlive = true);

      GameEntity unit = gameContext.CreateEntity()
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { nearestEnemy.Id, furtherEnemy.Id })
        .AddCollectTargetsRadius(3f)
        .AddReachedTargetBuffer(new())
        .AddAllyBuffer(new());

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.TargetId.Value.Should().Be(nearestEnemy.Id);
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
