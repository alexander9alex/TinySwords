using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.FastInteraction.Systems
{
  public class UnitMoveFastInteractionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _fastInteractionRequests;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _buffer = new(1);

    public UnitMoveFastInteractionSystem(GameContext game)
    {
      _fastInteractionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.FastInteraction, GameMatcher.PositionOnScreen)
        .NoneOf(GameMatcher.Processed));
      
      _selected = game.GetGroup(GameMatcher.Selected);
    }

    public void Execute()
    {
      foreach (GameEntity request in _fastInteractionRequests.GetEntities(_buffer))
      {
        if (AllSelectedIsUnits())
        {
          CreateEntity.Empty()
            .AddPositionOnScreen(request.PositionOnScreen)
            .With(x => x.isChangeEndDestinationRequest = true);

          CreateEntity.Empty()
            .AddPositionOnScreen(request.PositionOnScreen)
            .With(x => x.isCreateMoveClickIndicator = true);
          
          request.isProcessed = true;
        }
      }
    }

    private bool AllSelectedIsUnits() =>
      _selected.AsEnumerable().All(selected => selected.isUnit);
  }
}
