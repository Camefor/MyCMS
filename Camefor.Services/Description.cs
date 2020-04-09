/// <summary>
/// Services 业务逻辑层，就是和我们平时使用的三层架构中的BLL层很相似
/// Service层只负责将Repository仓储层的数据进行调用，至于如何是与数据库交互的，它不去管，这样就可以达到一定程度上的解耦，
/// 假如以后数据库要换，比如MySql，那Service层就完全不需要修改即可，至于真正意义的解耦，还是得靠依赖注入
/// </summary>
namespace Camefor.Services {
    public class Description {
    }
}
