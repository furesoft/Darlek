using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using BookGenerator.Core.Crawler;
using LiteDB;

namespace BookGenerator.Core
{
    public static class Repository
    {
        public static string CacheFile = Path.Combine(Environment.CurrentDirectory, ".rbg");

        public static void Clear()
        {
            var collectionName = GetCollectionName();
            using (var db = new LiteDatabase(CacheFile))
            {
                var collection = db.GetCollection(collectionName);
                collection.DeleteAll();
            }
        }

        public static void CollectCustomCommands()
        {
            using (var db = new LiteDatabase(CacheFile))
            {
                foreach (var finfo in db.FileStorage.FindAll().Where(_ => _.Filename.StartsWith("commands")))
                {
                    var eval = EvaluatorSelector.GetEvaluator<object>(finfo.Filename);
                    if (eval != null)
                    {
                        var ms = new MemoryStream();
                        db.FileStorage.Download(finfo.Filename, ms);

                        var source = Encoding.Default.GetString(ms.ToArray());
                        eval.AddCustomCommands(source);
                    }
                }
            }
        }

        public static string GetFile(string name)
        {
            using (var db = new LiteDatabase(CacheFile))
            {
                var ms = new MemoryStream();
                db.FileStorage.Download(name, ms);

                return Encoding.Default.GetString(ms.ToArray());
            }
        }

        internal static void RemoveByName(string name)
        {
            var collectionName = GetCollectionName();
            using (var db = new LiteDatabase(CacheFile))
            {
                var collection = db.GetCollection(collectionName);
                var id = collection.FindOne(Query.EQ("name", name)).AsObjectId;
                collection.Delete(id);
            }
        }

        public static void Add(BsonDocument model)
        {
            var collectionName = GetCollectionName();
            using (var db = new LiteDatabase(CacheFile))
            {
                var collection = db.GetCollection(collectionName);
                collection.Insert(model);
            }
        }

        public static string GetMetadata(string key)
        {
            using (var db = new LiteDatabase(CacheFile))
            {
                var storage = db.FileStorage.FindById("metadata");
                if (storage.Metadata.ContainsKey(key))
                {
                    return storage.Metadata[key];
                }

                return null;
            }
        }

        public static void RemoveSetting(string key)
        {
            using (var db = new LiteDatabase(CacheFile))
            {
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
        }

        public static string GetSetting(string key)
        {
            using (var db = new LiteDatabase(CacheFile))
            {
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
        }

        public static void SetMetadata(string key, string value)
        {
            using (var db = new LiteDatabase(CacheFile))
            {
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
        }

        public static void SetSetting(string key, string value)
        {
            using (var db = new LiteDatabase(CacheFile))
            {
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
        }

        public static void AddFile(string src, string id, string filename)
        {
            var wc = new WebClient();
            var raw = wc.DownloadData(src);

            using (var db = new LiteDatabase(CacheFile))
            {
                db.FileStorage.Upload(id, filename, new MemoryStream(raw));
            }
        }

        public static void AddContentFile(string src, string filename)
        {
            using (var db = new LiteDatabase(CacheFile))
            {
                if (db.FileStorage.Exists(filename))
                {
                    db.FileStorage.Delete(filename);
                }

                db.FileStorage.Upload(filename, filename, new MemoryStream(Encoding.Default.GetBytes(src)));
            }
        }

        public static byte[] GetCover()
        {
            using (var db = new LiteDatabase(CacheFile))
            {
                if (db.FileStorage.Exists("cover.jpg"))
                {
                    var ms = new MemoryStream();
                    db.FileStorage.Download("cover.jpg", ms);

                    return ms.ToArray();
                }

                return null;
            }
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

        public static void Remove(string id)
        {
            var collectionName = GetCollectionName();
            using (var db = new LiteDatabase(CacheFile))
            {
                var collection = db.GetCollection(collectionName);
                collection.Delete(id);
            }
        }

        private static string GetCollectionName()
        {
            return "entries";
        }

        public static IEnumerable<T> GetAll<T>()

        {
            var collectionName = GetCollectionName();
            using (var db = new LiteDatabase(CacheFile))
            {
                var collection = db.GetCollection<T>(collectionName);

                return collection.FindAll().ToArray();
            }
        }
    }
}