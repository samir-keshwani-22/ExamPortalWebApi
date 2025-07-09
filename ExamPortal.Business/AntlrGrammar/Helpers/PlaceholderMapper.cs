using System.Text.RegularExpressions;

namespace ExamPortal.Business.AntlrGrammar.Helpers;

public static class PlaceholderMapper
{
    public static string ReplacePlaceholders(string input, string primaryAlias = "a")
    {
        input = Regex.Replace(input,
           @"#\{\s*([a-zA-Z0-9\._]+)\s*\}\s*not\s*in\s*\{\s*([a-zA-Z0-9\._,\s]+)\s*\}",
           m => $"NOT has({m.Groups[2].Value}, {m.Groups[1].Value})",
           RegexOptions.IgnoreCase);

        input = Regex.Replace(input,
            @"#\{account\}\s*is\s*\{(source|dest)\}",
            match => {
                var accountType = match.Groups[1].Value.ToLower();
                var accountTypeValue = accountType == "source" ? "Source" : "Destination";
                return $"{primaryAlias}.trans_acct_type = '{accountTypeValue}'";
            }, RegexOptions.IgnoreCase);

        input = Regex.Replace(input, @"AND\s*$", "", RegexOptions.IgnoreCase).Trim();

        input = Regex.Replace(input,
            @"#\{([^}]+)\}\s*>=\s*\{([^}]+)\}",
            m => $"{GetFieldMapping(m.Groups[1].Value, primaryAlias)} >= '{m.Groups[2].Value}'");

        input = Regex.Replace(input,
            @"#\{([^}]+)\}\s*<=\s*\{([^}]+)\}",
            m => $"{GetFieldMapping(m.Groups[1].Value, primaryAlias)} <= '{m.Groups[2].Value}'");

        input = Regex.Replace(input,
            @"#\{([^}]+)\}\s*>\s*\{([^}]+)\}",
            m => $"{GetFieldMapping(m.Groups[1].Value, primaryAlias)} > '{m.Groups[2].Value}'");

        input = Regex.Replace(input,
            @"#\{([^}]+)\}\s*<\s*\{([^}]+)\}",
            m => $"{GetFieldMapping(m.Groups[1].Value, primaryAlias)} < '{m.Groups[2].Value}'");

        input = Regex.Replace(input,
            @"#\{([^}]+)\}\s*=\s*\{([^}]+)\}",
            m => {
                var field = m.Groups[1].Value.Trim();
                var values = m.Groups[2].Value;

                if (values.Contains(','))
                {
                    var valueList = values.Split(',').Select(v => $"'{v.Trim()}'");
                    var fieldMapping = GetFieldMapping(field, primaryAlias);
                    return $"{fieldMapping} in ({string.Join(", ", valueList)})";
                }
                else
                {
                    var fieldMapping = GetFieldMapping(field, primaryAlias);
                    return $"{fieldMapping} = '{values.Trim()}'";
                }
            });

        input = Regex.Replace(input,
            @"#\{([^}]+)\}\s*!=\s*\{([^}]+)\}",
            m => $"{GetFieldMapping(m.Groups[1].Value, primaryAlias)} != '{m.Groups[2].Value}'");

        input = Regex.Replace(input,
            @"#\{([^}]+)\}\s*in\s*\{([^}]+)\}",
            m => {
                var field = GetFieldMapping(m.Groups[1].Value, primaryAlias);
                var values = m.Groups[2].Value.Split(',').Select(v => $"'{v.Trim()}'");
                return $"{field} in ({string.Join(", ", values)})";
            });

        var simpleReplacements = new Dictionary<string, string>
        {
            ["#{account}"] = $"{primaryAlias}.acct_id",
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
            ["#{addr_country}"] = $"{primaryAlias}.addr_country",
            ["#{d.related_parties}"] = "d.related_parties",
            ["#{s.related_parties}"] = "s.related_parties",
            ["#{asset_type}"] = "t.asset_type",
            ["{dest}"] = "'Destination'",
            ["{source}"] = "'Source'",
            ["{location}"] = "t.location",
            ["{Deposit}"] = "'Deposit'",
            ["{Cheque}"] = "'Cheque'",
            ["{Transfer}"] = "'Transfer'",
            ["{AssetPurchase}"] = "'AssetPurchase'",
            ["{ATMWithdrawal}"] = "'ATMWithdrawal'",
            ["{High}"] = "'High'",
        };

        foreach (var kvp in simpleReplacements)
        {
            input = input.Replace(kvp.Key, kvp.Value);
        }

        input = Regex.Replace(input, @"(\S)AND(\S)", "$1 AND $2");

        return input
            .Replace("in {monitored_keywords_high}", "in (SELECT value FROM ref_list_items WHERE risk_level = 'High')")
            .Replace("in {monitored_keywords_prohibited}", "in (SELECT value FROM ref_list_items WHERE risk_level = 'Prohibited')")
            .Replace("in {country_risk_high}", "in (SELECT value FROM ref_list_items WHERE risk_level = 'High')")
            .Replace("in {country_risk_prohibited}", "in (SELECT value FROM ref_list_items WHERE risk_level = 'Prohibited')");
    }

    private static string GetFieldMapping(string field, string primaryAlias = "a")
    {
        var fieldMappings = new Dictionary<string, string>
        {
            ["account"] = $"{primaryAlias}.acct_id",
            ["trans_type"] = "t.trans_type",
            ["trans_method"] = "t.trans_method",
            ["s.entity_type"] = "s.entity_type",
            ["s.risk_level"] = "s.risk_level",
            ["amount"] = "t.amount",
            ["s.acct_id"] = "s.acct_id",
            ["d.acct_id"] = "d.acct_id",
            ["d.risk_level"] = "d.risk_level",
            ["industry"] = "t.industry",
            ["trans_ip"] = "t.trans_ip",
            ["acct_open_ip"] = "t.acct_open_ip",
            ["description"] = "t.description",
            ["purpose"] = "t.purpose",
            ["location"] = "t.location",
            ["addr_country"] = $"{primaryAlias}.addr_country",
            ["d.related_parties"] = "d.related_parties",
            ["s.related_parties"] = "s.related_parties",
            ["asset_type"] = "t.asset_type",
        };

        return fieldMappings.TryGetValue(field, out var mapping) ? mapping : $"t.{field}";
    }
}
