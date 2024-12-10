using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Tools
{
  public abstract class Validator
  {
    private const string ScenesDirPath = "Assets/Scenes";

    [MenuItem("Tools/Validation/~ Iterate Scenes")]
    public static void IterateScenes()
    {
      IEnumerable<string> scenePaths = AssetDatabase.FindAssets("t:Scene", new[] { ScenesDirPath })
        .Select(AssetDatabase.GUIDToAssetPath);

      foreach (string scenePath in scenePaths)
      {
        if (SceneManager.GetSceneByPath(scenePath).isLoaded)
        {
          Debug.Log(SceneManager.GetSceneByPath(scenePath).name);
        }
        else
        {
          Scene openedScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
          Debug.Log(openedScene.name);
          EditorSceneManager.CloseScene(openedScene, removeScene: true);
        }
      }
    }

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

    private static void FindMissingComponents(GameObject gameObject)
    {
      bool hasMissingScripts = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject) > 0;

      if (hasMissingScripts)
        Debug.LogWarning($"Game object {gameObject.name} has missing component(s)");
    }
  }
}
