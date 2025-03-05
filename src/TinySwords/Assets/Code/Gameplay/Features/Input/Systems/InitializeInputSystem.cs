using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Input.Services;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class InitializeInputSystem : IInitializeSystem
  {
    private readonly IInputService _inputService;

    public InitializeInputSystem(IInputService inputService) =>
      _inputService = inputService;

    public void Initialize()
    {
      GameEntity input = CreateEntity.Empty()
        .With(x => x.isInput = true);

      _inputService.SetInputEntity(input);
      _inputService.StartInput();
    }
  }
}
