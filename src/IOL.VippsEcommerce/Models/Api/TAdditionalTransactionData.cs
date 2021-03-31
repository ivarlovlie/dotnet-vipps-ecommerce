using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api
{
    public class TAdditionalTransactionData
    {
        /// <summary>
        /// Passenger name, initials, and a title.
        /// </summary>
        /// <value>Passenger name, initials, and a title.</value>
        [JsonPropertyName("passengerName")]
        public string PassengerName { get; set; }

        /// <summary>
        /// IATA 3-digit accounting code (PAX); numeric. It identifies the carrier. eg KLM &#x3D; 074
        /// </summary>
        /// <value>IATA 3-digit accounting code (PAX); numeric. It identifies the carrier. eg KLM &#x3D; 074</value>
        [JsonPropertyName("airlineCode")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// IATA 2-letter accounting code (PAX); alphabetical. It identifies the carrier. Eg KLM &#x3D; KL
        /// </summary>
        /// <value>IATA 2-letter accounting code (PAX); alphabetical. It identifies the carrier. Eg KLM &#x3D; KL</value>
        [JsonPropertyName("airlineDesignatorCode")]
        public string AirlineDesignatorCode { get; set; }

        /// <summary>
        /// The ticket&#x27;s unique identifier.
        /// </summary>
        /// <value>The ticket&#x27;s unique identifier.</value>
        [JsonPropertyName("ticketNumber")]
        public string TicketNumber { get; set; }

        /// <summary>
        /// Reference number for the invoice, issued by the agency.
        /// </summary>
        /// <value>Reference number for the invoice, issued by the agency.</value>
        [JsonPropertyName("agencyInvoiceNumber")]
        public string AgencyInvoiceNumber { get; set; }
    }
}