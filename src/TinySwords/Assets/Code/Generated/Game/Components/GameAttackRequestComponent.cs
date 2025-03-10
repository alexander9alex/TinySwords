//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherAttackRequest;

    public static Entitas.IMatcher<GameEntity> AttackRequest {
        get {
            if (_matcherAttackRequest == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AttackRequest);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAttackRequest = matcher;
            }

            return _matcherAttackRequest;
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

    static readonly Code.Gameplay.Features.Battle.AttackRequest attackRequestComponent = new Code.Gameplay.Features.Battle.AttackRequest();

    public bool isAttackRequest {
        get { return HasComponent(GameComponentsLookup.AttackRequest); }
        set {
            if (value != isAttackRequest) {
                var index = GameComponentsLookup.AttackRequest;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : attackRequestComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}
