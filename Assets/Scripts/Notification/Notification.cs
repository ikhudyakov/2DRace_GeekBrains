using System;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.UI;

public class Notification 
{
    private const string AndroidNotifierId = "android_notifier_id";

    public static void CreateNotifications(string name, string description)
    {
        if(Application.platform == RuntimePlatform.Android)
            CreateNotificationAndroid(name, description);
        if(Application.platform == RuntimePlatform.IPhonePlayer)
            CreateNotificationIOS();
    }
    private static void CreateNotificationAndroid(string name, string description)
    {
        var androidSettingsChanel = new AndroidNotificationChannel
        {
            Id = AndroidNotifierId,
            Name = name,
            Importance = Importance.High,
            CanBypassDnd = true,
            CanShowBadge = true,
            Description = description,
            EnableLights = true,
            EnableVibration = true,
            LockScreenVisibility = LockScreenVisibility.Public
        };

        AndroidNotificationCenter.RegisterNotificationChannel(androidSettingsChanel);

        var androidSettingsNotification = new AndroidNotification
        {
            Color = Color.black,
            FireTime = DateTime.Now.AddDays(1),
            Text = androidSettingsChanel.Description,
            Title = androidSettingsChanel.Name
        };

        AndroidNotificationCenter.SendNotification(androidSettingsNotification, AndroidNotifierId);
    }

    public static void CancelNotoficationAndroid()
    {
        AndroidNotificationCenter.CancelAllNotifications();
    }

    private static void CreateNotificationIOS()
    {
        //var iosSettingsNotification = new iOSNotification
        //{
        //    Identifier = "android_notifier_id",
        //    Title = "Game Notifier",
        //    Subtitle = "Subtitle notifier",
        //    Body = "Enter the game and get free crystals",
        //    Badge = 1,
        //    Data = "01/02/2021",
        //    ForegroundPresentationOption = PresentationOption.Alert,
        //    ShowInForeground = false
        //};

        //iOSNotificationCenter.ScheduleNotification(iosSettingsNotification);
    }

}
