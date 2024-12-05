using Code.Common.Entities;
using Code.Common.Extensions;

namespace Code.Gameplay.Features.Command.Services
{
  public class ProcessCommandService : IProcessCommandService
  {
    public void ProcessIncorrectAimedAttack(GameEntity request)
    {
      CreateEntity.Empty()
        .AddPositionOnScreen(request.PositionOnScreen)
        .With(x => x.isCreateMoveIndicator = true);
    }
  }
}
