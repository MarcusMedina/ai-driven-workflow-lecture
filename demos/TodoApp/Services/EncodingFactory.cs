using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace TodoApp.Services;

public static class EncodingFactory
{
    public static HtmlEncoder CreateHtmlEncoder() =>
        HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement);
}
