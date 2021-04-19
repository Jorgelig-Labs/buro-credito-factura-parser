using System;
using System.Collections.Generic;
using System.Text;

namespace Jorgelig.BuroCreditoFacturaParserTest
{
    public class BuroCreditoHeaderItem
    {
        public DateTime? IniciaPeriodo { get; set; }
        public DateTime? FinalizaPeriodo { get; set; } 
        public DateTime? FechaFactura { get; set; }
        
        public string? NombreCliente { get; set; }
        
    }
}
