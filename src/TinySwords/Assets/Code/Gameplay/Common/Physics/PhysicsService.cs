using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;
using UnityEngine;

namespace Code.Gameplay.Common.Physics
{
  public class PhysicsService : IPhysicsService
  {
    private static readonly RaycastHit2D[] Hits = new RaycastHit2D[128];
    private static readonly Collider2D[] OverlapHits = new Collider2D[128];

    private readonly ICollisionRegistry _collisionRegistry;

    public PhysicsService(ICollisionRegistry collisionRegistry) =>
      _collisionRegistry = collisionRegistry;

    public IEnumerable<GameEntity> RaycastAll(Vector2 worldPosition, Vector2 direction, int layerMask)
    {
      int hitCount = Physics2D.RaycastNonAlloc(worldPosition, direction, Hits, layerMask);

      for (int i = 0; i < hitCount; i++)
      {
        RaycastHit2D hit = Hits[i];
        if (hit.collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID());
        if (entity == null)
          continue;

        yield return entity;
      }
    }

    public GameEntity Raycast(Vector2 worldPosition, Vector2 direction, int layerMask)
    {
      int hitCount = Physics2D.RaycastNonAlloc(worldPosition, direction, Hits, layerMask);

      for (int i = 0; i < hitCount; i++)
      {
        RaycastHit2D hit = Hits[i];
        if (hit.collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID());
        if (entity == null)
          continue;

        return entity;
      }

      return null;
    }

    public GameEntity LineCast(Vector2 start, Vector2 end, int layerMask)
    {
      int hitCount = Physics2D.RaycastNonAlloc(start, end, Hits, layerMask);

      for (int i = 0; i < hitCount; i++)
      {
        RaycastHit2D hit = Hits[i];
        if (hit.collider == null)
          continue;

        GameEntity entity = _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID());
        if (entity == null)
          continue;

        return entity;
      }

      return null;
    }

    public IEnumerable<GameEntity> CircleCast(Vector3 position, float radius, int layerMask)
    {
      int hitCount = OverlapCircle(position, radius, OverlapHits, layerMask);

      DrawDebugCircle(position, radius, 1f, Color.red);

      for (int i = 0; i < hitCount; i++)
      {
        GameEntity entity = _collisionRegistry.Get<GameEntity>(OverlapHits[i].GetInstanceID());
        if (entity == null)
          continue;

        yield return entity;
      }
    }

    public int CircleCastNonAlloc(Vector3 position, float radius, int layerMask, GameEntity[] hitBuffer)
    {
      int hitCount = OverlapCircle(position, radius, OverlapHits, layerMask);

      DrawDebugCircle(position, radius, 1f, Color.green);

      for (int i = 0; i < hitCount; i++)
      {
        GameEntity entity = _collisionRegistry.Get<GameEntity>(OverlapHits[i].GetInstanceID());
        if (entity == null)
          continue;

        if (i < hitBuffer.Length)
          hitBuffer[i] = entity;
      }

      return hitCount;
    }

    public IEnumerable<GameEntity> BoxCast(Vector3 position, Vector2 size, int layerMask)
    {
      int hitCount = OverlapBox(position, size, layerMask, angle: 0f);

      DrawDebugBox(position, size, 10f, Color.magenta);

      for (int i = 0; i < hitCount; i++)
      {
        GameEntity entity = _collisionRegistry.Get<GameEntity>(OverlapHits[i].GetInstanceID());
        if (entity == null)
          continue;

        yield return entity;
      }
    }

    public TEntity OverlapPoint<TEntity>(Vector2 worldPosition, int layerMask) where TEntity : class
    {
      int hitCount = Physics2D.OverlapPointNonAlloc(worldPosition, OverlapHits, layerMask);

      for (int i = 0; i < hitCount; i++)
      {
        Collider2D hit = OverlapHits[i];
        if (hit == null)
          continue;

        TEntity entity = _collisionRegistry.Get<TEntity>(hit.GetInstanceID());
        if (entity == null)
          continue;

        return entity;
      }

      return null;
    }

    public int OverlapCircle(Vector3 worldPos, float radius, Collider2D[] hits, int layerMask) =>
      Physics2D.OverlapCircleNonAlloc(worldPos, radius, hits, layerMask);

    private static int OverlapBox(Vector3 position, Vector2 size, int layerMask, float angle) =>
      Physics2D.OverlapBoxNonAlloc(position, size, angle, OverlapHits, layerMask);

    private static void DrawDebugCircle(Vector2 worldPos, float radius, float seconds, Color color)
    {
      Debug.DrawRay(worldPos, radius * Vector3.up, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.down, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.left, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.right, color, seconds);
    }

    private static void DrawDebugBox(Vector2 worldPos, Vector2 size, float seconds, Color color)
    {
      Debug.DrawRay(worldPos - size / 2, size.y * Vector2.up, color, seconds);
      Debug.DrawRay(worldPos - size / 2, size.x * Vector2.right, color, seconds);
      Debug.DrawRay(worldPos + size / 2, size.y * Vector2.down, color, seconds);
      Debug.DrawRay(worldPos + size / 2, size.x * Vector2.left, color, seconds);
    }
  }
}
