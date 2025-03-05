using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Interactions.Select.Systems
{
  public class SelectHighlightedUnitsSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;

    private readonly IGroup<GameEntity> _highlights;
    private readonly IGroup<GameEntity> _interactions;

    public SelectHighlightedUnitsSystem(GameContext game, IPhysicsService physicsService)
    {
      _physicsService = physicsService;

      _interactions = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Interaction,
          GameMatcher.StartPosition,
          GameMatcher.EndPosition,
          GameMatcher.CompleteInteraction
        ));

      _highlights = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Highlight,
          GameMatcher.StartPosition,
          GameMatcher.EndPosition
        ));
    }

    public void Execute()
    {
      foreach (GameEntity _ in _interactions)
      foreach (GameEntity highlight in _highlights)
      foreach (GameEntity entity in GetHighlightedSelectables(highlight.StartPosition, highlight.EndPosition))
      {
        if (entity.isSelectable)
          Select(entity);

        CreateEntity.Empty()
          .With(x => x.isUnselectPreviouslySelected = true);

        CreateEntity.Empty()
          .With(x => x.isUpdateHudControlButtons = true);
      }
    }

    private static void Select(GameEntity entity)
    {
      entity.isUnselected = false;
      entity.isSelected = true;
      entity.isSelectedNow = true;
    }

    private IEnumerable<GameEntity> GetHighlightedSelectables(Vector2 startPos, Vector2 endPos)
    {
      return _physicsService.BoxCast(
        HighlightPos(startPos, endPos),
        HighlightSize(startPos, endPos),
        GameConstants.SelectionLayerMask);
    }

    private Vector3 HighlightPos(Vector2 startPos, Vector2 endPos) =>
      (startPos + endPos) / 2;

    private Vector3 HighlightSize(Vector2 startPos, Vector2 endPos) =>
      (endPos - startPos).Abs();
  }
}
