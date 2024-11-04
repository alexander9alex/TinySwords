using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CreateMoveRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _interactionRequests;

    public CreateMoveRequestSystem(GameContext game)
    {
      _interactionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.InteractionRequest, GameMatcher.PositionOnScreen));
    }

    public void Execute()
    {
      foreach (GameEntity request in _interactionRequests)
      {
        // future logic to interact done here
        
        CreateEntity.Empty()
          .With(x => x.isMoveRequest = true)
          .AddPositionOnScreen(request.PositionOnScreen);
      }
    }
  }
}
