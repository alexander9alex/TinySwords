using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using Entitas;
using ModestTree;

namespace Code.Gameplay.Features.FastInteraction.Systems
{
  public class UnitMoveFastInteractionSystem : IExecuteSystem
  {
    private readonly ISoundService _soundService;

    private readonly IGroup<GameEntity> _fastInteractionRequests;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _buffer = new(1);

    public UnitMoveFastInteractionSystem(GameContext game, ISoundService soundService)
    {
      _soundService = soundService;
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
            .AddCommandTypeId(CommandTypeId.Move)
            .With(x => x.isMoveCommand = true)
            .With(x => x.isProcessCommand = true);

          _soundService.PlaySound(SoundId.ApplyCommand); // todo: change interaction logic (through command service? make interaction service)
          
          request.isProcessed = true;
        }
      }
    }

    private bool AllSelectedIsUnits() =>
      _selected.AsEnumerable().All(selected => selected.isUnit) && !_selected.AsEnumerable().IsEmpty();
  }
}
