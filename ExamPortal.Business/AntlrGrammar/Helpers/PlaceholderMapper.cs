namespace ExamPortal.Business.AntlrGrammar.Helpers;

public static class PlaceholderMapper
{
    public static string ReplacePlaceholders(string input)
    {
        var simpleReplacements = new Dictionary<string, string>
        {
            ["#{account}"] = "d.acct_id",
            ["#{trans_type}"] = "t.trans_type",
            ["#{trans_method}"] = "t.trans_method",
            ["#{s.entity_type}"] = "s.entity_type",
            ["#{s.risk_level}"] = "s.risk_level",
            ["#{amount}"] = "t.amount",
            ["#{s.acct_id}"] = "s.acct_id",
            ["#{d.acct_id}"] = "d.acct_id",
            ["#{d.risk_level}"] = "d.risk_level",
            ["#{industry}"] = "t.industry",
            ["#{trans_ip}"] = "t.trans_ip",
            ["#{acct_open_ip}"] = "t.acct_open_ip",
            ["#{description}"] = "t.description",
            ["#{purpose}"] = "t.purpose",
            ["#{location}"] = "t.location",
            ["#{d.related_parties}"] = "d.related_parties",
            ["#{s.related_parties}"] = "s.related_parties",
            ["{dest}"] = "'Destination'",
            ["{source}"] = "'Source'",
            ["{Deposit}"] = "'Deposit'",
            ["{Cheque}"] = "'Cheque'",
            ["{Transfer}"] = "'Transfer'",
            ["{High}"] = "'High'",
        };

        foreach (var kvp in simpleReplacements)
        {
            input = input.Replace(kvp.Key, kvp.Value);
        }

        input = System.Text.RegularExpressions.Regex.Replace(input, @"#{(\w+)}\s*=\s*{([^}]+)}", match =>
      {
          var field = match.Groups[1].Value;
          var values = match.Groups[2].Value.Split(',').Select(v => $"'{v.Trim()}'");
          return $"d.{field} IN ({string.Join(", ", values)})";
      });

        return input
       .Replace("in {monitored_keywords_high}", "in (SELECT value FROM ref_list_items WHERE risk_level = 'High')")
       .Replace("in {monitored_keywords_prohibited}", "in (SELECT value FROM ref_list_items WHERE risk_level = 'Prohibited')")
       .Replace("in {country_risk_high}", "in (SELECT value FROM ref_list_items WHERE risk_level = 'High')")
       .Replace("in {country_risk_prohibited}", "in (SELECT value FROM ref_list_items WHERE risk_level = 'Prohibited')");
    }
}
