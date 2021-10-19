using System.Collections.Generic;

public interface IAnalyticTools 
{
    void SendMessage(string alias, IDictionary<string, object> eventData = null);
}
