using System;
using System.Linq;
using BookGenerator.Core;
using BookGenerator.Core.CLI;
using BookGenerator.Core.Epub;
using BookGenerator.Properties;
using LiteDB;
using Scriban;
using Scriban.Runtime;

namespace BookGenerator.Commands
{
    public class PublishCommand : ICliCommand
    {
        public string Name => "publish";

        public string HelpText => "publish";

        public string Description => "Generate Epub";

        public int Invoke(CommandlineArguments args)
        {
            var filename = Repository.GetMetadata("filename") ?? "my_book.epub";

            var epub = new EpubWriter();
            epub.SetTitle(Repository.GetMetadata("title"));
            epub.AddAuthor(Repository.GetMetadata("author"));

            var recipes = Repository.GetAll<BsonDocument>().ToArray();
            var bookInfo = new ScriptObject();
            bookInfo.Add("pages", recipes.Length);
            bookInfo.Add("title", Repository.GetMetadata("title"));
            bookInfo.Add("author", Repository.GetMetadata("author"));
            bookInfo.Add("date", DateTime.Now.ToString());

            for (var i = 0; i < recipes.Length; i++)
            {
                bookInfo.Add("page", i + 1);

                var sobj = Renderer.BuildScriptObject(recipes[i]);
                sobj.Add("book", bookInfo);

                var rendered = Renderer.RenderObject(sobj, TemplateSelector.GetTemplate());

                byte[] image = Repository.GetFile(recipes[i]["_id"].AsObjectId);
                if (image != null)
                {
                    epub.AddFile(recipes[i]["_id"].AsObjectId + ".jpg", image, Core.Epub.Format.EpubContentType.ImageJpeg);
                }

                if (recipes[i].ContainsKey("Name"))
                {
                    epub.AddChapter(recipes[i]["Name"], rendered);
                }
            }

            if (Repository.GetCover() != null)
            {
                epub.SetCover(Repository.GetCover(), ImageFormat.Jpeg);
            }

            epub.Write(filename);

            return 0;
        }
    }
}