using Code.UI.Data;
using UnityEngine;

namespace Code.UI.Windows.Data
{
  public class WindowConfig : ScriptableObject
  {
    public WindowId WindowId;
    
    [Header("Window Setup")]
    public GameObject WindowPrefab;
  }
}