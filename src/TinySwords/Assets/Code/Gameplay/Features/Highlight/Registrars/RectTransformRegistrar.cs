using Code.Gameplay.Common.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Highlight.Registrars
{
  public class RectTransformRegistrar : EntityComponentRegistrar
  {
    public RectTransform RectTransform;
    public override void RegisterComponents() =>
      Entity.AddRectTransform(RectTransform);

    public override void UnregisterComponents()
    {
      if (Entity.hasRectTransform)
        Entity.RemoveRectTransform();
    }
  }
}
