using AngleSharp.Dom.Html;

namespace TelegramBotApp
{
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
