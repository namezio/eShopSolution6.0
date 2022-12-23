using Newtonsoft.Json;

namespace eShop.WebApp.Repository;

public interface ISessionManager
{
    T GetValue<T>(string key);
    void SetValue<T>(string key, T value);
    void Remove(string key);
}
public class SessionManager : ISessionManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public T GetValue<T>(string key)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        string json = session.GetString(key);
        if (string.IsNullOrEmpty(json))
            return default(T);

        return JsonConvert.DeserializeObject<T>(json);
    }

    public void SetValue<T>(string key, T value)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        string json = JsonConvert.SerializeObject(value);
        session.SetString(key, json);
    }

    public void Remove(string key)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        session.Remove(key);
    }
}
