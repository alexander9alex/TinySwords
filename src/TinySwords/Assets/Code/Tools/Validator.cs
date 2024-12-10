using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Tools
{
  public abstract class Validator
  {
    [MenuItem("Tools/Validation/Missing Components")]
    public static void FindMissingComponents()
    {
      Debug.Log("Hello from Validator!");

      Scene scene = SceneManager.GetActiveScene();

      Queue<GameObject> gameObjectQueue = new Queue<GameObject>(scene.GetRootGameObjects());

      while (gameObjectQueue.Count > 0)
      {
        GameObject gameObject = gameObjectQueue.Dequeue();
        
        FindMissingComponents(gameObject);

        foreach (Transform child in gameObject.transform)
          gameObjectQueue.Enqueue(child.gameObject);
      }
    }

    private static void FindMissingComponents(GameObject gameObject) =>
      Debug.Log(gameObject.name);
  }
}
