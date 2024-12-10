using UnityEditor;
using UnityEngine;

namespace Code.Tools
{
  public abstract class Validator
  {
    [MenuItem("Tools/Validation/Missing Components")]
    public static void FindMissingComponents() =>
      Debug.Log("Hello from Validator!");
  }
}
