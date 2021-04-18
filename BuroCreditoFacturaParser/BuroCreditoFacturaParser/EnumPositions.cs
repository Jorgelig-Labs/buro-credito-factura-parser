using System;
using System.Collections.Generic;
using System.Text;

namespace Jorgelig.BuroCreditoFacturaParser
{
        enum CodePositions
    {
        Line = 1,
        Start = 17,
        End = 25,
        Length = (End - Start) - 1
    }

    enum PageNumberPositions
    {
        Line = 1,
        Start = 106,
        End = 113,
        Length = (End - Start) - 1
    }

    enum StartPeriodDatePositions
    {
        Line = 2,
        Start = 9,
        End = 20,
        Length = (End - Start) - 1
    }

    enum EndPeriodDatePositions
    {
        Line = 2,
        Start = 23,
        End = 34,
        Length = (End - Start) - 1
    }

    enum BillingDatePositions
    {
        Line = 2,
        Start = 105,
        End = 116,
        Length = (End - Start) - 1
    }

    enum ClientNamePositions
    {
        Line = 4,
        Start = 19,
        End = 84,
        Length = (End - Start) - 1
    }

    enum DataDayPositions
    {
        StartLine = 9,
        Start = 0,
        End = 3,
        Length = (End - Start) - 1
    }

    enum DataHourPositions
    {
        StartLine = 9,
        Start = 3,
        End = 12,
        Length = (End - Start) - 1
    }

    enum DataBcControlNumberPositions
    {
        StartLine = 9,
        Start = 12,
        End = 23,
        Length = (End - Start) - 1
    }

    enum DataUsuarioReferenceCodePositions
    {
        StartLine = 9,
        Start = 23,
        End = 51,
        Length = (End - Start) - 1
    }

    enum DataLastNamePositions
    {
        StartLine = 9,
        Start = 50,
        End = 65,
        Length = (End - Start) - 1
    }
    
    enum DataMaidenNamePositions
    {
        StartLine = 9,
        Start = 64,
        End = 78,
        Length = (End - Start) - 1
    }

    enum DataFirstNamePositions
    {
        StartLine = 9,
        Start = 77,
        End = 85,
        Length = (End - Start) - 1
    }

    enum DataSvcoPositions
    {
        StartLine = 9,
        Start = 84,
        End = 92,
        Length = (End - Start) - 1
    }

    enum DataScPositions
    {
        StartLine = 9,
        Start = 91,
        End = 94,
        Length = (End - Start) - 1
    }

    enum DataAPositions
    {
        StartLine = 9,
        Start = 94,
        End = 99,
        Length = (End - Start) - 1
    }

    enum DataICPositions
    {
        StartLine = 9,
        Start = 96,
        End = 99,//100
        Length = (End - Start) - 1
    }

    enum DataPPositions
    {
        StartLine = 9,
        Start = 100,
        End = 102,
        Length = (End - Start) - 1
    }

    enum DataHNPositions
    {
        StartLine = 9,
        Start = 102,
        End = 110,//106
        Length = (End - Start) - 1
    }

    enum DataOPPositions
    {
        StartLine = 9,
        Start = 106,
        End = 110,
        Length = (End - Start) - 1
    }

    enum DataCostoPositions
    {
        StartLine = 9,
        Start = 110,
        End = 116,
        Length = (End - Start) - 1
    }
}
