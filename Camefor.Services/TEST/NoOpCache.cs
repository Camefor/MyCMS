using System;

namespace Camefor.Services.TEST {
    public class NoOpCache : ICache {
        public object Get(string key) {
            Console.WriteLine("查询缓存:" + key);
            return null;
        }
        public void Put(string key, object value) {
            Console.WriteLine("插入缓存:" + key);
        }
    }
}
