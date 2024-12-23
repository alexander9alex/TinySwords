using System.Collections.Generic;
using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Units.Data;
using UnityEngine;

namespace Code.Gameplay.Constants
{
  public static class GameConstants
  {
    public const float SelectionClickDelta = 10;
    public const float ClickRadius = 0.01f;
    public static float FocusRadius => ClickRadius;
    public const float UnitMinRadius = 0.7f;
    public const float CameraSpeed = 0.2f;
    public const float StoppingDistance = 0.01f;
    public const float MinLastTimeToUpdateFieldOfVision = 0.25f;
    public const TeamColor UserTeamColor = TeamColor.Blue;

    public static readonly int SelectionLayerMask = LayerMask.GetMask("Unit", "Building");
    public static int FocusLayerMask => SelectionLayerMask;
    public static readonly int UILayer = LayerMask.NameToLayer("UI");
    public static readonly int UnitsAndBuildingsLayerMask = LayerMask.GetMask("Unit", "Building");

    public static readonly List<CommandTypeId> AllUnitCommands = new()
    {
      CommandTypeId.Move,
      CommandTypeId.MoveWithAttack,
      CommandTypeId.AimedAttack,
    };

    public static readonly Dictionary<TeamColor, TeamColor> AllyTeamColor = new()
    {
      { TeamColor.Blue, TeamColor.White },
      { TeamColor.White, TeamColor.Blue },
      { TeamColor.Red, TeamColor.Red },
    };
  }
}
