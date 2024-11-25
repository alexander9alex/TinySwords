using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Features.MoveIndicator.Factory;
using Entitas;

namespace Code.Gameplay.Features.MoveIndicator.Systems
{
  public class CreateMoveClickIndicatorSystem : IExecuteSystem
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly IMoveClickIndicatorFactory _moveClickIndicatorFactory;

    private readonly IGroup<GameEntity> _createMoveClickIndicatorRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CreateMoveClickIndicatorSystem(GameContext game, ICameraProvider cameraProvider, IMoveClickIndicatorFactory moveClickIndicatorFactory)
    {
      _cameraProvider = cameraProvider;
      _moveClickIndicatorFactory = moveClickIndicatorFactory;

      _createMoveClickIndicatorRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CreateMoveClickIndicator, GameMatcher.PositionOnScreen));
    }

    public void Execute()
    {
      foreach (GameEntity request in _createMoveClickIndicatorRequests.GetEntities(_buffer))
      {
        GameEntity moveIndicator = _moveClickIndicatorFactory.CreateMoveIndicator(_cameraProvider.MainCamera.ScreenToWorldPoint(request.PositionOnScreen));

        moveIndicator.isCreatedNow = true;

        CreateEntity.Empty()
          .With(x => x.isDestructOldMoveIndicatorRequest = true);

        request.isDestructed = true;
      }
    }
  }
}
