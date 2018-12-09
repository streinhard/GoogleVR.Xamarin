using System;

namespace GoogleVR.Forms
{
    public interface IVrVideoRenderer
    {
        void PlayVideo();
        void PauseVideo();
        void SeekTo(long position);
    }
}
