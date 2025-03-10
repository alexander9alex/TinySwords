//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherUpdateFieldOfVisionInterval;

    public static Entitas.IMatcher<GameEntity> UpdateFieldOfVisionInterval {
        get {
            if (_matcherUpdateFieldOfVisionInterval == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.UpdateFieldOfVisionInterval);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherUpdateFieldOfVisionInterval = matcher;
            }

            return _matcherUpdateFieldOfVisionInterval;
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

    public Code.Gameplay.Features.CollectEntities.UpdateFieldOfVisionInterval updateFieldOfVisionInterval { get { return (Code.Gameplay.Features.CollectEntities.UpdateFieldOfVisionInterval)GetComponent(GameComponentsLookup.UpdateFieldOfVisionInterval); } }
    public float UpdateFieldOfVisionInterval { get { return updateFieldOfVisionInterval.Value; } }
    public bool hasUpdateFieldOfVisionInterval { get { return HasComponent(GameComponentsLookup.UpdateFieldOfVisionInterval); } }

    public GameEntity AddUpdateFieldOfVisionInterval(float newValue) {
        var index = GameComponentsLookup.UpdateFieldOfVisionInterval;
        var component = (Code.Gameplay.Features.CollectEntities.UpdateFieldOfVisionInterval)CreateComponent(index, typeof(Code.Gameplay.Features.CollectEntities.UpdateFieldOfVisionInterval));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceUpdateFieldOfVisionInterval(float newValue) {
        var index = GameComponentsLookup.UpdateFieldOfVisionInterval;
        var component = (Code.Gameplay.Features.CollectEntities.UpdateFieldOfVisionInterval)CreateComponent(index, typeof(Code.Gameplay.Features.CollectEntities.UpdateFieldOfVisionInterval));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveUpdateFieldOfVisionInterval() {
        RemoveComponent(GameComponentsLookup.UpdateFieldOfVisionInterval);
        return this;
    }
}
