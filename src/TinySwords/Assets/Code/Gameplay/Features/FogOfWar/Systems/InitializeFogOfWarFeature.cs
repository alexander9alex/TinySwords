using Code.Gameplay.Features.FogOfWar.Services;
using Entitas;

namespace Code.Gameplay.Features.FogOfWar.Systems
{
  public class InitializeFogOfWarFeature : IInitializeSystem
  {
    private readonly IFogOfWarService _fogOfWarService;

    public InitializeFogOfWarFeature(IFogOfWarService fogOfWarService) =>
      _fogOfWarService = fogOfWarService;

    public void Initialize() =>
      _fogOfWarService.InitializeFogOfWar();
  }
}
