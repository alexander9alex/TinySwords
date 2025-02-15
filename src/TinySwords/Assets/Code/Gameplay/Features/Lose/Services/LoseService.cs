using UnityEngine;

namespace Code.Gameplay.Features.Lose.Services
{
  public class LoseService : ILoseService
  {
    public void Lose()
    {
      Debug.Log("You lose!");
    }
  }
}
