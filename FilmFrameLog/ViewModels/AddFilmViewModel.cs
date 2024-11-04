using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmFrameLog.Data;
using FilmFrameLog.Interfaces;
using FilmFrameLog.Models;

namespace FilmFrameLog.ViewModels;

[INotifyPropertyChanged]
public partial class AddFilmViewModel
{
    public ICommand SaveFilmCommand { set; get; }
    
    IFireStoreService firestoreService;
    
    FilmFrameLogDatabase database;
    
    public ObservableCollection<Film> AvailableFilms { get; set; } = new ObservableCollection<Film>();
    
    [RelayCommand]
    public async Task Close()
    {
        await Shell.Current.Navigation.PopAsync(true);
    }
    
    [ObservableProperty] private string _name;

    public AddFilmViewModel(IFireStoreService firestoreService)
    {
        this.firestoreService = firestoreService;

       // firestoreService.InsertTestFilm();
        
        database = new FilmFrameLogDatabase();
        
        Name = "Ilford HP5";
        
        LoadFilms();
        
        SaveFilmCommand = new RelayCommand(() =>
        {
            SaveFilm();
        });
    }

    private async void SaveFilm()
    {
        var film = new MyFilm()
        {
            Make = Name, Model = "*", ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg",
            Format = "35mm", Icon = "ilfordhp5400.png", Notes = "This is a note about the camera.",
            SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36"
        };
        await database.SaveFilmAsync(film);
        
        await Shell.Current.Navigation.PopAsync(true);
    }
    
    private async Task LoadFilms()
    {
        // load the cameras from firestore
        var cams = await firestoreService.GetAvailableFilms();
        foreach (var cam in cams)
        {
            AvailableFilms.Add(cam);
        }
        
    }

  
}