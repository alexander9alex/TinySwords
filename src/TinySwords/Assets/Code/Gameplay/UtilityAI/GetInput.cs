using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.UtilityAI
{
  public class GetInput
  {
    private const float True = 1;
    private const float False = 0;

    public float HasEndDestination(GameEntity unit, UnitAction action) =>
      unit.hasEndDestination ? True : False;
  }
}
