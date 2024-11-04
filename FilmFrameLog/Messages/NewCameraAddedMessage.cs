using CommunityToolkit.Mvvm.Messaging.Messages;

namespace FilmFrameLog.Messages;

public class NewCameraAddedMessage : ValueChangedMessage<int>
{
    public NewCameraAddedMessage(int count) : base(count)
    {
    }
}