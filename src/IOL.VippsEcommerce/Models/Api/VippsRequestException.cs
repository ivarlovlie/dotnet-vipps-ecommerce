using System;

namespace IOL.VippsEcommerce.Models.Api
{
    [Serializable]
    public class VippsRequestException : Exception
    {
        public VippsRequestException() { }

        public VippsRequestException(string message)
            : base(message) { }

        public VippsRequestException(string message, Exception inner)
            : base(message, inner) { }

        public VippsErrorResponse ErrorResponse { get; set; }
    }
}