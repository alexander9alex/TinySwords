using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.FastInteract.Systems
{
  public class CreateMoveRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _interactionRequests;
    private readonly List<GameEntity> _buffer = new(1);
    private readonly IGroup<GameEntity> _selectedAndMovable;

    public CreateMoveRequestSystem(GameContext game)
    {
      _interactionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.FastInteraction, GameMatcher.PositionOnScreen)
        .NoneOf(GameMatcher.Processed));

      _selectedAndMovable = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Selected, GameMatcher.Movable));
    }

    public void Execute()
    {
      foreach (GameEntity request in _interactionRequests.GetEntities(_buffer))
      {
        if (_selectedAndMovable.count == 0)
          return;

        CreateEntity.Empty()
          .With(x => x.isMoveRequest = true)
          .AddPositionOnScreen(request.PositionOnScreen);

        request.isProcessed = true;
      }
    }
  }
}
