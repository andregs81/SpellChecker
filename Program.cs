using HtmlAgilityPack;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace CoGrOOSpellChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "e à bem da verdade, foi direcionada ao pessoal de documentação!";
            var values = new NameValueCollection();
            values.Add("text", text);
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                var result = client.UploadValues("http://comunidade.cogroo.org/grammar", "POST", values);
                var resultString = Encoding.UTF8.GetString(result);
                var html = new HtmlDocument();
                html.LoadHtml(resultString);

                var rows = html.DocumentNode.SelectSingleNode("//table[@id='analysisTable_id']/tbody");

                foreach (var row in rows.Elements("tr"))
                {
                    var cell = row.Descendants("td").Skip(1).FirstOrDefault();
                    var grammerErrorHtml = cell.InnerHtml;
                    // example
                    // e <span class="grammarerror" title="Não ocorre crase antes de palavras masculinas.">à</span> bem da verdade, foi direcionada ao pessoal de documentação!
                }
            }
        }
    }
}
