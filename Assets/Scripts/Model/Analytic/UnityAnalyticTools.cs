using System.Collections.Generic;

public class UnityAnalyticTools : IAnalyticTools
{
    public void SendMessage(string alias, IDictionary<string, object> eventData)
    {
        if (eventData == null)
            eventData = new Dictionary<string, object>();
        UnityEngine.Analytics.Analytics.CustomEvent(alias, eventData);
    }
}