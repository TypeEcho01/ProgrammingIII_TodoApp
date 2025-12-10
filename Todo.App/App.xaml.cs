using System.Configuration;
using System.Data;
using System.Windows;
using Todo.App.Services;
using Todo.App.ViewModels;

using Todo.Common;

using Task = Todo.Common.Task;

namespace Todo.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static readonly IInputDialogService InputDialogService = new InputDialogService();

    public static readonly IMessageDialogService MessageDialogService = new MessageDialogService();

    public static readonly IDateTimeDialogService DateTimeDialogService = new DateTimeDialogService();

    public static JsonRepository<Task> Repository = new JsonRepository<Task>("../../../tasks.json");

    public App()
    {
        
    }

    public static Result<string> Input(string title, string prompt, string defaultText = "") =>
        App.InputDialogService.Input(title, prompt, defaultText);

    public static void Message(string title, string message) =>
        App.MessageDialogService.Message(title, message);

    public static void Error(string message) =>
        App.MessageDialogService.Error(message);

    public static Result<DateTime> DateTimeInput(string title, DateTime? defaultDateTime = null) =>
        App.DateTimeDialogService.DateTimeInput(title, defaultDateTime);
}

