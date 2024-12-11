using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Tests.EditMode
{
  public class ValidationTests
  {
    private const string ScenesDirPath = "Assets/Scenes";

    [TestCase("Assets/Scenes/Boot.unity")]
    [TestCase("Assets/Scenes/Game.unity")]
    [TestCase("Assets/Scenes/MainMenu.unity")]
    public void AllGameObjectsShouldNotHaveMissingScripts(string scenePath)
    {
      Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

      List<string> gameObjectsWithMissingScripts = AllGameObjects(scene)
        .Where(HasMissingScripts)
        .Select(gameObject => gameObject.name)
        .ToList();

      EditorSceneManager.CloseScene(scene, removeScene: true);

      gameObjectsWithMissingScripts.Should().BeEmpty();
    }

    private static IEnumerable<Scene> OpenProjectScenes()
    {
      IEnumerable<string> scenePaths = AssetDatabase
        .FindAssets("t:Scene", new[] { ScenesDirPath })
        .Select(AssetDatabase.GUIDToAssetPath);

      foreach (string scenePath in scenePaths)
      {
        Scene scene = SceneManager.GetSceneByPath(scenePath);

        if (scene.isLoaded)
        {
          yield return scene;
        }
        else
        {
          Scene openedScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);

          yield return openedScene;

          EditorSceneManager.CloseScene(openedScene, removeScene: true);
        }
      }
    }

    private static IEnumerable<GameObject> AllGameObjects(Scene scene)
    {
      Queue<GameObject> gameObjectQueue = new(scene.GetRootGameObjects());

      while (gameObjectQueue.Count > 0)
      {
        GameObject gameObject = gameObjectQueue.Dequeue();

        yield return gameObject;

        foreach (Transform child in gameObject.transform)
          gameObjectQueue.Enqueue(child.gameObject);
      }
    }

    private bool HasMissingScripts(GameObject gameObject) =>
      GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject) > 0;
  }
}
