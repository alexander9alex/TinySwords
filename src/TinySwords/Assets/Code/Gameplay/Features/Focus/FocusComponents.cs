using Entitas;

namespace Code.Gameplay.Features.Focus
{
  [Game] public class Focused : IComponent { }
  [Game] public class Unfocused : IComponent { }
  [Game] public class Focusing : IComponent { }
  [Game] public class FocusRequest : IComponent { }
}
