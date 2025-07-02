using ExamPortal.API.Models;

namespace ExamPortal.Business.Interfaces;

public interface IRuleValidationService
{
    void CheckThreshold(RuleEvaluatorRequest request);
    void ValidateRules(RuleEvaluatorRequest request);
}
