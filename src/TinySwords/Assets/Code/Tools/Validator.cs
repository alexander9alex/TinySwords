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

    [MenuItem("Tools/Validation/Missing Components")]
    public static void FindMissingComponents()
    {
      Debug.Log("Searching for missing components...");

      IEnumerable<string> scenePaths = AssetDatabase
        .FindAssets("t:Scene", new[] { ScenesDirPath })
        .Select(AssetDatabase.GUIDToAssetPath);

      foreach (string scenePath in scenePaths)
      {
        Scene scene = SceneManager.GetSceneByPath(scenePath);

        if (scene.isLoaded)
        {
          FindMissingComponents(scene);
        }
        else
        {
          Scene openedScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
          FindMissingComponents(openedScene);
          EditorSceneManager.CloseScene(openedScene, removeScene: true);
        }
      }
    }

    private static void FindMissingComponents(Scene scene)
    {
      Queue<GameObject> gameObjectQueue = new(scene.GetRootGameObjects());

      while (gameObjectQueue.Count > 0)
      {
        GameObject gameObject = gameObjectQueue.Dequeue();

        FindMissingComponents(gameObject, scene);

        foreach (Transform child in gameObject.transform)
          gameObjectQueue.Enqueue(child.gameObject);
      }
    }

    private static void FindMissingComponents(GameObject gameObject, Scene scene)
    {
      bool hasMissingScripts = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject) > 0;

      if (hasMissingScripts)
        Debug.LogError($"Game object {gameObject.name} from scene {scene.name} has missing component(s)");
    }
  }
}
