using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmFrameLog.ViewModels;

namespace FilmFrameLog.Pages;

public partial class AddFilmPage : ContentPage
{
    public AddFilmPage()
    {
        InitializeComponent();
        BindingContext = new AddFilmViewModel();
    }
}