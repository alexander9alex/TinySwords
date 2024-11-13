using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Features.Move.Factory;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class CreateMoveClickIndicatorSystem : IExecuteSystem
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly IMoveClickIndicatorFactory _moveClickIndicatorFactory;

    private readonly IGroup<GameEntity> _moveRequests;

    public CreateMoveClickIndicatorSystem(GameContext game, ICameraProvider cameraProvider, IMoveClickIndicatorFactory moveClickIndicatorFactory)
    {
      _cameraProvider = cameraProvider;
      _moveClickIndicatorFactory = moveClickIndicatorFactory;
      
      _moveRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MoveRequest, GameMatcher.PositionOnScreen, GameMatcher.Processed));
    }

    public void Execute()
    {
      foreach (GameEntity request in _moveRequests)
      {
        GameEntity moveIndicator = _moveClickIndicatorFactory.CreateMoveIndicator(_cameraProvider.MainCamera.ScreenToWorldPoint(request.PositionOnScreen));
        
        moveIndicator.isCreatedNow = true;

        CreateEntity.Empty()
          .With(x => x.isDestructOldMoveIndicatorRequest = true);
      }
    }
  }
}
