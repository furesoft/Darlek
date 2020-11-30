function search(args) {
    console.log("hello " + args);

    return 0;
}

registerCommand("search", "Search for a query", "search -q <query>", search);