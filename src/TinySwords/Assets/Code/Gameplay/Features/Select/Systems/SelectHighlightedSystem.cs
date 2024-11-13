using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Entitas;

namespace Code.Gameplay.Features.Select.Systems
{
  public class SelectHighlightedSystem : IExecuteSystem
  {
    private const float PixelsPerUnit = 100;

    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;

    private readonly IGroup<GameEntity> _highlights;
    private readonly IGroup<GameEntity> _createMultipleSelectionRequests;

    public SelectHighlightedSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _highlights = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Highlight, GameMatcher.CenterPosition, GameMatcher.Size));

      _createMultipleSelectionRequests = game.GetGroup(GameMatcher.MultipleSelectionRequest);
    }

    public void Execute()
    {
      foreach (GameEntity _ in _createMultipleSelectionRequests)
      foreach (GameEntity highlight in _highlights)
      foreach (GameEntity entity in GetHighlightedEntities(highlight))
      {
        if (entity.isSelectable)
        {
          entity.isUnselected = false;
          entity.isSelected = true;
          entity.isSelectedNow = true;
        }

        CreateEntity.Empty()
          .With(x => x.isUnselectPreviouslySelectedRequest = true);
      }
    }

    private IEnumerable<GameEntity> GetHighlightedEntities(GameEntity highlight)
    {
      return _physicsService.BoxCast(
        _cameraProvider.MainCamera.ScreenToWorldPoint(highlight.CenterPosition),
        highlight.Size / PixelsPerUnit,
        GameConstants.SelectionLayerMask);
    }
  }
}
