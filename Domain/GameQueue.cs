using System.Timers;
using Infrastructure.Database.Tables;

namespace Domain;

public class GameQueue
{
    public string GameId { get => game.Id; }
    public int ServerId { get; }

    private int queueTimeout = 10;
    private Game game;
    private CancellationTokenSource token;
    private System.Timers.Timer timer;

    private Action<int> action;
    private Action<int, string> onEndAction;

    public GameQueue(
        Game game, 
        int serverId,  
        Action<int> action, 
        Action<int, string> onEndAction
    )
    {
        ServerId = serverId;
        this.game = game;
        this.action = action;
        this.onEndAction = onEndAction;
        token = new CancellationTokenSource();
        StartQueue(token.Token);
    }

    private void StartQueue(CancellationToken token)
    { 
        timer = new(1000)
        {
            AutoReset = true,
            Enabled = true
        };
        timer.Elapsed += QueueStep;
        Console.WriteLine("im here");
    }

    private void QueueStep(Object source, ElapsedEventArgs e)
    {
        queueTimeout--;
        if(queueTimeout == 0) QueueEnd();
        action(queueTimeout);
    }

    private void QueueEnd()
        => onEndAction(game.MaxPlayersCount, GameId);

    public void StopQueue()
    {
        token.Cancel();
        timer.Dispose();
    }
}
