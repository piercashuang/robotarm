using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SQLite4Unity3d;
using Unity.VisualScripting;
using UnityEngine;

public struct SingleDataBase
{
    public SQLiteConnection connection; //连接数据库
    public DataBaseType dbType;
}
/// <summary>
/// 可以用来进行保存
/// </summary>
public interface ICanSave
{
    public void Save();

}


public interface ICanGetEntity<T>
{
    public T GetEntity();

}

/// <summary>
/// 数据库管理器
/// </summary>
public static class DBManager
{
    private static Dictionary<DataBaseType, SingleDataBase> dataBases;

    static DBManager()
    {
        if (dataBases == null) Init();
    }

    public static void Init()
    {
        dataBases = new Dictionary<DataBaseType, SingleDataBase>();

        InitSingleDb("Save.db");

        CheckGameTable();
    }

    /// <summary>
    /// 创建新的单个数据库
    /// </summary>
    private static void InitSingleDb(string dataBaseName)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(Application.streamingAssetsPath);
        string dbDir = dirInfo + "/Data/";
        string dbName = dataBaseName;
#if UNITY_EDITOR

#endif
        CheckGameDataBase(dbDir, dbName);
        SingleDataBase dataBase = new SingleDataBase();
        dataBase.connection = new SQLiteConnection(dbDir + dbName
            , SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        dataBase.dbType = Enum.Parse<DataBaseType>(dataBaseName.Replace(".db", ""));
        dataBases.Add(dataBase.dbType, dataBase);
    }

    public static void Dispose()
    {
        foreach (var VARIABLE in dataBases.Keys)
        {
            dataBases[VARIABLE].connection.Close();
        }
    }

    /// <summary>
    /// 检查表
    /// </summary>
    private static void CheckGameTable()
    {
     
    }

    /// <summary>
    /// 检查是否缺少数据库
    /// </summary>
    private static void CheckGameDataBase(string dbDir, string dbName)
    {
        if (!Directory.Exists(dbDir)) Directory.CreateDirectory(dbDir);
        if (!File.Exists(dbDir + dbName))
        {
            var ffsteam = File.Create(dbDir + dbName);
            ffsteam.Close();
        }
    }

