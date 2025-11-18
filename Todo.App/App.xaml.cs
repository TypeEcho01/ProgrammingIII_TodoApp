using System.Configuration;
using System.Data;
using System.Windows;
using Todo.App.ViewModels;

using Todo.Common;

using Task = Todo.Common.Task;

namespace Todo.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static readonly TaskViewModel Task = new TaskViewModel
    (
        name: "My Task", 
        description: "My description.", 
        dueDate: DateTime.UtcNow.AddDays(1)
    );

    public App()
    {

    }
}

