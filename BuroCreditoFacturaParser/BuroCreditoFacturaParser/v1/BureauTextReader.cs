using System;
using System.Globalization;
using System.IO;

namespace Jorgelig.BuroCreditoFacturaParser.v1
{
    public class BureauTextReader : TextReader
    {
        private TextReader _baseReader;
        private int _position;
        private int _numberLine;
        private string _line;
        
        public string BcBillingCode;
        public int? BcBillingPageNumber;
        public DateTime? BcStartPeriodDate;
        public DateTime? BcEndPeriodDate;
        public DateTime? BcBillingDate;
        public string BcClientName;

        public DateTime? BcDataTimestamp;
        public string BcDataBcControlNumber;
        public string BcDataReferenceCode;
        public string BcDataLastName;
        public string BcDataMaidenName;
        public string BcDataFirstName;
        public string BcDataSvco;
        public string BcDataSc;
        public string BcDataA;
        public string BcDataIC;
        public string BcDataP;
        public string BcDataHN;
        public string BcDataOp;
        public decimal? BcDataCosto;

        public int? PageNumber => BcBillingPageNumber;




        public BureauTextReader(TextReader baseReader)
        {
            _baseReader = baseReader;
        }

        public BureauTextReader(MemoryStream msStream)
        {
            msStream.Position = 0;
            var textReader = new StreamReader(msStream);
            _baseReader = textReader;
        }

        #region override

        public override int Read()
        {
            _position++;
            return _baseReader.Read();
        }

        public override string ReadLine()
        {
            _numberLine++;
            _line = _baseReader.ReadLine();
            if (IsLineToIgnore())
            {
                Console.WriteLine($"{_numberLine}: '{_line}'");
            }
            else if (IsPageNumberLine())
            {
                BcBillingPageNumber = GetPageNumber();
                BcBillingCode = GetCode();
            }
            else if (IsBillingDateLine())
            {
                BcStartPeriodDate = GetStartPeriodDate();
                BcEndPeriodDate = GetEndPeriodDate();
                BcBillingDate = GetBillingDate();
            }
            else if (IsClientNameLine())
            {
                BcClientName = GetClientName();
            }
            else if (IsItemDataLine())
            {
                BcDataTimestamp = GetDataTimestamp();
                BcDataBcControlNumber = GetDataBcControlNumber();
                BcDataReferenceCode = GetDataUsuarioReferenceCode();
                BcDataLastName = GetDataLastName();
                BcDataMaidenName = GetDataMaidenName();
                BcDataFirstName = GetDataFirstName();
                BcDataSvco = GetDataSvco();
                BcDataSc = GetDataSc();
                BcDataA = GetDataA();
                BcDataIC = GetDataIC();
                BcDataP = GetDataP();
                BcDataHN = GetDataHN();
                BcDataOp = GetDataOP();
                BcDataCosto = GetDataCosto();
            }
            return _line;
        }

        public override int Peek()
        {
            return _baseReader.Peek();
        }

        #endregion


        private bool IsPageNumberLine()
        {
            var pageNumber = GetPageNumber();
            return pageNumber != null;
        }

        private bool IsBillingDateLine()
        {
            var billingDate =  GetBillingDate();
            return billingDate != null;
        }

        private bool IsClientNameLine()
        {
            return !string.IsNullOrWhiteSpace(GetClientName());
        }

        public bool IsItemDataLine()
        {
            var hasDataValues = !string.IsNullOrWhiteSpace(_line) && _line.Length >= 100;
            var startWithNumbers = hasDataValues && Int32.TryParse(_line.Substring(0, 2), out int day);
            return _numberLine >= (int) DataDayPositions.StartLine && hasDataValues && startWithNumbers;
        }

        public bool IsLineToIgnore()
        {
            var isPageNumberLine = IsPageNumberLine();
            var isBillingDateLine = IsBillingDateLine();
            var isClientNameLine = IsClientNameLine();
            var isItemDataLine = IsItemDataLine();

            return !isPageNumberLine 
                   && !isBillingDateLine 
                   && !isClientNameLine 
                   && !isItemDataLine;
        }

        
        private int? GetPageNumber()
        {
            if (string.IsNullOrWhiteSpace(_line) || _line.Length < 4 || !_line.StartsWith("Relacion No")) return null;
            var result = GetIntValue((int)PageNumberPositions.Start, (int)PageNumberPositions.Length);

            return result;
        }

        private string GetCode()
        {
            if (_numberLine != (int)CodePositions.Line) return null;
            var result = GetStringValue((int)CodePositions.Start, (int)CodePositions.Length);

            return result;
        }

        private DateTime? GetStartPeriodDate()
        {
            if (_numberLine != (int) StartPeriodDatePositions.Line) return null;
            var result = GetDateValue((int) StartPeriodDatePositions.Start, (int) StartPeriodDatePositions.Length, "MM-dd-yyyy");

            return result;
        }

        private DateTime? GetEndPeriodDate()
        {
            if (_numberLine != (int)EndPeriodDatePositions.Line) return null;
            var result = GetDateValue((int)EndPeriodDatePositions.Start, (int)EndPeriodDatePositions.Length, "MM-dd-yyyy");

            return result;
        }

