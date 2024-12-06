using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Command.Services;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;

namespace Code.Gameplay.Features.FastInteraction.Services
{
  class FastInteractionService : IFastInteractionService
  {
    private readonly ISoundService _soundService;
    private readonly ICommandService _commandService;

    public FastInteractionService(ISoundService soundService, ICommandService commandService)
    {
      _soundService = soundService;
      _commandService = commandService;
    }

    public void MoveSelected(GameEntity request)
    {
      CreateEntity.Empty()
        .AddCommandTypeId(CommandTypeId.Move)
        .AddPositionOnScreen(request.PositionOnScreen)
        .With(x => x.isMoveCommand = true)
        .With(x => x.isProcessCommandRequest = true);

      _soundService.PlaySound(SoundId.ApplyCommand);
    }

    public void MakeAimedAttack(GameEntity request)
    {
      CreateEntity.Empty()
        .AddCommandTypeId(CommandTypeId.AimedAttack)
        .AddPositionOnScreen(request.PositionOnScreen)
        .With(x => x.isAimedAttackCommand = true)
        .With(x => x.isProcessCommandRequest = true);

      _soundService.PlaySound(SoundId.ApplyCommand);
    }

    public bool HasNotTargetInPosition(GameEntity request) =>
      !_commandService.CanProcessAimedAttack(out _, request);
  }
}
