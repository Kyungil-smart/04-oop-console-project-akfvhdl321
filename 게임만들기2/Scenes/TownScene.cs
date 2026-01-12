using System;
using 게임만들기.Scenes;

public class TownScene : Scene
{
    private PlayerCharacter _player;
    private ConsoleKey lastKey = ConsoleKey.None;

    
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
        _player.Update();

        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            SceneManager.Change("Battle");
        }
    }

    public override void Render()
    {
        Console.Clear();

        Console.WriteLine("=== 마을 ===");
        Console.WriteLine($"플레이어 이름 : {_player.Name}");
        Console.WriteLine();
        Console.WriteLine("Enter : 전투 진입");

        _player.Render(); // ⭐ 캐릭터 직접 출력
    }

    public override void Exit()
    {
        // 필요 시 나중에 정리 로직 추가
    }
}
