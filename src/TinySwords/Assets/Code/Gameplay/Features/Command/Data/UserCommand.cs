using UnityEngine;

namespace Code.Gameplay.Features.Command.Data
{
  public class UserCommand
  {
    public CommandTypeId CommandTypeId;
    public Vector2? WorldPosition;
    public int? TargetId;
  }
}