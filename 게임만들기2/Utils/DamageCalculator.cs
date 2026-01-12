using System;

public sealed class DamageCalculator
{
    private readonly Random _rng;

    public DamageCalculator()
    {
        _rng = new Random();
    }

    // 기본 데미지: max(1, (공격 - 방어)) + 랜덤(0~3)
    public int CalcDamage(Battler attacker, Battler defender)
    {
        int baseDamage = Math.Max(1, attacker.Atk - defender.Def);
        int damage = baseDamage + _rng.Next(0, 4);
        return damage;
    }
}