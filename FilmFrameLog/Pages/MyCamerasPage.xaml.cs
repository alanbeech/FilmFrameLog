using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmFrameLog.Services;
using FilmFrameLog.ViewModels;

namespace FilmFrameLog.Pages;

public partial class MyCamerasPage : ContentPage
{
    public MyCamerasPage(MyCamerasViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}