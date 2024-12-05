using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Indicators.Data;

namespace Code.Gameplay.Features.Command.Services
{
  public class ProcessCommandService : IProcessCommandService
  {
    public void ProcessIncorrectAimedAttack(GameEntity request)
    {
      CreateEntity.Empty()
        .AddIndicatorTypeId(IndicatorTypeId.IncorrectCommand)
        .AddPositionOnScreen(request.PositionOnScreen)
        .With(x => x.isCreateIndicator = true);
    }
  }
}
