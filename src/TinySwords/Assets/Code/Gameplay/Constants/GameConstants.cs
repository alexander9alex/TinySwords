using System.Collections.Generic;
using Code.Gameplay.Features.ControlAction.Data;
using UnityEngine;

namespace Code.Gameplay.Constants
{
  public static class GameConstants
  {
    public const float SelectionClickDelta = 10;
    public const float ClickRadius = 0.01f;
    public const float MakeDecisionInterval = 2.5f; // todo: change to 0.5f

    public static readonly int SelectionLayerMask = LayerMask.GetMask("Unit", "Building");
    public static readonly int InteractionLayerMask = LayerMask.GetMask("Unit", "Building");
    public static readonly int UILayer = LayerMask.NameToLayer("UI");
    
    public static readonly List<UnitCommandTypeId> AllUnitCommands = new()
    {
      UnitCommandTypeId.Move,
      UnitCommandTypeId.MoveWithAttack,
    };
  }
}