        private DateTime? GetBillingDate()
        {
            if (_numberLine != (int)BillingDatePositions.Line) return null;
            var result = GetDateValue((int)BillingDatePositions.Start, (int)BillingDatePositions.Length, "dd/MM/yyyy");

            return result;
        }

        private string? GetClientName()
        {
            if (_numberLine != (int) ClientNamePositions.Line) return null;
            var result = GetStringValue((int) ClientNamePositions.Start, (int) ClientNamePositions.Length);

            return result;
        }


        private string? GetDataBcControlNumber()
        {
            if (!IsItemDataLine()) return null;
            var result = GetStringValue((int)DataBcControlNumberPositions.Start, (int)DataBcControlNumberPositions.Length);
            return result;
        }

        private string? GetDataUsuarioReferenceCode()
        {
            if (!IsItemDataLine()) return null;
            var result = GetStringValue((int)DataUsuarioReferenceCodePositions.Start, (int)DataUsuarioReferenceCodePositions.Length);
            return result;
        }

        private string? GetDataLastName()
        {
            if (!IsItemDataLine()) return null;
            var result = GetStringValue((int)DataLastNamePositions.Start, (int)DataLastNamePositions.Length);
            return result.RemoveAllNonAlphanumeric();
        }

        private string? GetDataMaidenName()
        {
            if (!IsItemDataLine()) return null;
            var result = GetStringValue((int)DataMaidenNamePositions.Start, (int)DataMaidenNamePositions.Length);
            return result.RemoveAllNonAlphanumeric();
        }

        private string? GetDataFirstName()
        {
            if (!IsItemDataLine()) return null;
            var result = GetStringValue((int)DataFirstNamePositions.Start, (int)DataFirstNamePositions.Length);
            return result.RemoveAllNonAlphanumeric();
        }

        private string? GetDataSvco()
        {
            if (!IsItemDataLine()) return null;
            var result = GetStringValue((int)DataSvcoPositions.Start, (int)DataSvcoPositions.Length);
            return result;
        }

        private string? GetDataSc()
        {
            if (!IsItemDataLine()) return null;
            var result = GetStringValue((int)DataScPositions.Start, (int)DataScPositions.Length);
            return result;
        }

        private string? GetDataA()
        {
            if (!IsItemDataLine()) return null;
            var result = GetStringValue((int)DataAPositions.Start, (int)DataAPositions.Length);
            return result;
        }

        private string? GetDataIC()
        {
            if (!IsItemDataLine()) return null;
            var result = GetStringValue((int)DataICPositions.Start, (int)DataICPositions.Length);
            return result;
        }

        private string? GetDataP()
        {
            if (!IsItemDataLine()) return null;
            var result = GetStringValue((int)DataPPositions.Start, (int)DataPPositions.Length);
            return result;
        }

        private string? GetDataHN()
        {
            if (!IsItemDataLine()) return null;
            var result = GetStringValue((int)DataHNPositions.Start, (int)DataHNPositions.Length);
            return result;
        }

        private string? GetDataOP()
        {
            if (!IsItemDataLine()) return null;
            var result = GetStringValue((int)DataOPPositions.Start, (int)DataOPPositions.Length);
            return result;
        }

        private decimal? GetDataCosto()
        {
            if (!IsItemDataLine()) return null;
            var result = GetDecimalValue((int)DataCostoPositions.Start, (int)DataCostoPositions.Length);
            return result;
        }
        
        private DateTime? GetDataTimestamp()
        {
            if (_numberLine < (int)DataDayPositions.StartLine || !BcStartPeriodDate.HasValue) return null;
            var yearNumber = BcStartPeriodDate.Value.Year;
            var monthNumber = BcStartPeriodDate.Value.Month;
            try
            {
                var day = GetIntValue((int)DataDayPositions.Start, (int)DataDayPositions.Length);
                var hourValue = GetDateValue((int)DataHourPositions.Start, (int)DataHourPositions.Length, "HH:mm:ss");
                
                if (monthNumber == 0 || !day.HasValue || !hourValue.HasValue) return null;
                var date = new DateTime(yearNumber, monthNumber, day.Value, hourValue.Value.Hour, hourValue.Value.Minute, hourValue.Value.Second,0);

                return date;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }

        }

        private DateTime? GetDateValue(int startPosition, int length, string format = "ddMMyyyy")
        {
            var text = GetStringValue(startPosition, length);
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                var result = DateTime.ParseExact(text, format, provider);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private int? GetIntValue(int startPosition, int length)
        {
            var text = GetStringValue(startPosition, length);
            if (int.TryParse(text, out var tmp))
                return int.Parse(text);

            return default;
        }

        private string? GetStringValue(int startPosition, int length, bool trim = true)
        {
            try
            {
                var result = _line.Substring(startPosition, length);
                if (!string.IsNullOrWhiteSpace(result) && trim) result = result.Trim();
                return result;
            }
            catch (Exception e)
            {
                return default;
            }
        }

        private decimal? GetDecimalValue(int startPosition, int length)
        {
            Decimal.TryParse(GetStringValue(startPosition, length, true), out decimal value);

            return value;
        }

        public int Position => _position;
        public int LineNumber => _numberLine;
    }

}
