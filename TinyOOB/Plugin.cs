using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Plugin.Services;
using System;
using System.Threading;

namespace TinyOOB;

public sealed class Plugin : IDalamudPlugin {
    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;

    public Plugin() {
        CommandManager.AddHandler("/frz", new CommandInfo(OnFreezeGame) {
            ShowInHelp = false
        });
    }

    public void Dispose() {
        CommandManager.RemoveHandler("/frz");
    }

    private void OnFreezeGame(string command, string argument) {
        if (!float.TryParse(argument, out var time)) {
            time = 0.5f;
        }

        Thread.Sleep((int)(Math.Min(time, 60) * 1000));
    }
}
