using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Machine
  {
    [Required(ErrorMessage = "Machine name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "What does the machine do?")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Enter a date")]
    [CannotBeFuture]
    public Nullable<DateTime> EnrollmentDate { get; set; }
    public int MachineId { get; set; }
    public int Status = 0;
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public List<EngineerMachine> EngineerMachines { get; }

  }
  public class CannotBeFutureAttribute : RangeAttribute
  {
    public CannotBeFutureAttribute()
      : base(typeof(DateTime),
              DateTime.MinValue.ToShortDateString(),
              DateTime.Now.ToShortDateString())
    { }
    public override string FormatErrorMessage(string name)
    {
      return $"Enter a date between {Minimum} and {Maximum}.";
    }
  }
}