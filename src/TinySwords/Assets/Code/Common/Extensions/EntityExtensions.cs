using System;
using Entitas;

namespace Code.Common.Extensions
{
  public static class EntityExtensions
  {
    public static T With<T>(this T self, Action<T> set)
    {
      set?.Invoke(self);
      return self;
    }

    public static T With<T>(this T entity, Action<T> apply, bool when)
    {
      if (when)
        apply?.Invoke(entity);
      
      return entity;
    }
  }
}
