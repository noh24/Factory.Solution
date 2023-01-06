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
    public int Status { get; set; }
    public List<EngineerMachine> EngineerMachine { get; }

  }
  public class CannotBeFutureAttribute : ValidationAttribute
  {
    public CannotBeFutureAttribute() { }
    public string GetErrorMessage() => "You cannot add machines installed in the future.";

    protected override ValidationResults IsValid(object value, ValidationContext validationContext)
    {
      var date = (DateTime)value;

      if (DateTime.Compare(date, DateTime.Now) > 0) return new ValidationResult(GetErrorMessage());
      else return ValidationResult.Success;
    }
  }
}