    private static bool CheckTable<T>(DataBaseType dataBaseType = DataBaseType.Save) where T : new()
    {
        try
        {
            var obj = dataBases[dataBaseType].connection.Find<T>(0);
            return true;
        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            Debug.LogError(typeof(T).ToString() + "表格不存在");
#endif
            return false;
        }
    }

//————————————————————————————————————————暴露常用方法————————————————————————————————————————————————————————
    public static void CreateTable<T>(DataBaseType dataBaseType = DataBaseType.Save) where T : IDataBase, new()
    {
        //为了安全起见，会进行检查是否存在该表，避免被覆盖
        if (CheckTable<T>(dataBaseType)) return;

        var entity = new T();
        var depends = entity.dependEntity();
        //先创建entity的数据库类
        CreateSingleTable<T>(dataBaseType);
        //再创建entity依赖的数据库类
        if (depends == null) return;
        if (depends.Length == 0) return;
        for (int i = 0; i < depends.Length; i++)
        {
            var TDepend = depends[i]; //获取依赖

            //递归 再次调用此方法，直到depends中不存在任何依赖数据库
            MethodInfo method = typeof(DBManager).GetMethod(
                "CreateTable", BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(new Type[] { TDepend });
            method.Invoke(null, new object[]{dataBaseType});
        }
    }

    public static void DropTable<T>(DataBaseType dataBaseType = DataBaseType.Save) where T : IDataBase, new()
    {
        var entity = new T();
        var depends = entity.dependEntity();
        //先创建entity的数据库类
        DropSingleTable<T>(dataBaseType);
        //再创建entity依赖的数据库类
        if (depends == null) return;
        if (depends.Length == 0) return;
        for (int i = 0; i < depends.Length; i++)
        {
            var TDepend = depends[i]; //获取依赖
            //递归 再次调用此方法，直到depends中不存在任何依赖数据库
            MethodInfo method = typeof(DBManager).GetMethod(
                "DropTable", BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(new Type[] { TDepend });
            method.Invoke(null, new object[]{dataBaseType});
        }
    }

    public static bool InsertEntity<T>(int ID, DataBaseType dataBaseType = DataBaseType.Save) where T : IDataBase, new()
    {
        //检查是否有该ID
        try
        {
            var entity = new T();
            var depends = entity.dependEntity();
            var defaultEntity = entity.GetDefaultEntity(ID);
            //先创建entity的数据库类
            InsertSingleEntity(defaultEntity, dataBaseType);
            //再创建entity依赖的数据库类
            if (depends == null) return true;
            if (depends.Length == 0) return true;
            for (int i = 0; i < depends.Length; i++)
            {
                var TDepend = depends[i]; //获取依赖
                //递归 再次调用此方法，直到depends中不存在任何依赖数据库
                MethodInfo method = typeof(DBManager).GetMethod(
                        "InsertEntity", BindingFlags.Static | BindingFlags.Public)
                    .MakeGenericMethod(new Type[] { TDepend });
                method.Invoke(null, new object[] { ID,dataBaseType });
            }

            return true;
        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            Debug.LogError(typeof(T).ToString() + ":" + ID + "重复");
            Debug.LogError(e);
#endif
            return false;
        }
    }

    public static bool DropEntity<T>(int ID, DataBaseType dataBaseType = DataBaseType.Save) where T : IDataBase, new()
    {
        try
        {
            var entity = new T();
            var depends = entity.dependEntity();
            //先创建entity的数据库类
            DropSingleEntity<T>(ID, dataBaseType);
            //再创建entity依赖的数据库类
            if (depends == null) return true;
            if (depends.Length == 0) return true;
            for (int i = 0; i < depends.Length; i++)
            {
                var TDepend = depends[i]; //获取依赖
                //递归 再次调用此方法，直到depends中不存在任何依赖数据库
                MethodInfo method = typeof(DBManager).GetMethod(
                        "DropEntity", BindingFlags.Static | BindingFlags.Public)
                    .MakeGenericMethod(new Type[] { TDepend }); 
                method.Invoke(null, new object[] { ID,dataBaseType });
            }

            return true;
        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            Debug.Log(typeof(T).ToString() + ":" + ID + "不存在");
#endif
            return false;
        }
    }

    public static T GetEntity<T>(int id, DataBaseType dataBaseType = DataBaseType.Save) where T : IDataBase, new()
    {
        var entity = dataBases[dataBaseType].connection.Find<T>(id);
        return entity;
    }

    public static T GetEntity<T>(Func<T, bool> condition, DataBaseType dataBaseType = DataBaseType.Save)
        where T : IDataBase, new()
    {
        var entity = dataBases[dataBaseType].connection.Find<T>(condition);
        return entity;
    }

    public static List<T> GetAllEntity<T>(Func<T, bool> condition = null, DataBaseType dataBaseType = DataBaseType.Save)
        where T : IDataBase, new()
    {
        List<T> entities=new List<T>();
            entities = dataBases[dataBaseType].connection.CreateCommand($"select * from {typeof(T).ToString()}")
                .ExecuteQuery<T>();
            if (condition != null)
                entities = entities.Where(condition).ToList();
        return entities;
    }

    public static bool UpdateEntity<T>(T newEntity, DataBaseType dataBaseType = DataBaseType.Save) where T : IDataBase
    {
        try
        {
            dataBases[dataBaseType].connection.Update(newEntity);
            return true;
        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            Console.WriteLine(e);
#endif
            return false;
        }
    }

//————————————————————————————————————————暴露常用方法————————————————————————————————————————————————————————
    private static void CreateSingleTable<T>(DataBaseType dataBaseType = DataBaseType.Save)
    {
        dataBases[dataBaseType].connection.CreateTable<T>();
    }

    private static void DropSingleTable<T>(DataBaseType dataBaseType = DataBaseType.Save)
    {
        dataBases[dataBaseType].connection.DropTable<T>();
    }

    private static void InsertSingleEntity<T>(T entity, DataBaseType dataBaseType = DataBaseType.Save)
    {
        dataBases[dataBaseType].connection.Insert(entity);
    }

    private static void DropSingleEntity<T>(int ID, DataBaseType dataBaseType = DataBaseType.Save) where T : new()
    {
        try
        {
            var entity = dataBases[dataBaseType].connection.Find<T>(ID);
            if (entity != null)
                dataBases[dataBaseType].connection.Delete(entity);
        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            Debug.Log("删除表格" + typeof(T).ToString() + "失败");
            Debug.LogError(e);
#endif
        }
    }
}

public enum DataBaseType
{
    Save
}