public class Monster : GameObject
{
    public override string Icon => "👾";

    public Monster(int x, int y)
    {
        X = x;
        Y = y;
    }
}
