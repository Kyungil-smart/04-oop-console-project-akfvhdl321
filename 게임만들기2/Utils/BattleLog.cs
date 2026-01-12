using System.Collections.Generic;

public sealed class BattleLog
{
    private readonly Queue<string> _lines = new Queue<string>();

    public int MaxLines { get; }

    public BattleLog(int maxLines)
    {
        MaxLines = maxLines <= 0 ? 1 : maxLines;
    }

    public int Count => _lines.Count;

    public void Clear() => _lines.Clear();

    public void Push(string msg)
    {
        _lines.Enqueue(msg);

        while (_lines.Count > MaxLines)
            _lines.Dequeue();
    }

    public IEnumerable<string> Lines()
    {
        foreach (var line in _lines)
            yield return line;
    }
}