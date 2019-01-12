using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMe.Services
{
    public interface IDeviceInteraction
    {
        void MinimizeMe();
        void ShowKeyboard(Guid controlId);
        void HideKeyboard(Guid controlId);
        void RunOnMainThread(Action runMe);

        // https://medium.com/@dev.aritradas/xamarin-forms-speech-recognition-c16f07cdf164
        bool StartVoiceRecognition();
    }

 
}
