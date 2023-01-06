using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Engineer
  {
    [Required(ErrorMessage = "Engineer name must be entered!")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Add Engineer's Specialty Machine")]
    public string SpecialtyMachine { get; set; }
    [Required(ErrorMessage = "Enter a date")]
    [FromNow]
    public Nullable<DateTime> LicenseRenewalDate { get; set; }
    public int EngineerId { get; set; }
    public bool Status { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public List<EngineerMachine> EngineerMachine { get; }
  }

  public class FromNowAttribute : ValidationAttribute
  {
    public FromNowAttribute() { }
    public string GetErrorMessage() => "The date you have entered indicates this engineer's license needs to be renewed. Renew the license before entering.";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var date = (DateTime)value;

      if (DateTime.Compare(date, DateTime.Now) < 0) return new ValidationResult(GetErrorMessage());
      else return ValidationResult.Success;
    }
  }
}