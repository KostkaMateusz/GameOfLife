
using FluentValidation;
using LifeAPI;

namespace GameOfLifeApi;

public class ModelValidator : AbstractValidator<DataTable>
{
	public ModelValidator()
	{
		RuleFor(t => t.Data).NotEmpty().Custom((value, context) =>
		{
			if(value is null)
			{
                context.AddFailure("Valule", "Table can not be empty");
            }			
			else if (value.Length <= 0)
			{
				context.AddFailure("Valule", "Size must be grather than 0");
			}
			
			else if (value.Length >= 200) {
                context.AddFailure("Valule", "Size must be smaller than 0");
            }

			try
			{
				var secondDmiension = value[0].Length;

            }catch(Exception)
			{
                context.AddFailure("Value", "Must be 2D array");
            }
				
			
		});
	}
}
