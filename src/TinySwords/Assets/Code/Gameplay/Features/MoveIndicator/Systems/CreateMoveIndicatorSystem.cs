using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Features.MoveIndicator.Factory;
using Entitas;

namespace Code.Gameplay.Features.MoveIndicator.Systems
{
  public class CreateMoveIndicatorSystem : IExecuteSystem
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly IIndicatorFactory _indicatorFactory;

    private readonly IGroup<GameEntity> _createMoveIndicatorRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CreateMoveIndicatorSystem(GameContext game, ICameraProvider cameraProvider, IIndicatorFactory indicatorFactory)
    {
      _cameraProvider = cameraProvider;
      _indicatorFactory = indicatorFactory;

      _createMoveIndicatorRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CreateMoveIndicator, GameMatcher.PositionOnScreen));
    }

    public void Execute()
    {
      foreach (GameEntity request in _createMoveIndicatorRequests.GetEntities(_buffer))
      {
        GameEntity moveIndicator = _indicatorFactory.CreateMoveIndicator(_cameraProvider.MainCamera.ScreenToWorldPoint(request.PositionOnScreen));
        moveIndicator.isCreatedNow = true;

        CreateEntity.Empty()
          .With(x => x.isDestructOldMoveIndicatorRequest = true);

        request.isDestructed = true;
      }
    }
  }
}
