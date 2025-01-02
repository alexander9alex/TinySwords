using Code.Gameplay.Features.FogOfWar.Services;
using Entitas;

namespace Code.Gameplay.Features.FogOfWar.Systems
{
  public class ClearGlowingObjectsSystem : IExecuteSystem
  {
    private readonly IFogOfWarService _fogOfWarService;

    public ClearGlowingObjectsSystem(IFogOfWarService fogOfWarService) =>
      _fogOfWarService = fogOfWarService;

    public void Execute() =>
      _fogOfWarService.ClearGlowingObjects();
  }
}
