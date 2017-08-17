using HtmlAgilityPack;

namespace omicron.infra
{
    public class HtmlScraper : IScraper
    {
        private readonly string baseUrl;

        public HtmlScraper(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public HtmlDocument Get(string path)
        {
            var url = baseUrl + path;
            var web = new HtmlWeb();
            return web.Load(url);
        }
    }
}