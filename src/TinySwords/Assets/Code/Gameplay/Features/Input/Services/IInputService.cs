using UnityEngine.UI;

namespace Code.Gameplay.Features.Input.Services
{
  public interface IInputService
  {
    void ChangeInputMap(Data.InputMap inputMap);
    void SetGameZoneButton(Image gameZoneLayout);
  }
}
