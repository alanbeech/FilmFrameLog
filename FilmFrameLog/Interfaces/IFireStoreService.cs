using FilmFrameLog.Models;

namespace FilmFrameLog.Interfaces;

public interface IFireStoreService
{
    Task InsertSampleModel(SampleModel sample);
    
    
    Task<List<SampleModel>> GetSampleModels();

    Task InsertTestCameras();
    
    Task InsertTestFilm();
    
    Task<List<Camera>> GetAvailableCameras();


    Task<IEnumerable<Film>> GetAvailableFilms();
}