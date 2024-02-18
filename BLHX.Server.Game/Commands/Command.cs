﻿using BLHX.Server.Common.Utils;
using System.Reflection;

namespace BLHX.Server.Game.Commands;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class commandHandler : Attribute
{
    public string Name { get; }
    public string Description { get; }
    public string Example { get; }

    public commandHandler(string commandName, string description, string example)
    {
        Name = commandName;
        Description = description;
        Example = example;
    }
}

[AttributeUsage(AttributeTargets.Property)]
public class ArgumentAttribute : Attribute
{
    public string Key { get; }

    public ArgumentAttribute(string key)
    {
        Key = key;
    }
}

public abstract class Command
{
    readonly Dictionary<string, PropertyInfo> argsProperties;

    public Command()
    {
        argsProperties = GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => Attribute.IsDefined(x, typeof(ArgumentAttribute)))
            .ToDictionary(x => ((ArgumentAttribute)Attribute.GetCustomAttribute(x, typeof(ArgumentAttribute))!).Key, StringComparer.OrdinalIgnoreCase);
    }

    public virtual void Execute(Dictionary<string, string> args)
    {
        foreach (var arg in args)
        {
            if (argsProperties.TryGetValue(arg.Key, out var prop))
                prop.SetValue(this, arg.Value);
        }
    }
}

public static class CommandHandler
{
    public static readonly List<Command> Commands = new List<Command>();
    
    static readonly Dictionary<string, Action<Dictionary<string, string>>> commandFunctions;

    static CommandHandler()
    {
        commandFunctions = new Dictionary<string, Action<Dictionary<string, string>>>(StringComparer.OrdinalIgnoreCase);

        RegisterCommands(Assembly.GetExecutingAssembly());
    }

    public static void RegisterCommands(Assembly assembly)
    {
        var commandTypes = assembly.GetTypes()
            .Where(t => Attribute.IsDefined(t, typeof(commandHandler)) && typeof(Command).IsAssignableFrom(t));

        foreach (var commandType in commandTypes)
        {
            var commandAttribute = (commandHandler)Attribute.GetCustomAttribute(commandType, typeof(commandHandler));
            if (commandAttribute != null)
            {
                var commandInstance = (Command)Activator.CreateInstance(commandType);
                commandFunctions[commandAttribute.Name] = commandInstance.Execute;
                Commands.Add(commandInstance);
            }
        }
    }

    public static void HandleCommand(string commandLine)
    {
        var parts = commandLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return;

        if (!commandFunctions.TryGetValue(parts[0], out var command))
        {
            Logger.c.Warn($"Unknown command: {parts[0]}");
            return;
        }

        var arguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        for (var i = 1; i < parts.Length; i++)
        {
            var argParts = parts[i].Split('=', 2);
            if (argParts.Length == 2)
                arguments[argParts[0]] = argParts[1];
        }

        command(arguments);
    }
}