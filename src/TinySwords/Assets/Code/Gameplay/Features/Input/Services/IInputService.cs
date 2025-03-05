using UnityEngine;

namespace Code.Gameplay.Features.Input.Services
{
  public interface IInputService
  {
    void StartInput();
    void SetInputEntity(GameEntity input);
    void ChangeInputMap(Data.InputMap inputMap);
    bool PositionInGameZone(Vector2 pos);
    void Cleanup();
  }
}
