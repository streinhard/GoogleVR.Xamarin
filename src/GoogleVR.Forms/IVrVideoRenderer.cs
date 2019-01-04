using System;

namespace GoogleVR.Forms
{
    internal interface IVrVideoRenderer
    {
        void PlayVideo();
        void PauseVideo();
        void SeekTo(double position);
    }
}
