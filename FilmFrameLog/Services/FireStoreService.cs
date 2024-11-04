using FilmFrameLog.Interfaces;
using FilmFrameLog.Models;
using Google.Cloud.Firestore;

namespace FilmFrameLog.Services
{
    public class FirestoreService : IFireStoreService
    {
       
        private FirestoreDb db;
        private async Task SetupFirestore()
        {
            if (db == null)
            {
                var stream = await FileSystem.OpenAppPackageFileAsync("GoogleAppCredentials.json");
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();
    
                db = new FirestoreDbBuilder
                {
                    ProjectId = "filmframelog",
    
                    ConverterRegistry = new ConverterRegistry
                    {
                        new DateTimeToTimestampConverter(),
                    },
                    JsonCredentials = contents
                }.Build();
            }
        }

        public async Task InsertTestCameras()
        {
            await SetupFirestore();
            await db.Collection(nameof(Camera)).AddAsync(new Camera
            {
                Make = "Olympus",
                Model = "OM-1",
                Icon = "om1.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg",
                Format = "35mm"
            });
            
            await db.Collection(nameof(Camera)).AddAsync(new Camera
            {
                Make = "Nikon",
                Model = "F3",
                Icon = "nikonf3.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg",
                Format = "35mm"
            });
            
            await db.Collection(nameof(Camera)).AddAsync(new Camera
            {
                Make = "Canon",
                Model = "AE-1",
                Icon = "canonae1.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg",
                Format = "35mm"
            });
            
            await db.Collection(nameof(Camera)).AddAsync(new Camera
            {
                Make = "Minolta",
                Model = "X-700",
                Icon = "minoltax700.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg",
                Format = "35mm"
            });
            
            await db.Collection(nameof(Camera)).AddAsync(new Camera
            {
                Make = "Pentax",
                Model = "K1000",
                Icon = "pentaxk1000.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg",
                Format = "35mm"
            });
            
            await db.Collection(nameof(Camera)).AddAsync(new Camera
            {
                Make = "Leica",
                Model = "M6",
                Icon = "leicam6.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg",
                Format = "35mm"
            });
            
            await db.Collection(nameof(Camera)).AddAsync(new Camera
            {
                Make = "Hasselblad",
                Model = "500C/M",
                Icon = "hasselblad500cm.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg",
                Format = "Medium Format"
            });
            
            await db.Collection(nameof(Camera)).AddAsync(new Camera
            {
                Make = "Mamiya",
                Model = "RB67",
                Icon = "mamiyarb67.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://filmadvance.com/wp-content/uploads/2011/09/OM10-1.jpg",
                Format = "Medium Format"
            });
        }

        public async Task InsertTestFilm()
        {
            await SetupFirestore();
            await db.Collection(nameof(Film)).AddAsync(new Film
            {
                Make = "Ilford",
                Name = "HP5",
                Icon = "ilfordhp5400.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://www.ilfordphoto.com/media/catalog/product/cache/8053a0603c326d24b9f382c3787f74d9/h/p/hp5_135_36_c_b.jpg",
                Format = "35mm",
                Speed = "400"
            });
            
            await db.Collection(nameof(Film)).AddAsync(new Film
            {
                Make = "Kodak",
                Name = "Gold 200",
                Icon = "kodakgold200.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://analoguewonderland.co.uk/cdn/shop/products/kodak-gold-200-35mm-film-at-analogue-wonderland-598037.jpg?v=1718029885&width=1200",
                Format = "35mm",
                Speed = "200"
            });
            
            await db.Collection(nameof(Film)).AddAsync(new Film
            {
                Make = "Ilford",
                Name = "FP4",
                Icon = "ilfordfp4.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://www.bristolcameras.co.uk/content/uploads/2021/05/ilford-fp4-plus-36-exposure-35mm-film-lrg.jpg",
                Format = "35mm",
                Speed = "125"
                
            });
            
            await db.Collection(nameof(Film)).AddAsync(new Film
            {
                Make = "Kodak",
                Name = "Tri-X",
                Icon = "kodaktrix.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://harrison-cameras.s3.amazonaws.com/p/l/504425/504425_1.jpg",
                Format = "35mm",
                Speed = "400"
            });
            
            await db.Collection(nameof(Film)).AddAsync(new Film
            {
                Make = "Ilford",
                Name = "Delta 3200",
                Icon = "ilforddelta3200.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://analoguewonderland.co.uk/cdn/shop/products/ilford-delta-3200-35mm-film-at-analogue-wonderland-212164.jpg?v=1675785260&width=1200",
                Format = "35mm",
                Speed = "3200"
            });
            
            await db.Collection(nameof(Film)).AddAsync(new Film
            {
                Make = "Kodak",
                Name = "Portra 400",
                Icon = "kodakportra400.png",
                Notes = "This is a note about the camera.",
                ImageUrl = "https://analoguewonderland.co.uk/cdn/shop/products/kodak-portra-800-35mm-colour-film-258200.jpg?v=1614963138&width=900",
                Format = "35mm",
                Speed = "400"
            });
        }

        public async Task<List<Camera>> GetAvailableCameras()
        {
            await SetupFirestore();
            
            var data = await db.Collection(nameof(Camera)).GetSnapshotAsync();
            var cameras = data.Documents
                .Select(doc =>
                {
                    var cameraModel = doc.ConvertTo<Camera>();
                    cameraModel.Id = doc.Id; // FirebaseId hinzufügen
                    return cameraModel;
                })
                .ToList();

            return cameras;
        }

        public async Task<IEnumerable<Film>> GetAvailableFilms()
        {
            await SetupFirestore();
            
            var data = await db.Collection(nameof(Film)).GetSnapshotAsync();
            var films = data.Documents
                .Select(doc =>
                {
                    var cameraModel = doc.ConvertTo<Film>();
                    cameraModel.Id = doc.Id; // FirebaseId hinzufügen
                    return cameraModel;
                })
                .ToList();

            return films;
        }

        public async Task InsertSampleModel(SampleModel sample)
        {
            await SetupFirestore();
            await db.Collection("SampleModels").AddAsync(sample);
        }
        public async Task<List<SampleModel>> GetSampleModels()
        {
            await SetupFirestore();
            var data = await db
                .Collection("SampleModels")
                .GetSnapshotAsync();
            var sampleModels = data.Documents
                .Select(doc =>
                {
                    var sampleModel = doc.ConvertTo<SampleModel>();
                    sampleModel.Id = doc.Id; // FirebaseId hinzufügen
                    return sampleModel;
                })
                .ToList();
            return sampleModels;
        }
    
    }

}

