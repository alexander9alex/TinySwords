using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.FastInteractions.Systems
{
  public class CreateFastInteractionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _inputs;

    public CreateFastInteractionSystem(GameContext game)
    {
      _inputs = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Input,
          GameMatcher.FastInteractionInput,
          GameMatcher.MousePosition
        ));
    }

    public void Execute()
    {
      foreach (GameEntity input in _inputs)
      {
        CreateEntity.Empty()
          .AddScreenPosition(input.MousePosition)
          .With(x => x.isFastInteraction = true);
      }
    }
  }
}
