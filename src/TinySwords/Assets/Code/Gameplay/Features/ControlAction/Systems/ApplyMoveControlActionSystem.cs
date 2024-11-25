using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.ControlAction.Systems
{
  public class ApplyMoveControlActionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _applyControlActionRequests;
    private readonly IGroup<GameEntity> _moveControlActions;

    public ApplyMoveControlActionSystem(GameContext game)
    {
      _applyControlActionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ApplyControlAction, GameMatcher.PositionOnScreen));
      
      _moveControlActions = game.GetGroup(GameMatcher.MoveControlAction);
    }

    public void Execute()
    {
      foreach (GameEntity request in _applyControlActionRequests)
      foreach (GameEntity _ in _moveControlActions)
      {
        CreateEntity.Empty()
          .AddPositionOnScreen(request.PositionOnScreen)
          .With(x => x.isChangeEndDestinationRequest = true);

        CreateEntity.Empty()
          .AddPositionOnScreen(request.PositionOnScreen)
          .With(x => x.isCreateMoveClickIndicator = true);
        
        CreateEntity.Empty()
          .With(x => x.isCancelControlAction = true);
      }
    }
  }
}
