public class Player : GameObject
{
    public override string Icon => "🧙";

    public Player(int startX, int startY)
    {
        X = startX;
        Y = startY;
    }

    public void Update()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
            Y--;

        if (InputManager.GetKey(ConsoleKey.DownArrow))
            Y++;

        if (InputManager.GetKey(ConsoleKey.LeftArrow))
            X--;

        if (InputManager.GetKey(ConsoleKey.RightArrow))
            X++;
    }
}
