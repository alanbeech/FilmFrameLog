using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using FilmFrameLog.Data;
using FilmFrameLog.Interfaces;
using FilmFrameLog.Messages;
using FilmFrameLog.Models;
using Foundation;

namespace FilmFrameLog.ViewModels;

[CommunityToolkit.Mvvm.ComponentModel.INotifyPropertyChanged]
public partial class AddCameraPageViewModel
{
    // public ICommand AddCameraCommand { set; get; }
    
    IFireStoreService firestoreService;


    [ObservableProperty]  
    public Camera selectedCamera;
    

    public ObservableCollection<Camera> AvailableCameras { get; set; } = new ObservableCollection<Camera>();
    
    FilmFrameLogDatabase database;
    private Camera _selectedCamera;

    [RelayCommand]
    public async Task CameraSelected(Camera aaa)
    {
        SelectedCamera = aaa;
    }

    [RelayCommand]
    public async Task Close()
    {
        await Shell.Current.Navigation.PopAsync(true);
    }
    
    [RelayCommand]
    public async Task AddCamera()
    {
        database = new FilmFrameLogDatabase();
        
        // add the selected camera to the local database
        var camera = new MyCamera(){ Make = SelectedCamera.Make, Model = SelectedCamera.Model, ImageUrl = SelectedCamera.ImageUrl, Format = SelectedCamera.Format, Icon = SelectedCamera.ImageUrl, Notes = "This is a note about the camera.", SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36" };
        await database.SaveItemAsync(camera);
        
        var myCameras = await database.GetItemsAsync();
        
        WeakReferenceMessenger.Default.Send(new NewCameraAddedMessage(1));
        
        await Shell.Current.Navigation.PopAsync(true);
    }
    
    public AddCameraPageViewModel(IFireStoreService firestoreService)
    {
        this.firestoreService = firestoreService ?? throw new ArgumentNullException(nameof(firestoreService));
        this.database = database;

        // Load the cameras from firestore
        LoadCameras();


        // AddCameraCommand = new RelayCommand(async () =>
        // {
        //     await AddCamera();
        // });
    }

    private async Task LoadCameras()
    {
        // load the cameras from firestore
        var cams = await firestoreService.GetAvailableCameras();
        foreach (var cam in cams)
        {
            AvailableCameras.Add(cam);
        }
        
    }
}