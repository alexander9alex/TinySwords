using System.Collections.Generic;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Features.Select.Data;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Select.Systems
{
  public class SelectHighlightedSystem : IExecuteSystem
  {
    private const float PixelsPerUnit = 100;

    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;

    private readonly IGroup<GameEntity> _highlights;

    public SelectHighlightedSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _highlights = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Highlight, GameMatcher.CenterPosition, GameMatcher.Size));
    }

    public void Execute()
    {
      foreach (GameEntity highlight in _highlights)
      foreach (GameEntity entity in GetHighlightedEntities(highlight))
      {
        if (entity.isSelectable)
        {
          entity.isUnselected = false;
          entity.isSelected = true;
          entity.isSelectedNow = true;
        }
      }
    }

    private IEnumerable<GameEntity> GetHighlightedEntities(GameEntity highlight)
    {
      return _physicsService.BoxCast(
        _cameraProvider.MainCamera.ScreenToWorldPoint(highlight.CenterPosition),
        highlight.Size / PixelsPerUnit,
        SelectionData.SelectionLayerMask);
    }
  }
}
