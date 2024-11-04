using Google.Cloud.Firestore;

namespace FilmFrameLog.Models;

[FirestoreData]
public class SampleModel
{
    [FirestoreProperty] public string Id { get; set; }
    [FirestoreProperty] public string Name { get; set; }

    [FirestoreProperty] public string Description { get; set; }

    //[FirestoreProperty(ConverterType = typeof(DateTimeToTimestampConverter))]
    [FirestoreProperty] public DateTime CreatedAt { get; set; }
}

public class DateTimeToTimestampConverter : IFirestoreConverter<DateTime>
{
    public object ToFirestore(DateTime value) => Timestamp.FromDateTime(value.ToUniversalTime());

    public DateTime FromFirestore(object value)
    {
        if (value is Timestamp timestamp)
        {
            return timestamp.ToDateTime();
        }

        throw new ArgumentException("Invalid value");
    }
}