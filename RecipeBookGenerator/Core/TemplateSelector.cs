using BookGenerator.Properties;

namespace BookGenerator.Core
{
    public static class TemplateSelector
    {
        public static string GetTemplate()
        {
            if (Repository.GetSetting("template") != null)
            {
                return Repository.GetFile("template.html");
            }

            return Resources.Template;
        }

        public static void SetTemplate(string template)
        {
            Repository.AddContentFile(template, "template.html");
            Repository.SetSetting("template", "template.html");
            Repository.SetSetting("template", "template.html");
        }
    }
}