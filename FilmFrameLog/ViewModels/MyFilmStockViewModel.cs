using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmFrameLog.Data;
using FilmFrameLog.Models;
using FilmFrameLog.Pages;
using FilmFrameLog.Services;
using Google.Cloud.Firestore.V1;

namespace FilmFrameLog.ViewModels;

[INotifyPropertyChanged]
public partial class MyFilmStockViewModel
{
    FilmFrameLogDatabase database;
    public ICommand MyCommand { set; get; }
    public  MyFilmStockViewModel()
    {
        database = new FilmFrameLogDatabase();
        
        // Load the films
        LoadFilms();
        
        MyCommand = new RelayCommand(() =>
        {
           AddFilm();
           
        });
    }

    private async void AddFilm()
    {
        var nav = new NavigationPage(new AddFilmPage(new AddFilmViewModel(new FirestoreService())));
        nav.Title = "Add Film";

        await Shell.Current.Navigation.PushModalAsync(nav, true);

        //LoadFilms();

        /*
        var film = new Film()
        {
            Make = "Ilford", Model = "HP5", ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg",
            Format = "35mm", Icon = "ilfordhp5400.png", Notes = "This is a note about the camera.",
            SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36"
        };
        await database.SaveFilmAsync(film);

        LoadFilms();*/
    }

    [ObservableProperty] 
    string filter;
    
    
    public ObservableCollection<MyFilm> Films { get; set; } = new ObservableCollection<MyFilm>();


    private async Task LoadFilms()
    {
        Films.Clear();
        var items = await database.GetFilmsAsync();
        foreach (var item in items)
        {
            Films.Add(item);
        }
        
        
        /*
        Films.Clear();
        //var monkeys = new List<Monkey>();
        Films.Add(new Film(){ Make = "Ilford", Model = "HP5", ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg", Format = "35mm", Icon = "ilfordhp5400.png", Notes = "This is a note about the camera.", SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36" });
        Films.Add(new Film(){ Make = "Kodak", Model = "Gold", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "35mm", Icon = "kodakgold200.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        */
    }
}