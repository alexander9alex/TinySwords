using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Select.Systems
{
  public class SelectHighlightedUnitSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;

    private readonly IGroup<GameEntity> _highlights;
    private readonly IGroup<GameEntity> _createMultipleSelectionRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public SelectHighlightedUnitSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _createMultipleSelectionRequests = game.GetGroup(GameMatcher.MultipleSelectionRequest);

      _highlights = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Highlight, GameMatcher.StartPosition, GameMatcher.EndPosition));
    }

    public void Execute()
    {
      foreach (GameEntity request in _createMultipleSelectionRequests.GetEntities(_buffer))
      foreach (GameEntity highlight in _highlights)
      foreach (GameEntity entity in GetHighlightedSelectables(highlight))
      {
        if (entity.isSelectable)
        {
          entity.isUnselected = false;
          entity.isSelected = true;
          entity.isSelectedNow = true;
        }

        CreateEntity.Empty()
          .With(x => x.isUnselectPreviouslySelectedRequest = true);

        CreateEntity.Empty()
          .With(x => x.isSelectedChanged = true);

        request.isProcessed = true;
      }
    }

    private IEnumerable<GameEntity> GetHighlightedSelectables(GameEntity highlight)
    {
      return _physicsService.BoxCast(
          HighlightPos(highlight),
          HighlightSize(highlight),
          GameConstants.SelectionLayerMask)
        .Where(entity => entity.isSelectable);
    }

    private Vector3 HighlightPos(GameEntity highlight) =>
      _cameraProvider.ScreenToWorldPoint(highlight.CenterPosition);

    private Vector3 HighlightSize(GameEntity highlight)
    {
      Vector2 min = Vector2.Min(highlight.StartPosition, highlight.EndPosition);
      Vector2 max = Vector2.Max(highlight.StartPosition, highlight.EndPosition);

      return _cameraProvider.ScreenToWorldPoint(max) - _cameraProvider.ScreenToWorldPoint(min);
    }
  }
}
