using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace WarPlane.Controllers;

public class GameTimerController
{
    private readonly Dictionary<int, List<Action>> _actionTicks = new();

    private readonly Timer _timer;

    private int _ticksCounter;

    public GameTimerController(int timerInterval)
    {
        _timer = new Timer(timerInterval);
        _timer.Elapsed += OnElapsed;
        
        _timer.Start();
    }

    private void OnElapsed(object sender, ElapsedEventArgs e)
    {
        foreach (var interval in _actionTicks.Keys.Where(interval => _ticksCounter % interval == 0))
        {
            _actionTicks[interval].ForEach(action => action.Invoke());
        }
        
        _ticksCounter++;

        if (_ticksCounter >= int.MaxValue)
            _ticksCounter = 0;
    }

    public void RegisterAction(Action action, int interval)
    {
        if (!_actionTicks.ContainsKey(interval))
            _actionTicks[interval] = new List<Action>();
        
        _actionTicks[interval].Add(action);
    }

    public void Stop() => _timer.Stop();
}