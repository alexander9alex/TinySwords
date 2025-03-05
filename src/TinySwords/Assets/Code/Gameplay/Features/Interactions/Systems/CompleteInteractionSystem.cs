using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Interactions.Systems
{
  public class CompleteInteractionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _inputs;
    private readonly IGroup<GameEntity> _interactions;

    public CompleteInteractionSystem(GameContext game)
    {
      _inputs = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Input,
          GameMatcher.InteractionEndInput
        ));

      _interactions = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Interaction));
    }

    public void Execute()
    {
      foreach (GameEntity _ in _inputs)
      foreach (GameEntity interaction in _interactions)
      {
        interaction
          .With(x => x.isCompleteInteraction = true);
      }
    }
  }
}
