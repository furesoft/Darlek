function crawl() {
    var model = model || {};

    model.header = "hello";
    model.body = "world";

    model.name = $("a[class='ds-copy-link bi-recipe-title']").innerHtml;

    model.content = Repository.GetMetadata("title");

    return model;
};