using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Jorgelig.BuroCreditoFacturaParser;
using Jorgelig.BuroCreditoFacturaParser.v1;
using Xunit;

namespace Jorgelig.BuroCreditoFacturaParserTest
{
    public class BureauTextReaderTest
    {
        private string FilePath = $"{Directory.GetCurrentDirectory()}\\Files\\01111OMISIONR.wri";

        public BureauTextReaderTest()
        {
      
        }


        [Fact]
        public void Test_Read_File_Return_BureauTextReader()
        {
            using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            string line = String.Empty;


            using (var _bureauTextReader = new BureauTextReader(sr))
            {
                Assert.NotNull(_bureauTextReader);
            }
        }

        [Fact]
        public void Read_File_Return_Page_Number()
        {
            using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            string line = String.Empty;


            using (var _bureauTextReader = new BureauTextReader(sr))
            {
                while (!_bureauTextReader.IsItemDataLine())
                {
                    _bureauTextReader.ReadLine();
                }
                Assert.NotNull(_bureauTextReader.PageNumber);
                
            }
        }

        [Fact]
        public void Read_File_Return_Item_List()
        {
            using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            string line = String.Empty;
            var items = new List<BuroCreditoItem>();


            using (var _bureauTextReader = new BureauTextReader(sr))
            {
                Assert.NotNull(_bureauTextReader);

                while ((line = _bureauTextReader.ReadLine()) != null)
                {
                    if (_bureauTextReader.IsItemDataLine())
                    {
                        var item = _bureauTextReader.ToBuroCreditoItem();
                        items.Add(item);
                    }
                    
                }

                Assert.NotEmpty(items);
            }
        }

        [Fact]
        public void Create_Item_List_Return_All_FirstName_Valid()
        {
            using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            string line = String.Empty;
            var items = new List<BuroCreditoItem>();


            using (var _bureauTextReader = new BureauTextReader(sr))
            {
                Assert.NotNull(_bureauTextReader);

                while ((line = _bureauTextReader.ReadLine()) != null)
                {
                    if (_bureauTextReader.IsItemDataLine())
                    {
                        var item = _bureauTextReader.ToBuroCreditoItem();
                        items.Add(item);
                    }
                    
                }

                var itemsCount = items.Count;
                var firstNameValidCount = items.Count(i => !string.IsNullOrWhiteSpace(i.Nombres));
                Assert.Equal(itemsCount,firstNameValidCount);
            }
        }

        [Fact]
        public void Create_Encabezado_List_Return_Periodo_Fecha_Factura()
        {
            using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            string line = String.Empty;
            
            using (var _bureauTextReader = new BureauTextReader(sr))
            {
                BuroCreditoHeaderItem item = null;
                while ((line = _bureauTextReader.ReadLine()) != null && item == null)
                {
                    if (_bureauTextReader.IsItemDataLine())
                    {
                        item = new BuroCreditoHeaderItem
                        {
                            FechaFactura = _bureauTextReader.BcBillingDate,
                            IniciaPeriodo = _bureauTextReader.BcStartPeriodDate,
                            FinalizaPeriodo = _bureauTextReader.BcEndPeriodDate,
                            NombreCliente = _bureauTextReader.BcClientName
                        };
                    }
                    
                }
                
                Assert.NotNull(item?.NombreCliente);
                Assert.NotNull(item?.FechaFactura);
                Assert.NotNull(item?.IniciaPeriodo);
                Assert.NotNull(item?.FinalizaPeriodo);
            }
        }

        
    }
}
