using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Linq;
namespace Webstore
{
    [MetadataType(typeof(CustomerMetaData))]
    public partial class Customer
    {
        public class CustomerMetaData
        {
            [Required(ErrorMessage="Type your firstname")]
            [StringLength(50, ErrorMessage="Max 50 characters in firstname")]
            public string firstname { get; set; }

            [Required(ErrorMessage = "Type your lastname")]
            [StringLength(50, ErrorMessage = "Max 50 characters in lastname")]
            public string lastname { get; set; }

            [Required(ErrorMessage = "Type your address")]
            [StringLength(100, ErrorMessage = "Max 100 characters in firstname")]
            public string address { get; set; }

            [Required(ErrorMessage = "Type your zipcode")]
            [StringLength(4, ErrorMessage = "Max 4 characters in firstname")]
            public string zipcode { get; set; }


            //TODO regex validation
            [Required(ErrorMessage = "Type your email")]
            [StringLength(100, ErrorMessage = "Max 100 characters in firstname")]
            [DataType(DataType.EmailAddress)]
            public string email { get; set; }

            [Required(ErrorMessage = "Type your telephonenumber")]
            [StringLength(50, ErrorMessage = "Max 50 characters in firstname")]
            public string telephone { get; set; }

            [Required(ErrorMessage = "Type your password")]
            [StringLength(50, ErrorMessage = "Max 50 characters in firstname")]
            [DataType(DataType.Password)]
            public byte[] password { get; set; }

            
        }

    }
}