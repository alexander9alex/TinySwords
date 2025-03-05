using UnityEngine;

namespace Code.Gameplay.Features.Input.Factory
{
  public interface IHighlightFactory
  {
    GameEntity CreateHighlight(Vector2 startScreenPos, Vector2 endScreenPos);
  }
}
