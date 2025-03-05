using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Interactions.Systems
{
  public class CreateInteractionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _inputs;

    public CreateInteractionSystem(GameContext game)
    {
      _inputs = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Input,
          GameMatcher.InteractionStartInput,
          GameMatcher.MousePosition
        ));
    }

    public void Execute()
    {
      foreach (GameEntity input in _inputs)
      {
        CreateEntity.Empty()
          .AddStartPosition(input.MousePosition)
          .With(x => x.isInteraction = true);
      }
    }
  }
}
