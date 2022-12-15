using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using LoginRegister.Models;

namespace LoginRegister.Models
{
    public class AccountDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int  AccountNumber{ get; set; }
           
        public int UserId { get; set; }

        public int Amount { get; set; }
    }
}