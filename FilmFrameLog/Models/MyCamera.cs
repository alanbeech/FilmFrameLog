using SQLite;

namespace FilmFrameLog.Models;

public class MyCamera
{
    [PrimaryKey, AutoIncrement]
    
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Icon { get; set; }
    public string Notes { get; set; }
    public string ImageUrl { get; set; }
    public string Format { get; set; }
    
    public string SerialNumber { get; set; }
    
    public string FramesUsed { get; set; }
    
    public string FilmType { get; set; }
}