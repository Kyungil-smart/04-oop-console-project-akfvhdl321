public sealed class Battler
{
    public string Name { get; }
    public int MaxHp { get; }
    public int Hp { get; private set; }
    public int Atk { get; }
    public int Def { get; }

    public bool IsDead => Hp <= 0;

    public Battler(string name, int maxHp, int atk, int def)
    {
        Name = name;
        MaxHp = maxHp;
        Hp = maxHp;
        Atk = atk;
        Def = def;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0) damage = 0;

        Hp -= damage;
        if (Hp < 0) Hp = 0;
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;

        Hp += amount;
        if (Hp > MaxHp) Hp = MaxHp;
    }
}