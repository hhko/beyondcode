namespace GymDdd.Tests.Architecture.Abstractions.ArchitectureRules.SyntaxLevelRules.Utilities;

public static class ArchitectureRuleUtilities
{
    public static IEnumerable<RuleEvaluationResult> Evaluate(this IEnumerable<IArchitectureRule> rules, ArchUnitNET.Domain.Architecture architecture)
    {
        //List<RuleEvaluationResult> errors = [];

        //foreach (var rule in rules)
        //{
        //    RuleEvaluationResult result = rule.Evaluate(architecture);
        //    if (result.HasViolation)
        //    {
        //        errors.Add(result);
        //    }
        //}

        //return errors;

        List<RuleEvaluationResult> results = [];

        foreach (var rule in rules)
        {
            results.Add(rule.Evaluate(architecture));
        }

        return results;
    }

    public static void ShouldHaveNoViolationRules(this IEnumerable<RuleEvaluationResult> results)
    {
        var violations = results
            .Where(r => r.HasViolation)
            .ToList();

        if (!violations.Any())
            return;

        var message = $"Architecture rule violations found ({violations.Count} rules violated, {violations.Sum(v => v.Results.Count)} total violations):\n\n" +
                      string.Join("\n\n", violations.Select(v =>
                      {
                          var groupedByTypes = v.Results.GroupBy(r => r.AnalyzedObject?.FullName ?? "Unknown");

                          var details = string.Join("\n", groupedByTypes.Select(group =>
                              $"  [{group.Key}]\n" + string.Join("\n", group.Select(r =>
                              $"    - {r.FailDescription ?? "Unknown failure"}"))
                          ));

                          return $"{v.RuleName}:\n{details}";
                      }));

        Assert.Fail(message);
    }
}
