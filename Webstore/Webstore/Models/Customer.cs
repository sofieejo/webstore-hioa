using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Linq;

namespace Webstore.Models
{
    [MetadataType(typeof(CustomerMetaData))]
    public partial class customer
    {
        public class CustomerMetaData
        {
            [Required(ErrorMessage="Type your firstname")]
            [StringLength(50, ErrorMessage="Max 50 characters in your firstname")]
            [RegularExpression(@"[A-Za-z \-]+", ErrorMessage = "Use only letters in your name" )]
            public string firstname { get; set; }

            [Required(ErrorMessage = "Type your lastname")]
            [StringLength(50, ErrorMessage = "Max 50 characters in your lastname")]
            [RegularExpression(@"[A-Za-z \-]+", ErrorMessage = "Use only letters in your name")]
            public string lastname { get; set; }

            [Required(ErrorMessage = "Type your address")]
            [StringLength(100, ErrorMessage = "Max 100 characters in your address")]
            [RegularExpression(@"[A-Za-z0-9 \-]+", ErrorMessage = "Please do not use any spesial characters in your address, only letters and numbers")]
            public string address { get; set; }

            [Required(ErrorMessage = "Type your zipcode")]
            [StringLength(4, ErrorMessage = "Max 4 characters in your zipcode")]
            [RegularExpression(@"^[\d]{4}$", ErrorMessage = "Your zipcode must contain 4 digits")]
            public string zipcode { get; set; }

            //TODO regex validation
            [Required(ErrorMessage = "Type your email")]
            [StringLength(100, ErrorMessage = "Max 100 characters in your email address")]
            [RegularExpression(@"^([\w\.\-]+)@([\w\-\.]+)\.([\w]{2,3})$", 
              ErrorMessage= "Make sure you have typed the correct form on your email address. Example: abc@def.com")]
            [DataType(DataType.EmailAddress)]
            public string email { get; set; }

            [Required(ErrorMessage = "Type your telephonenumber")]
            [StringLength(8, ErrorMessage = "Max 8 digits in telephonenumber")]
            [RegularExpression(@"^[\d]{8}$", ErrorMessage = "Your telephonenumber must contain 8 digits")]
            public string telephone { get; set; }
        }
    }
}