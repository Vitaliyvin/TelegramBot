using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotApp{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;

        HtmlLoader loader;

        bool isActive;

        #region Properties

        public IParser<T> Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        #endregion

        public event Action<object, T> OnFinish;


        public ParserWorker(IParser<T> parser, IParserSettings parserSettings)
        {
            this.parser = parser;
            this.parserSettings = parserSettings;
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Worker()
        {
            if (!isActive)
            {
                return;
            }
            loader = new HtmlLoader(Settings);
            var source = await loader.GetSourcePage();
            var domParser = new HtmlParser();

            var document = await domParser.ParseAsync(source);

            var result = parser.Parse(document);

            OnFinish?.Invoke(this, result);

            isActive = false;

        }
    }
}
