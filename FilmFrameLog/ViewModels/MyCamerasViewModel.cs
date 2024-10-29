using System.Collections.ObjectModel;
using System.Windows.Input;
using FilmFrameLog.Data;
using FilmFrameLog.Models;

namespace FilmFrameLog.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


[INotifyPropertyChanged]
public partial class MyCamerasViewModel
{
    FilmFrameLogDatabase database;
    
    public ICommand MyCommand { set; get; }
    public ICommand DeleteCommand { set; get; }

    
    public MyCamerasViewModel()
    {
        database = new FilmFrameLogDatabase();
        // Load the cameras
        LoadCameras();
        
        MyCommand = new RelayCommand(() =>
        {
            AddCamera();
        });
        
        DeleteCommand = new RelayCommand<Camera>((camera) =>
        {
            DeleteCamera(camera);
        });
    }

    private void DeleteCamera(Camera? camera)
    {
        if (camera != null)
        {
            database.DeleteItemAsync(camera);
            LoadCameras();
        }
    }

    private async void AddCamera()
    {
        var camera = new Camera(){ Make = "Olympus", Model = "OM-1", ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg", Format = "35mm", Icon = "olympusomone.png", Notes = "This is a note about the camera.", SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36" };
        await database.SaveItemAsync(camera);

        LoadCameras();
    }

    [ObservableProperty] 
    string filter;
    
    
    public ObservableCollection<Camera> Cameras { get; set; } = new ObservableCollection<Camera>();


    private async Task LoadCameras()
    {
        // are there any cameras in the db
 
        
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