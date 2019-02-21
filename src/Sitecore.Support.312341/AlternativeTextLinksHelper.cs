using Sitecore.Configuration;
using Sitecore.Modules.EmailCampaign.Core.Pipelines.GenerateLink;
using Sitecore.Modules.EmailCampaign.Messages;
using Sitecore.Pipelines;

namespace Sitecore.Support
{
  public class AlternativeTextLinksHelper
  {
    private static AlternativeTextTokens _tokens = Factory.CreateObject("exm/plainTextTokens", true) as AlternativeTextTokens;

    public static string ReplaceAllLinkTokens(MailMessageItem item, string alternateText, bool preview)
    {
      var result = alternateText;
      if (_tokens != null)
      {
        foreach (var token in _tokens.Tokens)
        {
          var encodedToken = EncodeLink(item, token.Value, preview);
          result = result.Replace(token.Key, encodedToken);
        }
      }

      return result;
    }

    private static string EncodeLink(MailMessageItem item, string link, bool preview)
    {
      GenerateLinkPipelineArgs args = new GenerateLinkPipelineArgs(link, item, preview);
      CorePipeline.Run("modifyHyperlink", args);
      if (!args.Aborted)
      {
        return args.GeneratedUrl;
      }
      return null;
    }
  }
}