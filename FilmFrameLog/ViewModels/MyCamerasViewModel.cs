using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using FilmFrameLog.Data;
using FilmFrameLog.Interfaces;
using FilmFrameLog.Messages;
using FilmFrameLog.Models;
using FilmFrameLog.Pages;
using FilmFrameLog.Services;
using Google.Api.Gax;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace FilmFrameLog.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


[INotifyPropertyChanged]
public partial class MyCamerasViewModel
{
    FilmFrameLogDatabase database;
    
    IFireStoreService firestoreService;
    FilmFrameLogDatabase database;
    
    public ICommand AddCameraCommand { set; get; }
    public ICommand DeleteCommand { set; get; }
    
    [RelayCommand]
    public async Task ReloadMyCameras()
    {
        LoadCameras();
    }

    // [RelayCommand]
    // public async Task AddCamera()
    // {
    //     var nav = new NavigationPage(new AddCameraPage(new AddCameraPageViewModel()));
    //     nav.Title = "Add Camera";
    //
    //     await Shell.Current.Navigation.PushModalAsync(nav, true);
    //     
    //     // var sample = new SampleModel
    //     // {
    //     //     Name = "Sample",
    //     //     Description = "This is a sample",
    //     //     CreatedAt = DateTime.Now
    //     // };
    //     // await firestoreService.InsertSampleModel(sample);
    // }

    public MyCamerasViewModel(IFireStoreService firestoreService)
    {
        this.firestoreService = firestoreService ?? throw new ArgumentNullException(nameof(firestoreService));


        // this.firestoreService.InsertTestCameras();
        
        WeakReferenceMessenger.Default
            .Register<NewCameraAddedMessage>(
                this,
                async (recipient, message) =>
                {
                    LoadCameras();
                });
        
        
        database = new FilmFrameLogDatabase();
        // Load the cameras
        LoadCameras();
        
        AddCameraCommand = new RelayCommand(async () =>
        {
            var nav = new NavigationPage(new AddCameraPage(new AddCameraPageViewModel(new FirestoreService())));
            nav.Title = "Add Camera";

            await Shell.Current.Navigation.PushModalAsync(nav, true);
        });
        
        DeleteCommand = new RelayCommand<MyCamera>((camera) =>
        {
            DeleteCamera(camera);
        });
    }

    private void DeleteCamera(MyCamera? camera)
    {
        if (camera != null)
        {
            database.DeleteItemAsync(camera);
            LoadCameras();
        }
    }
    
    public async Task<FirestoreDb> initFirestore()
    {
        try
        {
            var stream = await FileSystem.OpenAppPackageFileAsync(Constants.FirestoreKeyFilename);
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            FirestoreClientBuilder fbc = new FirestoreClientBuilder { JsonCredentials = contents };
            return FirestoreDb.Create(Constants.FirestoreProjectID, fbc.Build());
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async void AddCamera()
    {
        var camera = new MyCamera(){ Make = "Olympus", Model = "OM-1", ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg", Format = "35mm", Icon = "olympusomone.png", Notes = "This is a note about the camera.", SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36" };
        await database.SaveItemAsync(camera);

        LoadCameras();
    }

    [ObservableProperty] 
    string filter;
    
    
    public ObservableCollection<MyCamera> Cameras { get; set; } = new ObservableCollection<MyCamera>();


    private async Task LoadCameras()
    {
        

        
        Cameras.Clear();
        var items = await database.GetItemsAsync();
        foreach (var item in items)
        {
            Cameras.Add(item);
        }



        /*
        Cameras.Clear();
        //var monkeys = new List<Monkey>();
        Cameras.Add(new Camera(){ Make = "Olympus", Model = "OM-1", ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg", Format = "35mm", Icon = "olympusomone.png", Notes = "This is a note about the camera.", SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36" });
        Cameras.Add(new Camera(){ Make = "Olympus", Model = "OM-10", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "35mm", Icon = "olympusomten.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        Cameras.Add(new Camera(){ Make = "Rollei", Model = "Rolleiflex 2.8 E3", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "Medium", Icon = "rolleirolleflexethree.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });

        Cameras.Add(new Camera(){ Make = "Olympus", Model = "OM-1", ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg", Format = "35mm", Icon = "olympusomone.png", Notes = "This is a note about the camera.", SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36" });
        Cameras.Add(new Camera(){ Make = "Olympus", Model = "OM-10", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "35mm", Icon = "olympusomten.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        Cameras.Add(new Camera(){ Make = "Rollei", Model = "Rolleiflex 2.8 E3", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "Medium", Icon = "rolleirolleflexethree.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        Cameras.Add(new Camera(){ Make = "Olympus", Model = "OM-1", ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg", Format = "35mm", Icon = "olympusomone.png", Notes = "This is a note about the camera.", SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36" });
        Cameras.Add(new Camera(){ Make = "Olympus", Model = "OM-10", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "35mm", Icon = "olympusomten.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        Cameras.Add(new Camera(){ Make = "Rollei", Model = "Rolleiflex 2.8 E3", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "Medium", Icon = "rolleirolleflexethree.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        Cameras.Add(new Camera(){ Make = "Olympus", Model = "OM-1", ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg", Format = "35mm", Icon = "olympusomone.png", Notes = "This is a note about the camera.", SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36" });
        Cameras.Add(new Camera(){ Make = "Olympus", Model = "OM-10", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "35mm", Icon = "olympusomten.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        Cameras.Add(new Camera(){ Make = "Rollei", Model = "Rolleiflex 2.8 E3", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "Medium", Icon = "rolleirolleflexethree.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        Cameras.Add(new Camera(){ Make = "Olympus", Model = "OM-1", ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg", Format = "35mm", Icon = "olympusomone.png", Notes = "This is a note about the camera.", SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36" });
        Cameras.Add(new Camera(){ Make = "Olympus", Model = "OM-10", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "35mm", Icon = "olympusomten.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        Cameras.Add(new Camera(){ Make = "Rollei", Model = "Rolleiflex 2.8 E3", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "Medium", Icon = "rolleirolleflexethree.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        Cameras.Add(new Camera(){ Make = "Olympus", Model = "OM-1", ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg", Format = "35mm", Icon = "olympusomone.png", Notes = "This is a note about the camera.", SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36" });
        Cameras.Add(new Camera(){ Make = "Olympus", Model = "OM-10", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "35mm", Icon = "olympusomten.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        Cameras.Add(new Camera(){ Make = "Rollei", Model = "Rolleiflex 2.8 E3", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "Medium", Icon = "rolleirolleflexethree.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
*/

    }
}