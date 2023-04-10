using Avalonia.Controls;
using Avalonia.Controls.Templates;
using AvaloniaApplication8.ViewModels;
using System;
using System.Xml.Linq;

namespace AvaloniaApplication8
{
    public class ViewLocator : IDataTemplate
    {
        public Control Build(object? data)
        {
            if (data != null)
            { 
                var name = data.GetType().FullName!.Replace("ViewModel", "View");
                var type = Type.GetType(name);

                if (type != null)
                {
                    return (Control)Activator.CreateInstance(type)!;
                }

                return new TextBlock { Text = "Not Found: " + name };

            }
            return new TextBlock { Text = "Not Found: " };
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}