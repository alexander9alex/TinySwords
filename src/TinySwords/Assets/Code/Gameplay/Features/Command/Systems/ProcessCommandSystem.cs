using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ProcessCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _updateCommandRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public ProcessCommandSystem(GameContext game)
    {
      _updateCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.UpdateCommand, GameMatcher.CommandTypeId, GameMatcher.PositionOnScreen));
    }

    public void Execute()
    {
      foreach (GameEntity request in _updateCommandRequests.GetEntities(_buffer))
      {
        CreateEntity.Empty()
          .AddPositionOnScreen(request.PositionOnScreen)
          .With(x => x.isChangeEndDestinationRequest = true);

        CreateEntity.Empty()
          .AddPositionOnScreen(request.PositionOnScreen)
          .With(x => x.isCreateMoveClickIndicator = true);

        request.isDestructed = true;
      }
    }
  }
}
