using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotApp
{
    interface IParserSettings
    {
        string BaseUrl { get; set; }

        string Prefix { get; set; }
    }
}
