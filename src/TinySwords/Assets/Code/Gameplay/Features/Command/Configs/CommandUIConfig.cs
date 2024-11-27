using Code.Gameplay.Features.Command.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Command.Configs
{
  [CreateAssetMenu(menuName = "Static Data/UI/Command UI Config", fileName = "CommandUIConfig", order = 0)]
  public class CommandUIConfig : ScriptableObject
  {
    public CommandTypeId CommandTypeId;
    public GameObject CommandButtonPrefab;
    public GameObject CommandDescriptionPrefab;
  }
}