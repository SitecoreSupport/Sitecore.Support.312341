using System;
using Sitecore.Diagnostics;
using Sitecore.EDS.Core.Dispatch;
using Sitecore.EmailCampaign.Cm.Pipelines.SendEmail;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Modules.EmailCampaign;
using Sitecore.Modules.EmailCampaign.Core.Crypto;
using Sitecore.Modules.EmailCampaign.Messages;

namespace Sitecore.Support.EmailCampaign.Cm.Pipelines.SendEmail
{
  public class HtmlMailFiller : Sitecore.EmailCampaign.Cm.Pipelines.SendEmail.HtmlMailFiller
  {
    private readonly HtmlMailBase _htmlMailBase;

    public HtmlMailFiller(HtmlMailBase message, SendMessageArgs args, ILogger logger, IStringCipher cipher) : base(
      message, args, logger, cipher)
    {
      Assert.ArgumentNotNull(message, "message");
      _htmlMailBase = message;
    }

    protected override void FillBody()
    {
      var utcNow = DateTime.UtcNow;
      var body = _htmlMailBase.Body;
      Email.ContentType = MessageContentType.Html;
      Email.HtmlBody = _htmlMailBase.ReplaceTokens(body);
      Util.TraceTimeDiff("SetEmailHtmlBody(ReplaceTokens(Body))", utcNow);
      utcNow = DateTime.UtcNow;
      Email.PlainTextBody = _htmlMailBase.ReplaceTokens(_htmlMailBase.AlternateText);
      Email.PlainTextBody = AlternativeTextLinksHelper.ReplaceAllLinkTokens(_htmlMailBase, _htmlMailBase.AlternateText, false);
      Util.TraceTimeDiff("SetEmailAltBody(ReplaceTokens(AlternateText))", utcNow);
    }
  }
}