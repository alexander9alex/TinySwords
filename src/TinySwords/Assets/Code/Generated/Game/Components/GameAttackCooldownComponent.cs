//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherAttackCooldown;

    public static Entitas.IMatcher<GameEntity> AttackCooldown {
        get {
            if (_matcherAttackCooldown == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AttackCooldown);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAttackCooldown = matcher;
            }

            return _matcherAttackCooldown;
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

    public Code.Gameplay.Features.Battle.AttackCooldown attackCooldown { get { return (Code.Gameplay.Features.Battle.AttackCooldown)GetComponent(GameComponentsLookup.AttackCooldown); } }
    public float AttackCooldown { get { return attackCooldown.Value; } }
    public bool hasAttackCooldown { get { return HasComponent(GameComponentsLookup.AttackCooldown); } }

    public GameEntity AddAttackCooldown(float newValue) {
        var index = GameComponentsLookup.AttackCooldown;
        var component = (Code.Gameplay.Features.Battle.AttackCooldown)CreateComponent(index, typeof(Code.Gameplay.Features.Battle.AttackCooldown));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceAttackCooldown(float newValue) {
        var index = GameComponentsLookup.AttackCooldown;
        var component = (Code.Gameplay.Features.Battle.AttackCooldown)CreateComponent(index, typeof(Code.Gameplay.Features.Battle.AttackCooldown));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveAttackCooldown() {
        RemoveComponent(GameComponentsLookup.AttackCooldown);
        return this;
    }
}
