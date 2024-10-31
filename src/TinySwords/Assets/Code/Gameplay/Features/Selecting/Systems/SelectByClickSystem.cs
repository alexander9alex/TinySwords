using System.Linq;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Common.Providers;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Selecting.Systems
{
  public class SelectByClickSystem : IExecuteSystem
  {
    private const float ClickRadius = 0.25f;

    private readonly IPhysicsService _physicsService;
    private readonly ICameraProvider _cameraProvider;
    private readonly IGroup<GameEntity> _interactions;
    private readonly IGroup<GameEntity> _mousePositions;

    private readonly int _layerMask = 1 << LayerMask.NameToLayer("Unit");

    public SelectByClickSystem(GameContext game, IPhysicsService physicsService, ICameraProvider cameraProvider)
    {
      _physicsService = physicsService;
      _cameraProvider = cameraProvider;

      _interactions = game.GetGroup(GameMatcher.MakeInteraction);
      _mousePositions = game.GetGroup(GameMatcher.MousePosition);
    }

    public void Execute()
    {
      foreach (GameEntity _ in _interactions)
      foreach (GameEntity mousePosition in _mousePositions)
      {
        GameEntity entity = GetSelectableEntityFromPosition(mousePosition.MousePosition);

        if (entity != null && entity.isSelectable)
          entity.isSelected = true;
      }
    }

    private GameEntity GetSelectableEntityFromPosition(Vector2 mousePos)
    {
      return _physicsService.CircleCast(
          _cameraProvider.MainCamera.ScreenToWorldPoint(mousePos),
          ClickRadius,
          _layerMask)
        .FirstOrDefault();
    }
  }
}
