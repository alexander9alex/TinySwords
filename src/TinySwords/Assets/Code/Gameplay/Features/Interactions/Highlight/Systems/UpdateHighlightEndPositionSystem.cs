using Code.Gameplay.Common.Providers;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Interactions.Highlight.Systems
{
  public class UpdateHighlightEndPositionSystem : IExecuteSystem
  {
    private readonly ICameraProvider _cameraProvider;

    private readonly IGroup<GameEntity> _highlights;
    private readonly IGroup<GameEntity> _inputs;

    public UpdateHighlightEndPositionSystem(GameContext game, ICameraProvider cameraProvider)
    {
      _cameraProvider = cameraProvider;
      _highlights = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Highlight,
          GameMatcher.StartPosition,
          GameMatcher.EndPosition
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
      foreach (GameEntity input in _inputs)
      {
        Vector2 endPos = _cameraProvider.ScreenToWorldPoint(input.MousePosition);
        highlight.ReplaceEndPosition(endPos);
      }
    }
  }
}
