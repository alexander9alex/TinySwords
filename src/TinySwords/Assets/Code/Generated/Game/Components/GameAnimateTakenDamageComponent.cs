//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherAnimateTakenDamage;

    public static Entitas.IMatcher<GameEntity> AnimateTakenDamage {
        get {
            if (_matcherAnimateTakenDamage == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AnimateTakenDamage);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAnimateTakenDamage = matcher;
            }

            return _matcherAnimateTakenDamage;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Code.Gameplay.Features.Effects.AnimateTakenDamage animateTakenDamageComponent = new Code.Gameplay.Features.Effects.AnimateTakenDamage();

    public bool isAnimateTakenDamage {
        get { return HasComponent(GameComponentsLookup.AnimateTakenDamage); }
        set {
            if (value != isAnimateTakenDamage) {
                var index = GameComponentsLookup.AnimateTakenDamage;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : animateTakenDamageComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}
