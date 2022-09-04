using Darlek.Core;
using Darlek.Core.Epub;
using LiteDB;
using Scriban.Runtime;
using Spectre.Console;
using System;
using System.Linq;

namespace Darlek.Commands.Manage;

public class PublishCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
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
            var sobj = Renderer.BuildScriptObject(recipes[i]);
            sobj.Add("book", bookInfo);
            sobj.Add("page", i + 1);

            var image = Repository.GetFile(recipes[i]["_id"].AsObjectId);
            if (image != null)
            {
                AnsiConsole.Status().AutoRefresh(true).Start("Add Cover Image", (_) => {
                    epub.AddFile(recipes[i]["_id"].AsObjectId + ".jpg", image, Core.Epub.Format.EpubContentType.ImageJpeg);
                });
            }

            if (recipes[i].ContainsKey("Name"))
            {
                AnsiConsole.Status().AutoRefresh(true).Start($"Add '{recipes[i]["Name"]}'", (_) => {
                    var rendered = Renderer.RenderObject(sobj, TemplateSelector.GetTemplate());
                    epub.AddChapter(recipes[i]["Name"], rendered);
                });
            }
        }

        if (Repository.GetCover() != null)
        {
            epub.SetCover(Repository.GetCover(), ImageFormat.Jpeg);
        }
        AnsiConsole.Status().AutoRefresh(true).Start("Saving To File", (_) => {
            epub.Write(filename);
        });

        parentMenu.WaitAndShow();
    }
}