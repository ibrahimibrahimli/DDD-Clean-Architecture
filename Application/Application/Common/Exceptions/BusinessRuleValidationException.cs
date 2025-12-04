using System.Security.Cryptography.X509Certificates;

namespace Application.Common.Exceptions
{
    public sealed class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException( string businessRuleCode, string message)
            : base($"Business rule validation failed: {businessRuleCode} - {message}")
        {
           BusinessRuleCode = businessRuleCode;
        }
        public string BusinessRuleCode { get; }
    }
}
