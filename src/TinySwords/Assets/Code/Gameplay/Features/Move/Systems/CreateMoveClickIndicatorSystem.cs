using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Features.Move.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Move.Systems
{
  public class CreateMoveClickIndicatorSystem : IExecuteSystem
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly IMoveClickIndicatorFactory _moveClickIndicatorFactory;

    private readonly IGroup<GameEntity> _changeEndDestinationRequests;

    public CreateMoveClickIndicatorSystem(GameContext game, ICameraProvider cameraProvider, IMoveClickIndicatorFactory moveClickIndicatorFactory)
    {
      _cameraProvider = cameraProvider;
      _moveClickIndicatorFactory = moveClickIndicatorFactory;
      
      _changeEndDestinationRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ChangeEndDestinationRequest, GameMatcher.PositionOnScreen, GameMatcher.Processed));
    }

    public void Execute()
    {
      foreach (GameEntity request in _changeEndDestinationRequests)
      {
        GameEntity moveIndicator = _moveClickIndicatorFactory.CreateMoveIndicator(_cameraProvider.MainCamera.ScreenToWorldPoint(request.PositionOnScreen));
        
        moveIndicator.isCreatedNow = true;

        CreateEntity.Empty()
          .With(x => x.isDestructOldMoveIndicatorRequest = true);
      }
    }
  }
}
