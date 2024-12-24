using Code.Gameplay.Features.Destruct;
using UnityEngine;

namespace Code.Tests.Tools
{
  public static class Destruct
  {
    public static void AllEntities(ProcessDestructedFeature processDestructedFeature, GameContext gameContext)
    {
      foreach (GameEntity entity in gameContext.GetEntities())
        entity.isDestructed = true;

      processDestructedFeature.Execute();
      processDestructedFeature.Cleanup();
    }

    public static void CoroutineRunner() =>
      Object.Destroy(GameObject.Find(nameof(Infrastructure.Common.CoroutineRunner.CoroutineRunner)));
  }
}
