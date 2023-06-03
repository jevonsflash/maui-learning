using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using Lession2.TouchRecognizer;

namespace Lession2;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private void TouchContentView_OnTouchActionInvoked(object sender, TouchActionEventArgs e)
    {
        Debug.WriteLine("OnTouchAction as been invoke!");
    }
}

