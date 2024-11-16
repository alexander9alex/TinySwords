using System.Collections.Generic;
using Code.UI.Buttons.Data;
using UnityEngine;

namespace Code.Gameplay.Constants
{
  public static class GameConstants
  {
    public const float SelectionClickDelta = 10;
    public const float ClickRadius = 0.01f;
    
    public static readonly int SelectionLayerMask = LayerMask.GetMask("Unit", "Building");
    public static readonly int InteractionLayerMask = LayerMask.GetMask("Unit", "Building");
    public static readonly int UILayer = LayerMask.NameToLayer("UI");
    
    public static readonly List<ControlActionTypeId> AllActions = new()
    {
      ControlActionTypeId.Move,
      ControlActionTypeId.MoveWithAttack,
    };
  }
}