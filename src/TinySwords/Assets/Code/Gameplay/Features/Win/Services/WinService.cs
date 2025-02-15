using UnityEngine;

namespace Code.Gameplay.Features.Win.Services
{
  class WinService : IWinService
  {
    public void Win()
    {
      Debug.Log("You win!");
    }
  }
}
