function changePage(Page) {
    var url = new URL(window.location.href);
    var SerachParams = url.searchParams;
    //Change PageId
    SerachParams.set('Page', Page);
    url.search = SerachParams.toString();

    //new Url String
    var newUrlString = url.toString();
    window.location.replace(newUrlString);
}