using System;
using System.Collections.Generic;

namespace CalculatorTest.Models;

public partial class TblCalculation
{
    public int Id { get; set; }

    public string? Input { get; set; }

    public string? FinalResult { get; set; }

    public DateTime? CreatedDate { get; set; }
}
