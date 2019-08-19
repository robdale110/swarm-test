
using App.Api.Models;

namespace App.Api.Documents
{
    public class PasswordDetails
    {
        public bool HasUpper { get; set; }

        public bool HasLower { get; set; }

        public bool HasNumber { get; set; }

        public bool HasSpecial { get; set; }

        public int Length { get; set; }

        public string ApiVersion { get; set; }

        public string ApiServer { get; set; }

        public PasswordDetails(PasswordResponse response)
        {
            ApiVersion = response.ApiVersion;
            ApiServer = response.ApiServer;
            Length = response.Password.Length;
            HasUpper = response.Password.IndexOfAny(Upper) > -1;
            HasLower = response.Password.IndexOfAny(Lower) > -1;
            HasNumber = response.Password.IndexOfAny(Number) > -1;
            HasSpecial = response.Password.IndexOfAny(Special) > -1;
        }

        private readonly char[] Upper = "ABCDEFGHJKLMNOPQRSTUVWXYZ".ToCharArray();
        private readonly char[] Lower = "abcdefghijkmnopqrstuvwxyz".ToCharArray();
        private readonly char[] Number = "0123456789".ToCharArray();
        private readonly char[] Special = "!@$?_-".ToCharArray();
    }
}