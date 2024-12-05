using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Features.MoveIndicator.Factory;
using Entitas;

namespace Code.Gameplay.Features.IncorrectCommandIndicator.Systems
{
  public class CreateIncorrectCommandIndicatorSystem : IExecuteSystem
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly IIndicatorFactory _indicatorFactory;

    private readonly IGroup<GameEntity> _createIncorrectCommandIndicatorRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CreateIncorrectCommandIndicatorSystem(GameContext game, ICameraProvider cameraProvider, IIndicatorFactory indicatorFactory)
    {
      _cameraProvider = cameraProvider;
      _indicatorFactory = indicatorFactory;

      _createIncorrectCommandIndicatorRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CreateIncorrectCommandIndicator, GameMatcher.PositionOnScreen));
    }

    public void Execute()
    {
      foreach (GameEntity request in _createIncorrectCommandIndicatorRequests.GetEntities(_buffer))
      {
        GameEntity indicator = _indicatorFactory.CreateIncorrectCommandIndicator(_cameraProvider.MainCamera.ScreenToWorldPoint(request.PositionOnScreen));
        indicator.isCreatedNow = true;
  
        CreateEntity.Empty()
          .With(x => x.isDestructOldIncorrectCommandIndicatorRequest = true);

        request.isDestructed = true;
      }
    }
  }
}
