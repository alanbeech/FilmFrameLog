using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmFrameLog.ViewModels;

namespace FilmFrameLog.Pages;

public partial class AddCameraPage : ContentPage
{
    public AddCameraPage(AddCameraPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}