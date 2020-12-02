using System;
using System.ComponentModel.DataAnnotations;

namespace POSLite.Domain
{
    public abstract  class Base 
    {
        [Display(AutoGenerateField = false)]
        public DateTime CreatedAt { get; set; }
        // public Staff CreatedBy { get; set; }
        [Display(AutoGenerateField = false)]
        public DateTime UpdatedAt { get; set; }
        //  public Staff UpdatedBy { get; set; }
        [Display(AutoGenerateField = false)]
        public string Terminus { get; set; }
    }
}
