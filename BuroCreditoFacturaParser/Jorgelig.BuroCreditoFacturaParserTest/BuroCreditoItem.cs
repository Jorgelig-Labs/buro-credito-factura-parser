using System;
using System.Collections.Generic;
using System.Text;
using Jorgelig.BuroCreditoFacturaParser;
using Jorgelig.BuroCreditoFacturaParser.v1;

namespace Jorgelig.BuroCreditoFacturaParserTest
{
    public class BuroCreditoItem
    {
        public DateTime? Creado { get; set; }
        public DateTime? Fecha { get; set; }
        public string? NumeroControlConsulta { get; set; }
        public string? CodigoReferencia { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Nombres { get; set; }
        public string? TipoConsulta { get; set; }
        public string? Score { get; set; }
        public string? Auditoria { get; set; }
        public string? IntercambioCirculoCredito { get; set; }
        public string? Paquete { get; set; }
        public string? Hit { get; set; }
        public string? Optimiza { get; set; }
        public decimal? Costo { get; set; }

    }

    public static class Extensions
    {
        public static BuroCreditoItem ToBuroCreditoItem(this BureauTextReader trackingReader) => new BuroCreditoItem
        {
            Creado = DateTime.Now,
            Fecha = trackingReader.BcDataTimestamp,
            NumeroControlConsulta = trackingReader.BcDataBcControlNumber,
            CodigoReferencia = trackingReader.BcDataReferenceCode,
            ApellidoPaterno = trackingReader.BcDataLastName,
            ApellidoMaterno = trackingReader.BcDataMaidenName,
            Nombres = trackingReader.BcDataFirstName,
            TipoConsulta = trackingReader.BcDataSvco,
            Score = trackingReader.BcDataSc,
            Auditoria = trackingReader.BcDataA,
            IntercambioCirculoCredito = trackingReader.BcDataIC,
            Paquete = trackingReader.BcDataP,
            Hit = trackingReader.BcDataHN,
            Optimiza = trackingReader.BcDataOp,
            Costo = trackingReader.BcDataCosto
        };

    }
}
