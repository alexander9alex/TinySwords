using UnityEngine;

namespace Code.Gameplay.Constants
{
  public static class GameConstants
  {
    public const float SelectionClickDelta = 10;
    public const float ClickRadius = 0.01f;
    
    public static readonly int SelectionLayerMask = LayerMask.GetMask("Unit", "Building");
    public static readonly int InteractionLayerMask = LayerMask.GetMask("Unit", "Building");
  }
}