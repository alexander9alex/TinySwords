using Entitas;

namespace Code.Gameplay.Features.Interactions.Systems
{
  public class SetInteractionEndPositionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _inputs;
    private readonly IGroup<GameEntity> _interactions;

    public SetInteractionEndPositionSystem(GameContext game)
    {
      _inputs = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Input,
          GameMatcher.InteractionEndInput,
          GameMatcher.MousePosition
        ));

      _interactions = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Interaction));
    }

    public void Execute()
    {
      foreach (GameEntity input in _inputs)
      foreach (GameEntity interaction in _interactions)
      {
        interaction
          .AddEndPosition(input.MousePosition);
      }
    }
  }
}
