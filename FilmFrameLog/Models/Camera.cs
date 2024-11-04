using Google.Cloud.Firestore;

namespace FilmFrameLog.Models;

[FirestoreData]
public class Camera
{
    [FirestoreProperty] 
    public string Id { get; set; }
    
    [FirestoreProperty]
    public string Make { get; set; }
    
    [FirestoreProperty]
    public string Model { get; set; }
    
    [FirestoreProperty]
    public string Icon { get; set; }
    
    [FirestoreProperty]
    public string Notes { get; set; }
    
    [FirestoreProperty]
    public string ImageUrl { get; set; }
    
    [FirestoreProperty]
    public string Format { get; set; }
}