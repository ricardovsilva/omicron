using HtmlAgilityPack;

namespace omicron.infra
{
    public interface IScraper
    {
        HtmlDocument Get(string path);
    }
}