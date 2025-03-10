﻿using Code.Gameplay.Features.Command.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Command.Services
{
  public interface ICommandService
  {
    void SelectCommand(CommandTypeId command);
    void CancelCommand(bool isCommandProcessed);
    bool CanApplyCommand(CommandTypeId command, Vector2 screenPos);
    void ApplyCommand(CommandTypeId command, Vector2 screenPos);
    void ProcessIncorrectCommand(CommandTypeId commandTypeId, Vector2 screenPos);
  }
}
