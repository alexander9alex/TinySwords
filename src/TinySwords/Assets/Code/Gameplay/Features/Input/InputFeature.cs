using Code.Gameplay.Features.Input.Systems;
using Code.Infrastructure.Factory;

namespace Code.Gameplay.Features.Input
{
  public sealed class InputFeature : Feature
  {
    public InputFeature(ISystemFactory systems)
    {
      Add(systems.Create<CreateLeftClickSystem>());
      
      Add(systems.Create<CleanupLeftClickSystem>());
      Add(systems.Create<CleanupLeftClickInputsSystem>());
      
      Add(systems.Create<CleanupRightClickSystem>());
      
      Add(systems.Create<CleanupMousePositionSystem>());
    }
  }
}
