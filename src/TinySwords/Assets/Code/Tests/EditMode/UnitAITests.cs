using Code.Common.Entities;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.UtilityAI;
using Code.Gameplay.UtilityAI.Brains;
using Code.Gameplay.UtilityAI.Components;
using FluentAssertions;
using NUnit.Framework;

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
