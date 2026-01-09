
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CManager.Presentation.GuiApp.Models;

public class ActivityItem :INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public string Activity { get; set; } = null!;

    public bool _isCompleted;
    public bool IsCompleted
    {
        get => _isCompleted;
        set 
        {
            if (_isCompleted != value)
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));

            }
        }
    }
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
