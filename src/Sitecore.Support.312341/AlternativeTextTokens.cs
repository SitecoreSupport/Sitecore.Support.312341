using System.Collections.Concurrent;
using System.Xml;
using Sitecore.Framework.Conditions;
using Sitecore.Xml;

namespace Sitecore.Support
{
  public class AlternativeTextTokens
  {
    public readonly ConcurrentDictionary<string, string> Tokens = new ConcurrentDictionary<string, string>();

    public void AddTokenMapping(XmlNode node)
    {
      Condition.Requires(node, nameof(node)).IsNotNull();

      string name = XmlUtil.GetAttribute("name", node);
      string value = node.InnerText;

      Condition.Ensures(name, nameof(name)).IsNotNull();
      Condition.Ensures(value, nameof(value)).IsNotNull();

      if (!Tokens.ContainsKey(name))
      {
        Tokens[name] = value;
      }
    }
  }
}