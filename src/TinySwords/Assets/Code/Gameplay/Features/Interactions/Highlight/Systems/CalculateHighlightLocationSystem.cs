using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Interactions.Highlight.Systems
{
  public class CalculateHighlightLocationSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _highlights;
    private readonly IGroup<GameEntity> _interactions;
    private readonly IGroup<GameEntity> _inputs;

    public CalculateHighlightLocationSystem(GameContext game)
    {
      _highlights = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Highlight,
          GameMatcher.CenterPosition,
          GameMatcher.Size
        ));

      _interactions = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Interaction,
          GameMatcher.StartPosition
        ));

      _inputs = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Input,
          GameMatcher.MousePosition
        ));
    }

    public void Execute()
    {
      foreach (GameEntity highlight in _highlights)
      foreach (GameEntity interaction in _interactions)
      foreach (GameEntity input in _inputs)
      {
        Vector2 pos = GetPos(interaction, input);
        highlight.ReplaceCenterPosition(pos);

        Vector2 size = GetSize(interaction.StartPosition, input.MousePosition);
        highlight.ReplaceSize(size);
      }
    }

    private static Vector2 GetSize(Vector2 start, Vector2 end)
    {
      Vector2 min = Vector2.Min(start, end);
      Vector2 max = Vector2.Max(start, end);

      return max - min;
    }

    private static Vector2 GetPos(GameEntity interaction, GameEntity input) =>
      (interaction.StartPosition + input.MousePosition) / 2;
  }
}
