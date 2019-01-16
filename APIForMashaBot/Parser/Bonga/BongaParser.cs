using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;

namespace TelegramBotApp
{
    class BongaParser : IParser<string>
    {
        public string Parse(IHtmlDocument document)
        {
            var items = document.QuerySelectorAll("span").Where(item => item.ClassName != null && item.ClassName.Contains("no_chat"));
            foreach (var item in items)
            {
                if (item.TextContent == "Модель оффлайн") return "оффлайн"; 
            }
            return "онлайн";
        }
    }
}
