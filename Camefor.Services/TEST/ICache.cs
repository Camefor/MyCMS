namespace Camefor.Services.TEST {
    public interface ICache {
        object Get(string key);
        void Put(string key, object value);
    }
}
