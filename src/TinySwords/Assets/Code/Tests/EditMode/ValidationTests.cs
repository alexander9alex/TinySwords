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

    [TestCaseSource(nameof(AllScenesPaths))]
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

    private static IEnumerable<string> AllScenesPaths()
    {
      return AssetDatabase
        .FindAssets("t:Scene", new[] { ScenesDirPath })
        .Select(AssetDatabase.GUIDToAssetPath);
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
