using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Darlek.Core;

public static class Repository
{
    public static string BaseDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Darlek");
    public static string CacheFile;

    static Repository()
    {
        Directory.CreateDirectory(BaseDir);
    }

    public static void Clear()
    {
        using var db = new LiteDatabase(CacheFile);

        var collection = db.GetCollection("entries");
        collection.DeleteAll();
    }

    public static BsonDocument GetAllMetadata()
    {
        using var db = new LiteDatabase(CacheFile);

        var storage = db.FileStorage.FindById("metadata");

        return storage.Metadata;
    }

    public static void Init(string title, string author)
    {
        Repository.CacheFile = Path.Combine(Repository.BaseDir, title + ".darlek");
        Repository.SetMetadata("title", title);
        Repository.SetMetadata("author", author);
        Repository.SetMetadata("filename", "exported");
    }

    public static void CollectCustomCommands(Menu menu)
    {
        using var db = new LiteDatabase(CacheFile);

        foreach (var finfo in db.FileStorage.FindAll().Where(_ => _.Filename.StartsWith("commands")))
        {
            var eval = new SchemeEvaluator();
            if (eval != null)
            {
                var ms = new MemoryStream();
                db.FileStorage.Download(finfo.Filename, ms);

                var source = Encoding.Default.GetString(ms.ToArray());
                eval.AddCustomCommands(source, menu);
            }
        }
    }

    public static string GetFile(string name)
    {
        using var db = new LiteDatabase(CacheFile);

        var ms = new MemoryStream();
        db.FileStorage.Download(name, ms);

        return Encoding.Default.GetString(ms.ToArray());
    }

    public static void Add(BsonDocument model)
    {
        using var db = new LiteDatabase(CacheFile);

        var collection = db.GetCollection("entries");
        collection.Insert(model);
    }

    public static string GetMetadata(string key)
    {
        using var db = new LiteDatabase(CacheFile);

        var storage = db.FileStorage.FindById("metadata");
        if (storage.Metadata.ContainsKey(key))
        {
            return storage.Metadata[key];
        }

        return null;
    }

    public static void RemoveSetting(string key)
    {
        using var db = new LiteDatabase(CacheFile);

        if (!db.FileStorage.Exists("settings"))
        {
            db.FileStorage.Upload("settings", "settings", new MemoryStream(Encoding.ASCII.GetBytes("SETTINGS")));
        }

        var storage = db.FileStorage.FindById("settings");
        if (storage.Metadata.ContainsKey(key))
        {
            storage.Metadata.Remove(key);
        }

        db.FileStorage.SetMetadata("settings", storage.Metadata);
    }

    public static string GetSetting(string key)
    {
        using var db = new LiteDatabase(CacheFile);

        if (!db.FileStorage.Exists("settings"))
        {
            return null;
        }

        var storage = db.FileStorage.FindById("settings");
        if (storage.Metadata.ContainsKey(key))
        {
            return storage.Metadata[key];
        }

        return null;
    }

    public static void SetMetadata(string key, string value)
    {
        using var db = new LiteDatabase(CacheFile);

        if (!db.FileStorage.Exists("metadata"))
        {
            db.FileStorage.Upload("metadata", "metadata", new MemoryStream(Encoding.ASCII.GetBytes("METADATA")));
        }

        var storage = db.FileStorage.FindById("metadata");
        if (!storage.Metadata.ContainsKey(key))
        {
            storage.Metadata.Add(key, value);
        }
        else
        {
            storage.Metadata[key] = value;
        }

        db.FileStorage.SetMetadata("metadata", storage.Metadata);
    }

    public static void SetSetting(string key, string value)
    {
        using var db = new LiteDatabase(CacheFile);

        if (!db.FileStorage.Exists("settings"))
        {
            db.FileStorage.Upload("settings", "settings", new MemoryStream(Encoding.ASCII.GetBytes("SETTINGS")));
        }

        var storage = db.FileStorage.FindById("settings");
        if (!storage.Metadata.ContainsKey(key))
        {
            storage.Metadata.Add(key, value);
        }
        else
        {
            storage.Metadata[key] = value;
        }

        db.FileStorage.SetMetadata("settings", storage.Metadata);
    }

    public static void AddFile(string src, string id, string filename)
    {
        var wc = new WebClient();
        var raw = wc.DownloadData(src);

        using var db = new LiteDatabase(CacheFile);

        db.FileStorage.Upload(id, filename, new MemoryStream(raw));
    }

    public static void AddContentFile(string src, string filename)
    {
        using var db = new LiteDatabase(CacheFile);

        if (db.FileStorage.Exists(filename))
        {
            db.FileStorage.Delete(filename);
        }

        db.FileStorage.Upload(filename, filename, new MemoryStream(Encoding.Default.GetBytes(src)));
    }

    public static byte[] GetCover()
    {
        using var db = new LiteDatabase(CacheFile);

        if (db.FileStorage.Exists("cover.jpg"))
        {
            var ms = new MemoryStream();
            db.FileStorage.Download("cover.jpg", ms);

            return ms.ToArray();
        }

        return null;
    }

    public static byte[] GetFile(ObjectId guid)
    {
        using (var db = new LiteDatabase(CacheFile))
        {
            var ms = new MemoryStream();
            if (db.FileStorage.Exists(guid.ToString()))
            {
                db.FileStorage.Download(guid.ToString(), ms);

                return ms.ToArray();
            }
        }

        return null;
    }

    public static void Remove(BsonDocument doc)
    {
        using var db = new LiteDatabase(CacheFile);

        var collection = db.GetCollection("entries");
        collection.Delete(doc["_id"]);
    }

    public static IEnumerable<T> GetAll<T>()
    {
        using var db = new LiteDatabase(CacheFile);
        var collection = db.GetCollection<T>("entries");

        return collection.FindAll().ToArray();
    }

    public static void Update(BsonDocument document)
    {
        using var db = new LiteDatabase(CacheFile);

        var collection = db.GetCollection("entries");
        collection.Update(document);
    }

    public static object Get(string id)
    {
        using var db = new LiteDatabase(CacheFile);

        var collection = db.GetCollection("entries");

        return collection.FindOne(Query.EQ("_id", id));
    }
}