using System;

namespace Tools.Ads
{
    public interface IAdsShower
    {
        void ShowInterstitial();
        void ShowVideo(Action onSuccess);
    }
}