// using SQLite4Unity3d;

// public class tablem2i2 : IDataBase
// {
//     [PrimaryKey] public int ID { get; set; }
//     public int Value { get; set; }
//     public string Description { get; set; }

//     public Type[] dependEntity()
//     {
//         return null;
//     }

//     public IDataBase GetDefaultEntity(int id)
//     {
//         return new tablem2i2() { ID = id };
//     }
// }

// // 如果你想在类中进行初始化
// public class DatabaseInitializer
// {
//     public static void Initialize()
//     {
//         DBManager.Init(); // 初始化数据库管理器，确保数据库连接已经建立

//         // 创建名为 Save.db 的数据库文件，并连接到该文件
//         DBManager.InitSingleDb("Save.db");

//         // 创建 tablem2i2 表格
//         DBManager.CreateTable<tablem2i2>(DataBaseType.Save);
//     }
// }

// // 如果你想在顶级代码中进行初始化
// public static class Program
// {
//     public static void Main(string[] args)
//     {
//         DBManager.Init(); // 初始化数据库管理器，确保数据库连接已经建立

//         // 创建名为 Save.db 的数据库文件，并连接到该文件
//         DBManager.InitSingleDb("Save.db");

//         // 创建 tablem2i2 表格
//         DBManager.CreateTable<tablem2i2>(DataBaseType.Save);
//     }
// }
