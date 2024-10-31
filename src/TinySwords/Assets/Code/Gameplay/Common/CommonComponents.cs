using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Gameplay.Common
{
  [Game] public class Id : IComponent { [PrimaryEntityIndex] public int Value; }
}
