using System;

public class BattleScene : Scene
{
    private enum BattleState
    {
        PlayerSelect,
        WaitAfterPlayer,
        WaitAfterEnemy,
        BattleEnd
    }

    private BattleState _state;

    private Battler _player;
    private Battler _enemy;

    private MenuList _commandMenu;

    private BattleLog _log;
    private DamageCalculator _damage;

    private const int MaxLogLines = 6;

    public override void Enter()
    {
        InputManager.ResetKey();

        _player = new Battler("용사", maxHp: 120, atk: 18, def: 4);
        _enemy = new Battler("슬라임", maxHp: 80, atk: 14, def: 2);

        _log = new BattleLog(MaxLogLines);
        _damage = new DamageCalculator();

        _log.Clear();
        _log.Push("야생의 슬라임이 나타났다!");
        _log.Push("무엇을 할까?");

        _commandMenu = new MenuList(
            ("공격", OnAttack),
            ("스킬(잠김)", OnLocked),
            ("아이템(잠김)", OnLocked),
            ("도망", OnRun)
        );
        _commandMenu.Reset();

        _state = BattleState.PlayerSelect;
    }

    public override void Update()
    {
        switch (_state)
        {
            case BattleState.PlayerSelect:
                Update_PlayerSelect();
                break;

            case BattleState.WaitAfterPlayer:
                if (InputManager.GetKey(ConsoleKey.Enter))
                    EnemyTurn();
                break;

            case BattleState.WaitAfterEnemy:
                if (InputManager.GetKey(ConsoleKey.Enter))
                {
                    _log.Push("무엇을 할까?");
                    _state = BattleState.PlayerSelect;
                }
                break;

            case BattleState.BattleEnd:
                if (InputManager.GetKey(ConsoleKey.Enter))
                    SceneManager.ChangePrevScene();
                break;
        }
    }

    private void Update_PlayerSelect()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            _commandMenu.SelectUp();
            return;
        }

        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            _commandMenu.SelectDown();
            return;
        }

        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            _commandMenu.Select();
        }
    }

    public override void Render()
    {
        DrawHeader();
        DrawStatus();
        DrawLog();
        DrawCommandUI();
        DrawHint();
    }

    public override void Exit()
    {
        // 지금은 정리할 것 없음
    }

    // =========================
    // 커맨드 액션
    // =========================
    private void OnAttack()
    {
        if (_state != BattleState.PlayerSelect) return;

        int damage = _damage.CalcDamage(_player, _enemy);
        _enemy.TakeDamage(damage);

        _log.Push($"용사의 공격! {_enemy.Name}에게 {damage} 데미지!");

        if (_enemy.IsDead)
        {
            _log.Push($"{_enemy.Name}을(를) 쓰러뜨렸다!");
            _log.Push("승리!");
            _state = BattleState.BattleEnd;
            return;
        }

        _log.Push("[Enter] 다음");
        _state = BattleState.WaitAfterPlayer;
    }

    private void OnRun()
    {
        if (_state != BattleState.PlayerSelect) return;

        _log.Push("용사는 도망쳤다!");
        _log.Push("전투 종료.");
        _state = BattleState.BattleEnd;
    }

    private void OnLocked()
    {
        if (_state != BattleState.PlayerSelect) return;

        _log.Push("아직 사용할 수 없다.");
    }

    // =========================
    // 적 턴
    // =========================
    private void EnemyTurn()
    {
        int damage = _damage.CalcDamage(_enemy, _player);
        _player.TakeDamage(damage);

        _log.Push($"{_enemy.Name}의 공격! 용사에게 {damage} 데미지!");

        if (_player.IsDead)
        {
            _log.Push("용사가 쓰러졌다...");
            _log.Push("패배...");
            _state = BattleState.BattleEnd;
            return;
        }

        _log.Push("[Enter] 다음");
        _state = BattleState.WaitAfterEnemy;
    }

    // =========================
    // 출력(연출/UI)
    // =========================
    private void DrawHeader()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("            [ 턴제 전투 - JRPG ]         ");
        Console.WriteLine("========================================");
        Console.WriteLine();

        Console.WriteLine($"  ({_enemy.Name})            VS            ({_player.Name})");
        Console.WriteLine();
    }

    private void DrawStatus()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($" {_enemy.Name,-8} HP {_enemy.Hp,3}/{_enemy.MaxHp,3}   |   {_player.Name,-8} HP {_player.Hp,3}/{_player.MaxHp,3}");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine();
    }

    private void DrawLog()
    {
        Console.WriteLine("[ 로그 ]");

        int lineCount = 0;
        foreach (var line in _log.Lines())
        {
            Console.WriteLine($" - {line}");
            lineCount++;
        }

        for (int i = lineCount; i < MaxLogLines; i++)
            Console.WriteLine(" -");

        Console.WriteLine();
    }

    private void DrawCommandUI()
    {
        Console.WriteLine("[ 커맨드 ]");

        if (_state == BattleState.PlayerSelect)
        {
            _commandMenu.Render(x: 0, y: Console.CursorTop);
        }
        else
        {
            Console.WriteLine("  공격");
            Console.WriteLine("  스킬(잠김)");
            Console.WriteLine("  아이템(잠김)");
            Console.WriteLine("  도망");
        }

        Console.WriteLine();
    }

    private void DrawHint()
    {
        Console.WriteLine("----------------------------------------");

        switch (_state)
        {
            case BattleState.PlayerSelect:
                Console.WriteLine("조작: ↑/↓ 이동, Enter 선택");
                break;
            case BattleState.WaitAfterPlayer:
            case BattleState.WaitAfterEnemy:
                Console.WriteLine("Enter를 누르면 다음 진행");
                break;
            case BattleState.BattleEnd:
                Console.WriteLine("Enter를 누르면 이전 씬으로 돌아갑니다");
                break;
        }
    }
}