using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Constants;
using Entitas;

namespace Code.Gameplay.Features.Select.Systems
{
  public class SelectHighlightedCastleSystem : IExecuteSystem
  {
    private const float PixelsPerUnit = 100;

    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;

    private readonly IGroup<GameEntity> _highlights;
    private readonly IGroup<GameEntity> _createMultipleSelectionRequests;
    private readonly IGroup<GameEntity> _selectedNow;

    public SelectHighlightedCastleSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _createMultipleSelectionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MultipleSelectionRequest)
        .NoneOf(GameMatcher.Processed));
      
      _highlights = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Highlight, GameMatcher.CenterPosition, GameMatcher.Size));

      _selectedNow = game.GetGroup(GameMatcher.SelectedNow);
    }

    public void Execute()
    {
      foreach (GameEntity request in _createMultipleSelectionRequests)
      foreach (GameEntity highlight in _highlights)
      {
        if (_selectedNow.count > 0)
          return;

        List<GameEntity> highlightedCastles = GetHighlightedCastles(highlight);

        if (highlightedCastles.Count > 0)
        {
          SelectCastle(highlightedCastles.First());
          
          CreateEntity.Empty()
            .With(x => x.isUnselectPreviouslySelectedRequest = true);
          
          CreateEntity.Empty()
            .With(x => x.isSelectedChanged = true);
          
          request.isProcessed = true;
        }
      }
    }

    private List<GameEntity> GetHighlightedCastles(GameEntity highlight)
    {
      return _physicsService.BoxCast(
          _cameraProvider.MainCamera.ScreenToWorldPoint(highlight.CenterPosition),
          highlight.Size / PixelsPerUnit,
          GameConstants.SelectionLayerMask)
        .Where(entity => entity.isSelectable)
        .Where(entity => entity.isCastle)
        .ToList();
    }
    
    private static void SelectCastle(GameEntity entity)
    {
      entity.isUnselected = false;
      entity.isSelected = true;
      entity.isSelectedNow = true;
    }
  }
}
