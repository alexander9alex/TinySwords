using Code.Gameplay.Features.ControlAction.Data;
using UnityEngine;

namespace Code.Gameplay.Features.ControlAction.Configs
{
  [CreateAssetMenu(menuName = "Static Data/UI/Unit Command UI Config", fileName = "UnitCommandUIConfig", order = 0)]
  public class UnitActionUIConfig : ScriptableObject
  {
    public UnitCommandTypeId UnitCommandTypeId;
    public GameObject UnitActionButtonPrefab;
    public GameObject UnitActionDescriptionPrefab;
  }
}