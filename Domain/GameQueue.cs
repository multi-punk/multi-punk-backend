using System.Timers;
using Infrastructure.Database.Tables;

namespace Domain;

public class GameQueue
{
    public string GameId { get => game.Id; }
    public int? ServerId { get; set;}

    private int queueTimeout = 10;
    private Game game;
    private CancellationTokenSource token;
    private System.Timers.Timer timer;

    private Func<int, string, Task> onEndAction;
    private Func<int, string, int, GameQueue, Task<int>> action;

    public GameQueue(
        Game game, 
        int? serverId,  
        Func<int, string, Task> onEndAction,
        Func<int, string, int, GameQueue, Task<int>> action
    )
    {
        ServerId = serverId;
        this.game = game;
        this.action = action;
        this.onEndAction = onEndAction;

        timer = new(1000)
        {
            AutoReset = true,
            Enabled = true
        };

        token = new CancellationTokenSource();
        StartQueue(token.Token);
    }

    private void StartQueue(CancellationToken token)
    { 
        timer.Elapsed += QueueStep;
        Console.WriteLine("im here");
    }

    private async void QueueStep(object? source, ElapsedEventArgs e)
    {
        if(queueTimeout <= 0) QueueEnd();
        queueTimeout = await action(queueTimeout, GameId, game.MaxPlayersCount, this);
    }

    private void QueueEnd()
    {
        StopQueue();
        Console.WriteLine("im there");
        onEndAction(game.MaxPlayersCount, GameId);
    }

    public void StopQueue()
    {
        token.Cancel();
        timer.Stop();
        timer.Dispose();
    }
}
