using Code.Gameplay.Features.FogOfWar.Services;
using Entitas;

namespace Code.Gameplay.Features.FogOfWar.Systems
{
  public class UpdateFogOfWarSystem : IExecuteSystem
  {
    private readonly IFogOfWarService _fogOfWarService;

    public UpdateFogOfWarSystem(IFogOfWarService fogOfWarService) =>
      _fogOfWarService = fogOfWarService;

    public void Execute() =>
      _fogOfWarService.UpdateFogOfWar();
  }
}
