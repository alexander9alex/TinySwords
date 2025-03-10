//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherReachedTargetBuffer;

    public static Entitas.IMatcher<GameEntity> ReachedTargetBuffer {
        get {
            if (_matcherReachedTargetBuffer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ReachedTargetBuffer);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherReachedTargetBuffer = matcher;
            }

            return _matcherReachedTargetBuffer;
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

    public Code.Gameplay.Features.CollectEntities.ReachedTargetBuffer reachedTargetBuffer { get { return (Code.Gameplay.Features.CollectEntities.ReachedTargetBuffer)GetComponent(GameComponentsLookup.ReachedTargetBuffer); } }
    public System.Collections.Generic.List<int> ReachedTargetBuffer { get { return reachedTargetBuffer.Value; } }
    public bool hasReachedTargetBuffer { get { return HasComponent(GameComponentsLookup.ReachedTargetBuffer); } }

    public GameEntity AddReachedTargetBuffer(System.Collections.Generic.List<int> newValue) {
        var index = GameComponentsLookup.ReachedTargetBuffer;
        var component = (Code.Gameplay.Features.CollectEntities.ReachedTargetBuffer)CreateComponent(index, typeof(Code.Gameplay.Features.CollectEntities.ReachedTargetBuffer));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceReachedTargetBuffer(System.Collections.Generic.List<int> newValue) {
        var index = GameComponentsLookup.ReachedTargetBuffer;
        var component = (Code.Gameplay.Features.CollectEntities.ReachedTargetBuffer)CreateComponent(index, typeof(Code.Gameplay.Features.CollectEntities.ReachedTargetBuffer));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveReachedTargetBuffer() {
        RemoveComponent(GameComponentsLookup.ReachedTargetBuffer);
        return this;
    }
}
