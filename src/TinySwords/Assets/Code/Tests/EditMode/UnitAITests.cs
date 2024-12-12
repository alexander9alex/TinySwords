using Code.Common.Entities;
using Code.Common.Extensions;
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
      GameContext gameContext = Contexts.sharedInstance.game;
      IUnitAI unitAI = UnitAI(gameContext);

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
    public void WhenUnitHasTarget_ThenUnitAIShouldMakeMoveToTargetDecision()
    {
      // Arrange
      GameContext gameContext = Contexts.sharedInstance.game;
      IUnitAI unitAI = UnitAI(gameContext);

      GameEntity enemy = CreateEntity.Empty()
          .AddId(0)
          .AddWorldPosition(Vector2.one)
          .With(x => x.isAlive = true);
      
      GameEntity unit = CreateEntity.Empty()
        .AddWorldPosition(Vector2.zero)
        .AddTargetBuffer(new() { enemy.Id })
        .AddReachedTargetBuffer(new())
        .AddAllyBuffer(new());

      // Act
      UnitDecision decision = unitAI.MakeBestDecision(unit);

      // Assert
      decision.UnitDecisionTypeId.Should().Be(UnitDecisionTypeId.MoveToTarget);
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
