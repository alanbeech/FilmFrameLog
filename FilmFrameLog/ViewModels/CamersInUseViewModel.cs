using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmFrameLog.Models;

namespace FilmFrameLog.ViewModels;

[INotifyPropertyChanged]
public partial class CamerasInUseViewModel
{
    public ICommand MyCommand { set; get; }

    public CamerasInUseViewModel()
    {
        MyCommand = new RelayCommand(() =>
        {
            LoadCameras();
        });
        
        // Load the cameras
        
    }

    [ObservableProperty] 
    string filter;
    
    
    
    public ObservableCollection<CameraInUse> CamerasInUse { get; set; } = new ObservableCollection<CameraInUse>();


    private async Task LoadCameras()
    {
        CamerasInUse.Clear();
        //var monkeys = new List<Monkey>();
        CamerasInUse.Add(new CameraInUse(){ Make = "Olympus", Model = "OM-1", ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg", Format = "35mm", Icon = "olympusomone.png", Notes = "This is a note about the camera.", SerialNumber = "1234", FilmType = "Kodak Gold - 200", FramesUsed = "12 of 36" });
        CamerasInUse.Add(new CameraInUse(){ Make = "Olympus", Model = "OM-10", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "35mm", Icon = "olympusomten.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        CamerasInUse.Add(new CameraInUse(){ Make = "Rollei", Model = "Rolleiflex 2.8 E3", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Olympus_OM-1.jpg/600px-Olympus_OM-1.jpg", Format = "Medium", Icon = "rolleirolleflexethree.png", Notes = "This is a note about the camera.", SerialNumber = "2343", FilmType = "Ilford HP5 - 400", FramesUsed = "24 of 36" });
        
    }
}