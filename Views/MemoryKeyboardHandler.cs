using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WarPlane.Views;

public class MemoryKeyboardHandler
{
    private readonly HashSet<Keys> _pressedKeys = new HashSet<Keys>();

    private readonly Dictionary<Keys, Action> _keyHandlers = new Dictionary<Keys, Action>();
    
    public void RegisterKeyHandler(Keys keyCode, Action action)
    {
        _keyHandlers.Add(keyCode, action);
    }

    public void AddPressedKey(Keys keyCode) => _pressedKeys.Add(keyCode);

    public void RemovePressedKey(Keys keyCode) => _pressedKeys.Remove(keyCode);

    public void InvokeNecessaryActions()
    {
        foreach (var key in _keyHandlers.Keys.Where(key => _pressedKeys.Contains(key)))
        {
            _keyHandlers[key].Invoke();
        }
    }
}