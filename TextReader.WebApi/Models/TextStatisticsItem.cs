using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TextReader.WebApi.Models
{
    public class TextStatisticsItem
    {

        [Required]
        public string Text { get; set; }
    }
}
