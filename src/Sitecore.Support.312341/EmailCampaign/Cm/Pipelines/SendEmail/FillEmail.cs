using Sitecore.Diagnostics;
using Sitecore.EmailCampaign.Cm.Pipelines.SendEmail;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.Modules.EmailCampaign.Core.Crypto;
using Sitecore.Modules.EmailCampaign.Messages;

namespace Sitecore.Support.EmailCampaign.Cm.Pipelines.SendEmail
{
  public class FillEmail
  {
    private readonly IStringCipher _cipher;
    private readonly ILogger _logger;

    public FillEmail(IStringCipher cipher, ILogger logger)
    {
      Assert.ArgumentNotNull(cipher, "cipher");
      Assert.ArgumentNotNull(logger, "logger");
      _cipher = cipher;
      _logger = logger;
    }

    private MailFiller GetMailFiller(SendMessageArgs args)
    {
      var ecmMessage = args.EcmMessage as HtmlMailBase;
      if (ecmMessage != null)
      {
        return new HtmlMailFiller(ecmMessage, args, _logger, _cipher);
      }

      var message = args.EcmMessage as MailMessageItem;
      if (message != null)
      {
        return new MailMessageFiller(message, args, _cipher);
      }

      return null;
    }

    public void Process(SendMessageArgs args)
    {
      if (args.EcmMessage != null)
      {
        var mailFiller = GetMailFiller(args);
        mailFiller?.FillEmail();
      }
    }
  }
}