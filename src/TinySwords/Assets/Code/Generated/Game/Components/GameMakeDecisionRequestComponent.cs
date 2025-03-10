//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherMakeDecisionRequest;

    public static Entitas.IMatcher<GameEntity> MakeDecisionRequest {
        get {
            if (_matcherMakeDecisionRequest == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MakeDecisionRequest);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMakeDecisionRequest = matcher;
            }

            return _matcherMakeDecisionRequest;
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

    static readonly Code.Gameplay.Features.AI.MakeDecisionRequest makeDecisionRequestComponent = new Code.Gameplay.Features.AI.MakeDecisionRequest();

    public bool isMakeDecisionRequest {
        get { return HasComponent(GameComponentsLookup.MakeDecisionRequest); }
        set {
            if (value != isMakeDecisionRequest) {
                var index = GameComponentsLookup.MakeDecisionRequest;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : makeDecisionRequestComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}
