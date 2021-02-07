using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace WashcoDropBox.Models
{
    public partial class WashcoBox
    {
        public long Id { get; set; }
        public string FilePath { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] //Format as ShortDateTime
        public DateTime? DateAdd { get; set; }
        public string Person { get; set; }
        public string Section { get; set; }
        public string Notee { get; set; }
        public string OrderNo { get; set; }
        [NotMapped]
        public string Customer_name { get; set; }
    }
}
