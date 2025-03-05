namespace Code.Gameplay.Features.Input.Services
{
  public interface IInputService
  {
    void ChangeInputMap(Data.InputMap inputMap);
    void SetInputEntity(GameEntity input);
    void StartInput();
  }
}
