using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Model
{

    public class ApiResponse
    {
        public int Code { get; set; }
        public string? Type { get; set; }
        public string? Message { get; set; }
    }

}
