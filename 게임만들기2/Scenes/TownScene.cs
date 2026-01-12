using System;
using 게임만들기.Scenes;

public class TownScene : Scene
{
    private PlayerCharacter _player;
    private ConsoleKey lastKey = ConsoleKey.None;

    // ✅ GameManager에서 요구하는 생성자
    public TownScene(PlayerCharacter player)
    {
        _player = player;
    }

    public override void Enter()
    {
        lastKey = ConsoleKey.None;
    }

    public override void Update()
    {
        // ⚠️ InputManager를 이미 쓰고 있다면
        // Console.ReadKey는 여기서 쓰지 않는다

        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            SceneManager.Change("Battle");
        }
    }

    public override void Render()
    {
        Console.WriteLine("=== 마을 ===");
        Console.WriteLine();
        Console.WriteLine($"플레이어 이름 : {_player.Name}");
        Console.WriteLine($"마지막 키 : {lastKey}");
        Console.WriteLine();
        Console.WriteLine("Enter : 전투 진입");
    }

    public override void Exit()
    {
        // 필요 시 나중에 정리 로직 추가
    }
}
